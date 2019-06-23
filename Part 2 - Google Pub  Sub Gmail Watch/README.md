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

    }
    catch(Exception ex)
    {
        var err = ex;
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
