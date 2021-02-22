using InvoiceSystem.Logic.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Controllers
{
    internal abstract class ControllerObject : IDisposable
    {
        private bool contextDispose;
        protected IContext Context { get; private set; }

        protected ControllerObject(IContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
            contextDispose = true;
        }
        protected ControllerObject(ControllerObject controller)
        {
            if (controller == null)
                throw new ArgumentNullException(nameof(controller));

            Context = controller.Context;
            contextDispose = false;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (contextDispose && Context != null)
                    {
                        Context.Dispose();
                    }
                }
                Context = null;
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
