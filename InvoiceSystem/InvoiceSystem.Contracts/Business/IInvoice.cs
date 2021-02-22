using InvoiceSystem.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Contracts.Business
{
    public interface IInvoice : IIdentifiable, ICopyable<IInvoice>
    {
        IInvoiceHead InvoiceHead { get; }
        IEnumerable<IInvoicePosition> InvoicePositions { get; }
        new int Id { get; }

        int PositionsCount { get; }

        double UnitedPrice { get; }

        double Tax { get; }
        void Add(IInvoicePosition position);
        void Remove(IInvoicePosition position);
    }
}
