using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;
using EasyNetQ;
namespace AppharborBot.WebHost
{
    public class RabbitMQClient
    {
        public void Connect()
        {
            //string exchangeName = "Appharborbot", queueName = "Appharborqueue", routingKey = "";
            //ConnectionFactory factory = new ConnectionFactory();
            //factory.Uri = @"amqp://13b7233d-b4f8-4986-9cd2-4254b38d1869_apphb.com:vDoN650Qz_Lz3wSLrdBOYkkm-EPsFfCx@lemur.cloudamqp.com/13b7233d-b4f8-4986-9cd2-4254b38d1869_apphb.com";
            //IConnection conn = factory.CreateConnection();
            ////IModel model = conn.CreateModel();

            //model.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);
            //model.QueueDeclare(queueName, true, false,false, new Dictionary<string, object>());
            //model.QueueBind(queueName, exchangeName, routingKey);

            //byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
            //model.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

            var bus = RabbitHutch.CreateBus("lemur.cloudamqp.com", "5672", "13b7233d-b4f8-4986-9cd2-4254b38d1869_apphb.com", "13b7233d-b4f8-4986-9cd2-4254b38d1869_apphb.com", "vDoN650Qz_Lz3wSLrdBOYkkm-EPsFfCx", new MyLogger());

            Notification n = new Notification();
            n.application = new Application();
            n.build = new Build();
            n.application.name = "test";

            bus.Connected += () => { System.Diagnostics.Trace.TraceInformation("connected"); };
            bus.Disconnected += () => { System.Diagnostics.Trace.TraceInformation("disconnected"); };

            
            using (var publishChannel = bus.OpenPublishChannel())
            {
                publishChannel.Publish<Notification>(n);
            }
        }
    }
}