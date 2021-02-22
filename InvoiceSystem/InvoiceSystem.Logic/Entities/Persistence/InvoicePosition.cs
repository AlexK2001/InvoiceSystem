using InvoiceSystem.CommonBase.Extensions;
using InvoiceSystem.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Entities.Persistence
{
    internal class InvoicePosition : IdentityObject, IInvoicePosition
    {
        public int Order { get; set; }
        public string Text { get; set; }
        public double Quantity { get; set; }
        public double Tax { get; set; }
        public double Price { get; set; }
        public int InvoiceHeadId { get; set; }

        public void CopyProperties(IInvoicePosition other)
        {
            other.CheckArgument(nameof(other));

            Id = other.Id;
            Order = other.Order;
            Text = other.Text;
            Quantity = other.Quantity;
            Tax = other.Tax;
            Price = other.Price;
            InvoiceHeadId = other.InvoiceHeadId;
        }

        public InvoiceHead InvoiceHead { get; set; }
    }
}
