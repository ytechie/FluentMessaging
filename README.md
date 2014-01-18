# FluentMessaging

Declarative syntax for piping data in Azure Service Bus Message queues and other technologies.

### Moving Data from a Queue to a Topic
	QueueFramework
	    .FromQueue(queueConnectionString, queueName)
	    .ProcessWith<PassthroughQueueProcessor>()
	    .OutputToTopic(topicConnectionString, queueName);

### Subscribing to a Topic Subscription in the Form of a Reactive Stream

	var rx = QueueFramework
		.FromTopicSubscription(connectionString, topicName, subscriptionName)
		.OutputToReactive(serializer);

# License

Microsoft Developer & Platform Evangelism

Copyright (c) Microsoft Corporation. All rights reserved.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

The example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious. No association with any real company, organization, product, domain name, email address, logo, person, places, or events is intended or should be inferred.

