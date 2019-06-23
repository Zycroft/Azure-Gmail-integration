# Azure-Gmail-integration

This project is an example of how to have gmail notify Azure through an Http trigger when a new email has arrived.  Then the email can be consumed and stored on Azure blob storage.

1. [Set up Google Cloud Project with Gmail API and OAuth2.o](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%201%20-%20Gmail%20API%20and%20oAuth/README.md)
2. [Set up GCP Pub on the project and watch your gmail account](https://github.com/Zycroft/Azure-Gmail-integration/blob/master/Part%202%20-%20Google%20Pub%20%20Sub%20Gmail%20Watch/README.md)
3. Set up Azure web hook to subscribe to the gmail pub
4. Read and store gmail when push notification is recieved
5. Add Azure application insights to project
