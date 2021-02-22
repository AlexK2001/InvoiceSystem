using InvoiceSystem.CommonBase.Extensions;
using InvoiceSystem.Contracts.Business;
using InvoiceSystem.Contracts.Client;
using InvoiceSystem.Logic.Controllers.Persistence;
using InvoiceSystem.Logic.DataContext;
using InvoiceSystem.Logic.Entities.Business;
using InvoiceSystem.Logic.Entities.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Logic.Controllers.Business
{
    internal class InvoiceController : ControllerObject, IControllerAccess<IInvoice>
    {
        private InvoiceHeadController headController;
        private InvoicePositionController positionController;
        public InvoiceController(IContext context) : base(context)
        {
            headController = new InvoiceHeadController(this);
            positionController = new InvoicePositionController(this);
        }

        public InvoiceController(ControllerObject controller) : base(controller)
        {
            headController = new InvoiceHeadController(this);
            positionController = new InvoicePositionController(this);
        }

        public int MaxPageSize { get; }
        public Task<int> CountAsync()
        {
            return headController.CountAsync();
        }

        public async Task<IEnumerable<IInvoice>> GetAllAsync()
        {
            List<IInvoice> result = new List<IInvoice>();

            foreach (var item in (await headController.GetAllAsync()).ToList())
            {
                result.Add(await GetByIdAsync(item.Id));
            }
            return result;
        }

        public async Task<IInvoice> GetByIdAsync(int id)
        {
            Invoice result = null;
            var head = await headController.GetByIdAsync(id);

            if (head != null)
            {
                result = new Invoice();
                result.HeadEntity.CopyProperties(head);
                foreach (var item in await positionController.QueryAsync(e => e.InvoiceHeadId == id))
                {
                    result.PositionEntities.Add(item);
                }
            }
            else
            {
                throw new Exception("Entity is not Existing");
            }
            return result;
        }

        public Task<IInvoice> CreateAsync()
        {
            return Task.Run<IInvoice>(() => new Invoice());
        }

        public async Task<IInvoice> InsertAsync(IInvoice entity)
        {
           entity.CheckArgument(nameof(entity));
           entity.InvoiceHead.CheckArgument(nameof(entity));
           entity.InvoicePositions.CheckArgument(nameof(entity));

           var invoice = new Invoice();
           invoice.HeadEntity.CopyProperties(entity.InvoiceHead);
           await headController.InsertAsync(invoice.HeadEntity);

           foreach (var item in entity.InvoicePositions)
           {
               var pos = new InvoicePosition();
               pos.CopyProperties(item);
               pos.InvoiceHead = invoice.HeadEntity;

               await positionController.InsertAsync(item);
                invoice.Add(item);
           }

           return invoice;

        }

        public async Task<IInvoice> UpdateAsync(IInvoice entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.InvoiceHead.CheckArgument(nameof(entity));
            entity.InvoicePositions.CheckArgument(nameof(entity));

            var head = await headController.UpdateAsync(entity.InvoiceHead);
            var res = default(Invoice);
            res.HeadEntity.CopyProperties(head);

            foreach (var item in await positionController.QueryAsync(p => p.InvoiceHeadId == entity.InvoiceHead.Id && !entity.InvoicePositions.Any(i => i.Id == p.Id)))
            {
                await positionController.DeleteAsync(item.Id);
            }

            foreach (var item in entity.InvoicePositions)
            {
                if (item.Id != 0)
                {
                    var pos = await positionController.UpdateAsync(item);

                    item.CopyProperties(pos);
                    res.Add(item);
                }
                else
                {
                    item.InvoiceHeadId = entity.InvoiceHead.Id;
                    var pos = await positionController.InsertAsync(item);

                    item.CopyProperties(pos);
                    res.Add(item);
                }
            }
            return res;
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await GetByIdAsync(id);
            if (invoice != null)
            {
                foreach (var item in invoice.InvoicePositions)
                {
                    await positionController.DeleteAsync(item.Id);
                }

                await headController.DeleteAsync(invoice.InvoiceHead.Id);
            }
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            headController.Dispose();
            positionController.Dispose();

            headController = null;
            positionController = null;
        }

        public Task<IEnumerable<IInvoice>> GetPageListAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
