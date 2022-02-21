# Basic Star Wars Data REST API

This repo contains the details for standing up your own Star Wars Data REST API. This is by no means complete data, but its enough to get a basic demo up and running.

## What is this?

This is a Azure Functions app writen in C#. The application will utilise JSON files located in Azure Storage container to return data queried about them.
So far, this Data API contains information about people, planets and the nine star wars movies.
There are links between the people and the planets, and the film data which has a list of which people appear.
This is in no way a complete database, but it is enough for a some quick demos.

## How to use

### Generic Settings

Refer to this [documentation page](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp) for all you need to know for settings up Visual Studio Code and its plugins and the process for local testing and deploying to Azure.

### Project Specific settings

For project specific configuration, you will need to do the following:

1. The three data files in the \Data folder need to uploaded to Azure File storage. 
2. The three *_DATAFILE_URL fields in the `local.settings.json` file are set to where the files are uploaded to in Azure.

## How to deploy

Deployments are direct to your Azure subscription via Visual Studio code. Follow these steps to get up and running.

> This was a quick a dirty project, so there's no pipelines created for this! :)

- Go to the **Azure** tab on the right in Visual Studio Code
- Click on the "upload to azure" icon next to **FUNCTIONS**
- Select the Azure subscription
- Select the Azure function for the deployment
- Wait a few minutes
- Go to the Portal, and navigate to the Azure function
- Go to the **Settings -> Configuration** screen
- Add three application settings that match up with the three `*_DATAFILE_URL` values in the `local.settings.json` file
- Click the Save icon and restart the function
- Test from Azure

## Things to note

- When the application is deployed and when it is running locally, both instances will use the same source of files in Azure Storage containers.
