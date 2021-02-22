using InvoiceSystem.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.Logic.Entities
{
    internal class IdentityObject: IIdentifiable
    {
        public int Id { get; set; }
    }
}
