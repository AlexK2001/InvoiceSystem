using InvoiceSystem.CommonBase.Extensions;
using InvoiceSystem.Contracts;
using InvoiceSystem.Contracts.Client;
using InvoiceSystem.Logic.DataContext;
using InvoiceSystem.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Logic.Controllers
{
    internal abstract class GenericController<I, E> : ControllerObject, IControllerAccess<I>
        where I : IIdentifiable
        where E : IdentityObject, I, ICopyable<I>, new()
    {
        public int MaxPageSize => 500;

        protected IEnumerable<E> Set() => Context.Set<I, E>();

        protected GenericController(IContext context)
            : base(context)
        {

        }
        protected GenericController(ControllerObject controllerObject)
            : base(controllerObject)
        {
        }


        #region Async-Methods
        public Task<int> CountAsync()
        {
            return Context.CountAsync<I, E>();
        }
        public virtual Task<I> GetByIdAsync(int id)
        {
            return Task.Run<I>(() =>
            {
                var result = default(E);
                var item = Set().SingleOrDefault(i => i.Id == id);

                if (item != null)
                {
                    result = new E();
                    result.CopyProperties(item);
                }
                return result;
            });
        }
        internal virtual Task<IEnumerable<E>> QueryAsync(Func<E, bool> predicate)
        {
            return Task.Run(() => Set().Where(predicate));
        }

        public virtual Task<IEnumerable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            return Task.Run<IEnumerable<I>>(() =>
                Set().Skip(pageIndex * pageSize).Take(pageSize).Select(e =>
                {
                    var result = new E();

                    result.CopyProperties(e);
                    return result;
                }));
        }
        public virtual Task<IEnumerable<I>> GetAllAsync()
        {
            return Task.Run<IEnumerable<I>>(() =>
                Set().Select(e =>
                {
                    var result = new E();

                    result.CopyProperties(e);
                    return result;
                }));
        }
        public virtual Task<I> CreateAsync()
        {
            return Task.Run<I>(() => new E());
        }

        protected virtual Task BeforeInsertingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public virtual Task<I> InsertAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));

            var entityModel = new E();

            entityModel.CopyProperties(entity);
            return InsertAsync(entityModel);
        }
        public virtual async Task<I> InsertAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingAsync(entity);
            var result = await Context.InsertAsync<I, E>(entity);
            await AfterInsertedAsync(result);
            return result;
        }
        protected virtual Task AfterInsertedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeUpdatingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public virtual async Task<I> UpdateAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));

            var entityModel = Set().SingleOrDefault(i => i.Id == entity.Id);

            if (entityModel != null)
            {
                entityModel.CopyProperties(entity);
                var result = await UpdateAsync(entityModel);
                return result;
            }
            else
            {
                throw new Exception("Entity can't find!");
            }
        }
        public virtual async Task<I> UpdateAsync(E entity)
        {
            entity.CheckArgument(nameof(entity));

            await BeforeUpdatingAsync(entity);
            var result = await Context.UpdateAsync<I, E>(entity);
            await AfterUpdatedAsync(entity);
            return result;
        }
        protected virtual Task AfterUpdatedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeDeletingAsync(int id)
        {
            return Task.FromResult(0);
        }
        public async Task DeleteAsync(int id)
        {
            await BeforeDeletingAsync(id);
            var item = await Context.DeleteAsync<I, E>(id);

            if (item != null)
            {
                await AfterDeletedAsync(item);
            }
        }
        protected virtual Task AfterDeletedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveAsync();
        }
        #endregion Async-Methods
    }
}
