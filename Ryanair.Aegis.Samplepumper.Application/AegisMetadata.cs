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
}