using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceSystem.CommonBase.Extensions
{
    public static class ObjectExtension
    {
        public static void CheckArgument(this object source, string argName)
        {
            if (source == null)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}
