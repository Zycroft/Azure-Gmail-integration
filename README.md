# Azure-Gmail-integration

This project is an example of how to have gmail notify Azure through an Http trigger when a new email has arrived.  Then the email can be consumed and stored on Azure blob storage.

1. Set up Google Cloud Project with Gmail API and oAuth2.o
2. Set up GCP Pub on project and watch gmail account
3. Set up Azure web hook sub scription
4. Read and store gmail when push notification is recieved
5. Add Azure application insights to project
