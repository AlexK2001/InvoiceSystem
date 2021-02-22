using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Contracts.Persistence
{
    public interface IInvoiceHead : IIdentifiable, ICopyable<IInvoiceHead>
    {
        DateTime Date { get; set; }
        string Text { get; set; }
        string Street { get; set; }
        string ZipCode { get; set; }
        string City { get; set; }

    }
}
