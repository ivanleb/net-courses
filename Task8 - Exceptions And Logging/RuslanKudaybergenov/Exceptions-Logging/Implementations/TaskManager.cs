using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exceptions_Logging.Abstractions;

namespace Exceptions_Logging.Implementations
{
    public class TaskManager : IDisposable
    {
        public enum Functions { Cube = 1, Linear = 2, Bad = 3 };

        private ILogable loggerService;

        public TaskManager(ILogable loggerService)
        {
            DictionaryOfThreads = new Dictionary<string, Thread>();
            this.loggerService = loggerService;
        }

        private Dictionary<string, Thread> DictionaryOfThreads;
        private List<IPointProducer> producers;

        public void AddThread(Functions function, string threadName)
        {
            switch (function)
            {
                case Functions.Cube:
                    {
                        DictionaryOfThreads.Add("cube", new Thread(
                                () => new CubeFunctionProducer(loggerService, threadName).Run(
                                    (point) => loggerService.Info($"Function {point}"))));

                        break;
                    }
                case Functions.Linear:
                    {
                        DictionaryOfThreads.Add("linear", new Thread(
                            () => new LinearFunctionProducer(loggerService, threadName).Run(
                                (point) => loggerService.Info($"Function {point}"))));

                        break;
                    }
            }
        }

        public void Run()
        {
            foreach (var thread in DictionaryOfThreads)
            {
                thread.Value.Start();
            }
        }

        public void AddBadProducerWithConnectedClient(Client client)
        {
            BadProducer bad = new BadProducer(loggerService, "bad");
            DictionaryOfThreads.Add("bad", new Thread(() => bad.Run((point) => loggerService.Info($"Function {point}"))));
            client.StartListenToBadProducer(bad);
            
        }

        public void Stop()
        {
            foreach (var thread in DictionaryOfThreads)
            {
                thread.Value.Abort();
            }
        }

        

       

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                DictionaryOfThreads = null;
                loggerService = null;
                disposedValue = true;
                
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
         ~TaskManager() {
           // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
           Dispose(false);
         }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
