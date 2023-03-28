using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.Helpers
{
    public abstract class ThreadedComponent : IDisposable
    {
        protected virtual string ThreadName => nameof(ThreadedComponent);

        protected virtual TimeSpan ThreadTimeout { get; set; } = new TimeSpan(0, 0, 0, 3);

        protected virtual TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 1);


        protected  Thread AllocatedThread { get; set; }

        protected ThreadedComponent()
        {
            AllocatedThread = new Thread(AllocatedThreadStart)
            {
                Name = ThreadName
            };
        }
        public void Start()
        {
            AllocatedThread.Start();
        }
        private void AllocatedThreadStart()
        {
            try
            {
                while (true)
                {
                    FrameAction();
                    Thread.Sleep(ThreadFrameSleep);
                }
            }
            catch (ThreadInterruptedException)
            {
            }
        }
        public virtual void Dispose()
        {
            AllocatedThread.Interrupt();

            if (!AllocatedThread.Join(ThreadTimeout))
            {
                AllocatedThread.Abort();
            }

            AllocatedThread = default;
        }

        protected abstract void FrameAction();
    }
}
