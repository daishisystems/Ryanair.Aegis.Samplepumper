using System;
using Nest;

namespace Ryanair.Aegis.ElasticSearch.Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);

            for (var j = 0; j < 2500; j++)
            {
                var metadata = new AegisMetadata
                {
                    IPAddress = RandomIPGenerator.Create(),
                    Path = "availability",
                    Time = DateTime.UtcNow.ToString("O")
                };

                client.Index(metadata, i => i
                    .Index("traffic")
                    .Type("good")
                    .Id(Guid.NewGuid())
                    .Refresh()
                    );
            }

            Console.WriteLine("Indexed.");
            Console.ReadLine();
        }
    }
}