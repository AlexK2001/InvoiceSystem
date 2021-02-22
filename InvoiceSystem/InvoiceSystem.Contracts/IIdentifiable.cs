using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Contracts
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }
}
