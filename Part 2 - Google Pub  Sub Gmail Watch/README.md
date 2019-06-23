# Set up Google Pub/Sub Project with Gmail Watch

## Create a Google Pub/Sub

1. Select Pub/Sub on the GCP Navigation bar in the Google cloud console (https://console.cloud.google.com).  
2. Make sure your project is selected on the top bar
3. Select Enable API

![Create PubSub](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP1.png?raw=true)
3. Select create a topic and give it a name like "GmailNot". You should end up with something like below

![Create Topic](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP2.png?raw=true)

## Add Pub Watch to Gmail account

We will now programaticly set a subscription watch account on the Gmail inbox. This will allow us later to have a subscription that will get notified when mail arrives.  We will add the code to the last part to create the watch account once a day (the watch account expires).  Add this code to the bottom of the function that we created in Part 1.

code()
    WatchRequest body = new WatchRequest()
    {
        TopicName = sTopicName,
        LabelIds = new[] { "INBOX" }
    };

    UsersResource.WatchRequest watchRequest = service.Users.Watch(body, "me");
    try
    {
        WatchResponse test = watchRequest.Execute();
        log.LogInformation($"Watch Reqeust Response: {test.Expiration.Value.ToString()}");

    }
    catch(Exception e)
    {
        var err = e;
    }
1. Run the Modified code and the Logs should look like below
![Azure Log](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure1.png?raw=true)

2. If sucessful you should now have the publish side of the Pub/Sub Project setup

## Create Http trigger and verification proxy

We will now create a trigger function that subscribes to the published Gmail notification

1. Create a new Azure Http trigger function with "Anonymous" Authorization. We are only doing this for tutorial purposes a production application should use a Function key. Save the function URL for use later
2. Goto the topic in Google Cloud Console select "Create Subscription"

![Create Topic](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP3.png?raw=true)

3. Domain Ownership Validation - We now need to show that we own the HttpEnd point we are going to call for our subscription.  We do this by putting a file in the Azure function directory that Google can verify.  Go to the folling site <https://search.google.com/search-console>
4. Select URL prefix and paste in the Azure function URL you had saved in step 1. e.g. <https://gmailtest.azurewebsites.net/>
5. Download the html file. 

![Verify](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP5.png?raw=true)
6. Stay on the Verify ownership page as we will come back to verify.
7. Google is going to the url provided on the verification page.  Azure needs to be able to serve up that page. e.g. <https://gmailtest.azurewebsites.net/api/Gmail/google8c5c0e99c5320045.html>
8. Create Validation function to serve up the validation page. Name the fuction "Validation" and give it anonymous access
9. paste the code in the Run function. Put in your validation html
Code()
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using System.Net.Http.Headers;

    public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var stream = new FileStream(@"d:\home\site\wwwroot\Validation\google8c5c0e99c5320045.html", FileMode.Open);
        response.Content = new StreamContent(stream);
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        return response;
    }
6. To upload the verification html file go to view files and select upload
7. Create proxy for the validation html file
![Azure Log](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure3.png?raw=true)
8. Go back to Google Search Console and verify the site ownership

## Subsribe to notification with Azure Http trigger function

8. Create the subscription as shown below

![Create Topic](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP4.png?raw=true)

You are now ready to go to the next step
