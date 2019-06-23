using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;


public static void Run(TimerInfo myTimer, ILogger log)
{
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Need to only run one time to get the Google refresh token
            //GetAccessToken_OneTime(); 

            string sGoogleRefreshToken = @"";// From token.json
            string sClientId = "";// From credentials.json
            string sClientSecret = "";// From credentials.json
            string ApplicationName = "Gmail App";

            var token = new TokenResponse { RefreshToken = sGoogleRefreshToken };
            var credentials = new UserCredential(new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = sClientId,
                        ClientSecret = sClientSecret
                    }
                }), "user", token);


            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName,
            });

            // Create Gmail watch on inbox
            WatchRequest body = new WatchRequest()
            {
                TopicName = sTopicName,
                LabelIds = new[] { "INBOX" }
            };

            UsersResource.WatchRequest watchRequest = service.Users.Watch(body, "me");
            try
            {
                WatchResponse test = watchRequest.Execute();

            }
            catch(Exception ex)
            {
                var err = ex;
            }
}
