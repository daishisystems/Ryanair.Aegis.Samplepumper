using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ryanair.Aegis.Samplepumper.Application
{
    internal class AegisMetadata
    {
        public string IPAddress { get; set; }
        public string Path { get; set; }
        public string Time { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}   {2}", IPAddress, Path, Time);
        }
    }

    internal static class RandomIPGenerator
    {
        private static readonly Random Random = new Random();

        public static string Create()
        {
            return string.Format("{0}.{1}.{2}.{3}", Random.Next(0, 255),
                Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
        }
    }

    internal static class AegisMetadataGenerator
    {
        public static List<AegisMetadata> GenerateTrustworthyMetadata(
            int batchCount)
        {
            var trustworthyMetadata = new List<AegisMetadata>();

            for (var i = 0; i < batchCount; i++)
            {
                trustworthyMetadata.Add(new AegisMetadata
                {
                    IPAddress = RandomIPGenerator.Create(),
                    Path = Guid.NewGuid().ToString(),
                    Time = DateTime.Now.ToString("O")
                });
            }

            return trustworthyMetadata;
        }

        public static List<AegisMetadata> GenerateUntrustworthyMetadata(
            int batchCount)
        {

            // Create a new IP address and unique path
            var ipAddress = RandomIPGenerator.Create();
            // Path does not need to conform to standard HTTP path-formatting
            var path = Guid.NewGuid().ToString();

            // Cache the current time
            var timeSample = DateTime.Now;
            var second = timeSample.Second;

            var untrustworthyMetadata = new List<AegisMetadata>();

            // Create a list of sequential times, spaced by 1 second
            for (var i = 0; i < batchCount; i++)
            {
                var time = new DateTime(timeSample.Year, timeSample.Month,
                    timeSample.Day, timeSample.Hour, timeSample.Minute,
                    second++);

                untrustworthyMetadata.Add(new AegisMetadata
                {
                    IPAddress = ipAddress,
                    Path = path,
                    Time = time.ToString("O")
                });
            }

            return untrustworthyMetadata;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Randomise a large collection of HTTP metadata

            var trustworthy =
                AegisMetadataGenerator.GenerateTrustworthyMetadata(10);

            var untrustworthy =
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(5);

            var trustworthyTasks =
                trustworthy.Select(aegisMetadata => Task.Run(() =>
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(aegisMetadata);
                }));

            var untrustworthyTasks =
                untrustworthy.Select(aegisMetadata => Task.Run(() =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(aegisMetadata);
                }));

            Console.WriteLine("Begin output mixed-trust events...");
            Task.WaitAny(trustworthyTasks.ToArray());
            Task.WaitAny(untrustworthyTasks.ToArray());

            // Asynchronously pump collection in batches to Azure Event Hub

            // Introduce random wait-time

            // Repeat step 2

            Console.ReadLine();
        }
    }
}