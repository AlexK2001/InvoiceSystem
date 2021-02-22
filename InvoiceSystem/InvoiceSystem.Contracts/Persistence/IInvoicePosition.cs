using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Contracts.Persistence
{
    public interface IInvoicePosition : IIdentifiable, ICopyable<IInvoicePosition>
    {
        int Order { get; set; }
        string Text { get; set; }
        double Quantity { get; set; }
        double Tax { get; set; }
        double Price { get; set; }
        public int InvoiceHeadId { get; set; }
    }
}
