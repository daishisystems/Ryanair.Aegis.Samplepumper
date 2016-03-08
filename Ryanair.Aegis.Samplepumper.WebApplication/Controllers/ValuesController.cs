using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ryanair.Aegis.Samplepumper.WebApplication.Controllers
{
    [RoutePrefix("api/samples")]
    public class ValuesController : ApiController
    {
        [Route("sample/{ipaddress}")]
        [AcceptVerbs("GET")]
        public IEnumerable<AegisMetadata> Get(string ipaddress)
        {
            Samples.Metadata.Enqueue(new AegisMetadata
            {
                IPAddress = ipaddress.Replace('-', '.'),
                Path = "availability",
                Time = DateTime.UtcNow.ToString("O")
            });

            return Samples.Metadata;
        }
    }
}