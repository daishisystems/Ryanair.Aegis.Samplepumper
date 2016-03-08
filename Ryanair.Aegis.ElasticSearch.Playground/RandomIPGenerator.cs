using System;

namespace Ryanair.Aegis.ElasticSearch.Playground
{
    internal static class RandomIPGenerator
    {
        private static readonly Random Random = new Random();

        public static string Create()
        {
            return string.Format("{0}.{1}.{2}.{3}", Random.Next(0, 255),
                Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
        }
    }
}