using System.Web.Hosting;
using FluentScheduler;

namespace Ryanair.Aegis.Samplepumper.WebApplication
{
    public class PumpSamples : ITask, IRegisteredObject
    {

        private readonly object _lock = new object();

        private volatile bool _shuttingDown;

        public PumpSamples()
        {
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>Requests a registered object to unregister.</summary>
        /// <param name="immediate">
        ///     true to indicate the registered object should
        ///     unregister from the hosting environment before returning; otherwise, false.
        /// </param>
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                // Do work
            }
        }
    }
}