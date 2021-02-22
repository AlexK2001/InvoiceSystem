using InvoiceSystem.CommonBase.Extensions;
using InvoiceSystem.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Entities.Persistence
{
    internal class InvoiceHead : IdentityObject, IInvoiceHead
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public void CopyProperties(IInvoiceHead other)
        {
            other.CheckArgument(nameof(other));

            Id = other.Id;
            Date = other.Date;
            Text = other.Text;
            Street = other.Street;
            ZipCode = other.ZipCode;
            City = other.City;
        }
        public IEnumerable<InvoicePosition> Positions { get; set; }
    }
}
