# Set up Google Cloud Project with Gmail API and OAuth2.o

## Create a Google Cloud Platform project

1. Go to https://console.cloud.google.com and sign in with your Google account
2. Click "Select a project" and then "New Project"

![Create Project](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP1.png?raw=true)
3. Name your project

## Enable Gmail API

1. Go to the API & Services - Library

![Project API](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP2.png?raw=true)
2. Search "Gmail" and enable the API

## Get oAuth key

We will use oAuth to access the Gmail account. I suggest creating a new Gmail account for testing. OAuth requires a user approved/generated token that your application uses to access their data. We will use the refresh token to authenticate our access to our gmail account.

1. Generate OAuth client id and secret code (credentials.json).  This will be used to generate refresh token.

![Credentials.json](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/GCP3.png?raw=true)

Here is C# code to get the oAuth access and refresh tokens.  The acess token expires so we will use the refresh token to get new access tokens.  This code can be run just once in Visual Studio or VS code to extract the refresh token. The refresh token is in the generated file "token.json". Keep this information safe as the refresh token doesn't expire.

code()
        UserCredential credential;

        using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            // The file token.json stores the user's access and refresh tokens, and is created
            // automatically when the authorization flow completes for the first time.
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + credPath);
        }


## Login to Gmail from Azure function

We will now login to Gmail from an Azure function. Using the OAuth information that we generated.

1. Create a new Azure "Function App" by going to the portal https://portal.azure.com

![Azure Function App](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure1.png?raw=true)
2. Select a location near you and a ".NET Core" runtime stack
3. Create a new function select --> "In-Portal" --> "Timer"  function
4. The function should look like this

![Azure Function](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure2.png?raw=true)
5. Add Google API to the application.  View files on the right side of the dashboard and select add.  Type "function.proj".

![function.proj](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure3.png?raw=true)
6. Add the Google.Apis.Gmail.v1 And Newtonsoft.Json to function.proj and Save to install the nuget package

![function.proj2](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure6.png?raw=true)
7. Run the script. You can check the Logs and it should look similar to below

![Azure Log](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure4.png?raw=true)
8. Modify the timer to run once a day.  Set the cron expression to run at 11pm.

![CRON](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/Resources/Azure5.png?raw=true)

You are now ready to go to the next step
