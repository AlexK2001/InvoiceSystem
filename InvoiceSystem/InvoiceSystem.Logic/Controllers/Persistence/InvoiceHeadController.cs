using InvoiceSystem.Contracts.Persistence;
using InvoiceSystem.Logic.DataContext;
using InvoiceSystem.Logic.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Controllers.Persistence
{
    internal class InvoiceHeadController : GenericController<IInvoiceHead, InvoiceHead>
    {
        public InvoiceHeadController(IContext context) : base(context)
        {
        }

        public InvoiceHeadController(ControllerObject controllerObject) : base(controllerObject)
        {
        }
    }
}
