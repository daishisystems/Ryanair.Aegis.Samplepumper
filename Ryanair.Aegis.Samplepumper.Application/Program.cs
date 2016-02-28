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
            return string.Format("IP address:   {0}", IPAddress);
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
        public static IEnumerable<AegisMetadata> GenerateTrustworthyMetadata(
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

        public static IEnumerable<AegisMetadata> GenerateUntrustworthyMetadata(
            int batchCount)
        {

            // Create a new IP address and unique path
            var ipAddress = RandomIPGenerator.Create();
            // Path does not need to conform to standard HTTP path-formatting
            var path = Guid.NewGuid().ToString();

            // Cache the current time
            var timeSample = DateTime.Now;
            var untrustworthyMetadata = new List<AegisMetadata>();

            // Create a list of sequential times, spaced by 1 second
            for (var i = 0; i < batchCount; i++)
            {
                untrustworthyMetadata.Add(new AegisMetadata
                {
                    IPAddress = ipAddress,
                    Path = path,
                    Time = timeSample.AddSeconds(1).ToString("O")
                });
            }

            return untrustworthyMetadata;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            AsyncMain(args).Wait();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static async Task AsyncMain(string[] args)
        {
            Console.WriteLine("Begin outputting mixed-trust events...");

            await RunTasks(1500,
                AegisMetadataGenerator.GenerateTrustworthyMetadata(10));

            await RunTasks(1000,
                AegisMetadataGenerator.GenerateUntrustworthyMetadata(2));
        }

        private static async Task RunTasks(int delayThreshhold,
            IEnumerable<AegisMetadata> metadata)
        {
            var tasks =
                metadata.Select(m => Task.Run(async () =>
                {
                    var random = new Random();
                    await Task.Delay(random.Next(10, delayThreshhold));

                    Console.WriteLine(m);
                }));

            await Task.WhenAll(tasks.ToArray());
        }
    }
}