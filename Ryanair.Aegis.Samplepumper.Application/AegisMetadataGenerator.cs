using System;
using System.Collections.Generic;

namespace Ryanair.Aegis.Samplepumper.Application
{
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
}