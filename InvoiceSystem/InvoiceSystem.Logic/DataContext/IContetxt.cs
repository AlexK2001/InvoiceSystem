﻿using InvoiceSystem.Contracts;
using InvoiceSystem.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Logic.DataContext
{
    internal interface IContext: IDisposable
    {
        IEnumerable<E> Set<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        #region Async-Methods
        Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> InsertAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> UpdateAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityObject, I;

        Task SaveAsync();
        #endregion Async-Methods
    }
}
