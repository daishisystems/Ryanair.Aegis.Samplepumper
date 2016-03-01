using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Ryanair.Aegis.Samplepumper.Application
{
    internal class Program
    {
        private const string EventHubName = "samplepumper";

        private const string ConnectionString =
            "Endpoint=sb://ryanair-samplepumper.servicebus.windows.net/;SharedAccessKeyName=ALL;SharedAccessKey=ptn2jAbyjKdKEdNBoJELBzEMR84+qFo60YhQ4LvIr2I=";

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uploading metadata...");

            var eventcounter = 0;
            var eventHubClient =
                EventHubClient.CreateFromConnectionString(ConnectionString,
                    EventHubName);

            //UploadMetadata(eventHubClient,
            //    AegisMetadataGenerator.GenerateTrustworthyMetadata(1000),
            //    ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(10),
                ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateTrustworthyMetadata(50),
                ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(20),
                ref eventcounter);
            //UploadMetadata(eventHubClient,
            //    AegisMetadataGenerator.GenerateTrustworthyMetadata(1000),
            //    ref eventcounter);

            eventHubClient.Close();

            Console.WriteLine("Upload complete.");
            Console.ReadLine();
        }

        private static void UploadMetadata(EventHubClient client,
            IEnumerable<AegisMetadata> metadata, ref int eventCounter)
        {
            foreach (var m in metadata)
            {
                client.Send(new EventData(
                    Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(m))));

                //Console.Clear();
                //Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("{0} events uploaded...", ++eventCounter);
            }
        }
    }
}