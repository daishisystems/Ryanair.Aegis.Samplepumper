using System.Collections.Concurrent;

namespace Ryanair.Aegis.Samplepumper.WebApplication
{
    public static class Samples
    {
        public static ConcurrentQueue<AegisMetadata> Metadata { get; } =
            new ConcurrentQueue<AegisMetadata>();
    }
}