![ServiceBricks Logo](https://raw.githubusercontent.com/holomodular/ServiceBricks/main/Logo.png)   

[![NuGet version](https://badge.fury.io/nu/ServiceBricks.Notification.Microservice.svg)](https://badge.fury.io/nu/ServiceBricks.Notification.Microservice)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/e48b40f2064d0b0a359109f864c3aff7/raw/servicebricksnotification-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceBricks Notification Microservice

## Overview

This repository contains the notification microservice built using the ServiceBricks foundation.
The notification microservice is responsible for sending emails and SMS messages from the system.
It provides a background task to process notify messages, along with retry, should external providers not be available when being sent.
It subscribes to service bus messages for email and sms broadcasts, so that the security microservice and others have a default mechanism to send notifications from the system.

### Supported Providers
By default, dependency injection is registered with a dummy email and sms provider that does not send any message but simply logs them using an ILogger<> inteface.
You must explicitly add a line of code and configurations to register the providers below. 
You can quickly build your own providers by including the **ServiceBricks.Notification.Model** NuGet package and implementing the IEmailProvider and ISmsProvider interfaces.


#### Email

* SendGrid - Nuget Package - ServiceBricks.Notification.SendGrid

Add ServiceBricks:Notification:SendGrid:ApiKey to your appsettings.config
```csharp
using ServiceBricks.Notification.SendGrid;
services.AddServiceBricksNotificationSendGrid();
```

* SMTP  - Included in ServiceBricks.Notification

See config below
```csharp
services.AddScoped<IEmailProvider, SmtpEmailProvider>();
```

#### SMS

Coming soon! Or build your own as needed.

## Data Transfer Objects

### NotifyMessageDto - Admin Policy
Used to store a notification message. It additionally contains properties to support the IDpWorkProcess and WorkService for processing the table like a work queue.

```csharp

public class NotifyMessageDto : DataTransferObject, IDpWorkProcess
{    
    public string SenderType { get; set; }
    public virtual bool IsHtml { get; set; }
    public virtual string Priority { get; set; }
    public virtual string Subject { get; set; }
    public virtual string BccAddress { get; set; }
    public virtual string CcAddress { get; set; }
    public virtual string ToAddress { get; set; }
    public virtual string FromAddress { get; set; }
    public virtual string Body { get; set; }
    public virtual string BodyHtml { get; set; }

    // IDpWorkProcess related
    public bool IsError { get; set; }
    public bool IsComplete { get; set; }
    public int RetryCount { get; set; }
    public DateTimeOffset FutureProcessDate { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
    public DateTimeOffset ProcessDate { get; set; }
    public string ProcessResponse { get; set; }
    public bool IsProcessing { get; set; }

}

```

## Business Events and Processes

### SendNotificationProcess
This process is used to send a notify message. The SendNotificationProcessRule class implements the functionality to send an email or sms sendertype.
You can register new rules to handle new sender types or unregister the existing rule and build your own.


## Background Tasks and Timers

### SendNotificationTimer class
A background timer can be enabled, with an initial delay and interval, that executes the SendNotificationTask.

[View Source](https://github.com/holomodular/ServiceBricks-Notification/blob/main/src/V1/ServiceBricks.Notification/Background/SendNotificationTimer.cs)

### SendNotificationTask class
This background task invokes the [NotifyMessageWorkService](https://github.com/holomodular/ServiceBricks-Notification/blob/main/src/V1/ServiceBricks.Notification/Background/SendNotificationTask.cs)

[View Source](https://github.com/holomodular/ServiceBricks-Notification/blob/main/src/V1/ServiceBricks.Notification/Background/SendNotificationTask.cs)


## Service Bus

### CreateApplicationEmailBroadcast
This microservice subscribes to the CreateApplicationEmailBroadcast message.
It is associated to the [CreateApplicationEmailRule](https://github.com/holomodular/ServiceBricks-Notification/blob/main/src/V1/ServiceBricks.Notification/Rule/CreateApplicationEmailRule.cs) Business Rule.
When receiving the message from service bus, it will attempt to send the notification first, then store the process disposition before creating the message in storage. This reduces the reliance on the timer for sending messages and sends messages immediately when received.

```csharp

    public class CreateApplicationEmailBroadcast : DomainBroadcast<ApplicationEmailDto>
    {
        public CreateApplicationEmailBroadcast(ApplicationEmailDto obj)
        {
            DomainObject = obj;
        }
    }

```

### CreateApplicationSmsBroadcast
This microservice subscribes to the CreateApplicationSmsBroadcast message.
It is associated to the [CreateApplicationSmsRule](https://github.com/holomodular/ServiceBricks-Notification/blob/main/src/V1/ServiceBricks.Notification/Rule/CreateApplicationSmsRule.cs) Business Rule.
When receiving the message from service bus, it will attempt to send the notification first, then store the process disposition before creating the message in storage. This reduces the reliance on the timer for sending messages and sends messages immediately when received.
```csharp

    public class CreateApplicationSmsBroadcast : DomainBroadcast<ApplicationSmsDto>
    {
        public CreateApplicationSmsBroadcast(ApplicationSmsDto obj)
        {
            DomainObject = obj;
        }
    }

```

## Additional


## Application Settings

```json

{
  // ServiceBricks Settings
  "ServiceBricks":{

    // Notification Microservice Settings
    "Notification": {

      // Send Notification Process
      "Send": {
          "TimerEnabled": false,
          "TimerIntervalMilliseconds": 7000,
          "TimerDueMilliseconds": 1000,
          "EmailFromDefault": "support@servicebricks.com",
          "SmsFromDefault": "1234567890",
          "IsDevelopment": false,
          "DevelopmentEmailTo": "developer@servicebricks.com",
          "DevelopmentSmsTo": "1234567890"
      },

      // SMTP provider
      "Smtp": {
          "EmailServer": "yourserver.com",
          "EmailPort": 123,
          "EmailEnableSsl": true,
          "EmailUsername": "username",
          "EmailPassword": "password"
      },
    
      // ServiceBricks.Notification.SendGrid NuGet Package
      "SendGrid": {
        "ApiKey": "SendGridApiKey"
      }
    }
  }
}

```

# About ServiceBricks

ServiceBricks is the cornerstone for building a microservices foundation.
Visit https://ServiceBricks.com to learn more.

