using InvoiceSystem.Contracts;
using InvoiceSystem.Contracts.Client;
using InvoiceSystem.Contracts.Persistence;
using InvoiceSystem.Logic.Controllers.Persistence;
using InvoiceSystem.Logic.DataContext;
using InvoiceSystem.Logic.DataContext.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic
{
    public static class Factory
    {
        public enum PersistenceType
        {
            Db,
            //Csv,
            //Ser,
        }
        public static PersistenceType Persistence { get; set; } = Factory.PersistenceType.Db;
        internal static IContext CreateContext()
        {
            IContext result = null;

            if (Persistence == PersistenceType.Db)
            {
                result = new InvoiceSystemDbContext();
            }
            return result;
        }
        public static IControllerAccess<T> Create<T>() where T : IIdentifiable
        {
            IControllerAccess<T> result = null;

            if (typeof(T) == typeof(IInvoiceHead))
            {
                //result = new InvoiceHeadController(CreateContext()) as IControllerAccess<T>;
                result = (IControllerAccess<T>)CreateInvoiceHeadController();

            }
            else if (typeof(T) == typeof(IInvoicePosition))
            {
                //result = new InvoicePositionController(CreateContext()) as IControllerAccess<T>;
                result = (IControllerAccess<T>)CreateInvoicePositionController();
            }

            return result;
        }
        public static IControllerAccess<IInvoiceHead> CreateInvoiceHeadController()
        {
            return new InvoiceHeadController(CreateContext());
        }
        public static IControllerAccess<IInvoicePosition> CreateInvoicePositionController()
        {
            return new InvoicePositionController(CreateContext());
        }
    }
}
