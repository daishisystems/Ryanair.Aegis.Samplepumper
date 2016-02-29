using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Ryanair.Aegis.Samplepumper.Application
{
    internal class Program
    {
        private const string EventHubName = "fraud-detection";

        private const string ConnectionString =
            "Endpoint=sb://fraud-detection-ns.servicebus.windows.net/;SharedAccessKeyName=AllRule;SharedAccessKey=eYsQ3l8VfFd1/COKp4QUD2MGQ4Sl5QwSqt4DL3rA/U4=";

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uploading metadata...");

            var eventcounter = 0;
            var eventHubClient =
                EventHubClient.CreateFromConnectionString(ConnectionString,
                    EventHubName);

            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateTrustworthyMetadata(10000),
                ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(50),
                ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateTrustworthyMetadata(2000),
                ref eventcounter);
            UploadMetadata(eventHubClient,
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(50),
                ref eventcounter);

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

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0} events uploaded...", ++eventCounter);
            }
        }
    }
}