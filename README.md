# CMS Tweets

This repository contains an application which shows tweets with certain hashtag in real time, this hashtag on application is inserted by UI and can be changed at any time.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project in a production environment.

### Prerequisites

Install the following software to build and run the application:

- **Visual Studio 2019 v16.3** (or later) with the **.NET desktop development** workload installed

- [.NET core 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)

### Configuring

Update the appsettings.json file under CMSTweets folder or if you want to use the already deployed application then use the folder CMSTweetsPubDependent.

First you need to set you credentials to the Twitter API, for more information about Twitter API and how to get the tokens see "https://developer.twitter.com/en/docs/basics/apps/overview":

```
"Consumer_Key"
"Consumer_Secret"
"AccessToken"
"AccessTokenSecret"
```
After you set the number of tweets to show on screen:
```
"NumberOfResults"
```
Then you set the number of seconds to check new tweets:
```
"NumberOfSecondsToCheckNewTweets"
```

### Installing

Clone the repository to a working folder, navigate to the folder CMSTweets.

Open a solution in Visual Studio.

Run the project (press F5 key).

## Deployment

You have two options to deploy the application:

1. You can use the folder CMSTweetsPubDependent:
   1. This folder already contain the application published.
   2. Configure the appsettings.json as in the Configuring section.
   3. Open the CMSTweets.exe file.
2. You can publish the application in Visual Studio:
   1. Configure the appsettings.json as in the Configuring section.
   2. Select Build > Deploy Solution.

## Built With

* [.Net Core 3.0](https://docs.microsoft.com/en-us/dotnet/core/) - The .Net Core framework
* [WPF](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/overview/index) - UI framework for desktop client applications for Windows
* [Tweetinvi](https://github.com/linvi/tweetinvi/wiki/Introduction) - .NET C# library that allows developers to interact with Twitter
