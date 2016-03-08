using FluentScheduler;

namespace Ryanair.Aegis.Samplepumper.WebApplication
{
    public class SchedulerRegistry : Registry
    {
        public SchedulerRegistry()
        {
            Schedule<PumpSamples>().ToRunNow().AndEvery(2).Seconds();
        }
    }
}