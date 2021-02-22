using InvoiceSystem.Contracts.Persistence;
using InvoiceSystem.Logic.DataContext;
using InvoiceSystem.Logic.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Controllers.Persistence
{
    internal class InvoicePositionController : GenericController<IInvoicePosition, InvoicePosition>
    {
        public InvoicePositionController(IContext context) : base(context)
        {
        }

        public InvoicePositionController(ControllerObject controllerObject) : base(controllerObject)
        {
        }
    }
}
