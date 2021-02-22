using InvoiceSystem.CommonBase.Extensions;
using InvoiceSystem.Contracts.Business;
using InvoiceSystem.Contracts.Persistence;
using InvoiceSystem.Logic.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceSystem.Logic.Entities.Business
{
    internal class Invoice : IdentityObject, IInvoice
    {
        public InvoiceHead HeadEntity { get; set; }
        public IInvoiceHead InvoiceHead => HeadEntity;
        public List<InvoicePosition> PositionEntities { get; set; } = new List<InvoicePosition>();
        public IEnumerable<IInvoicePosition> InvoicePositions => PositionEntities;
        public int PositionsCount => PositionEntities.Count;
        public double UnitedPrice => PositionEntities.Sum(p => p.Price);
        public double Tax => PositionEntities.Sum(p => p.Tax * 0.2);
        public void Add(IInvoicePosition position)
        {
            position.CheckArgument(nameof(position));

            var pos = new InvoicePosition();

            pos.CopyProperties(position);
            PositionEntities.Add(pos);
        }

        public void Remove(IInvoicePosition position)
        {
            position.CheckArgument(nameof(position));

            var pos = PositionEntities.FirstOrDefault(i => (i.Id != 0 && i.Id == position.Id) || (i.Id == 0 && i.Text != null && i.Text.Equals(position.Text)));
            if (pos != null)
            {
                PositionEntities.Remove(pos);
            }
        }

        public new int Id => HeadEntity.Id;
        public void CopyProperties(IInvoice other)
        {
            other.CheckArgument(nameof(other));
            other.InvoiceHead.CheckArgument(nameof(other));
            other.InvoicePositions.CheckArgument(nameof(other));

            HeadEntity.CopyProperties(other.InvoiceHead);
            PositionEntities.Clear();
            foreach (var item in other.InvoicePositions)
            {
                var pos = new InvoicePosition();

                pos.CopyProperties(item);
                PositionEntities.Add(pos);
            }
        }
    }
}
