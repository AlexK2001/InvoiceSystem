using InvoiceSystem.Logic;
using System;

namespace InvoiceSystem.ConApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("InvoiceSystem");

            using var ctrlHeads = Factory.CreateInvoiceHeadController();
            using var ctrlPositions = Factory.CreateInvoicePositionController();

            var head = await ctrlHeads.CreateAsync();
            head.Text = "EDV-Ausstattung";
            head.City = "Leonding";
            head.ZipCode = "4060";
            head.Street = "Limesstraße 12-14";
            head.Date = DateTime.Parse("13.03.2020");

            head = await ctrlHeads.InsertAsync(head);
            await ctrlHeads.SaveChangesAsync();

            var pos = await ctrlPositions.CreateAsync();
            pos.Order = 1;
            pos.Text = "PC-Systeme";
            pos.Quantity = 25;
            pos.Price = 4798.0;
            pos.Tax = 20;
            pos.InvoiceHeadId = head.Id;
            await ctrlPositions.InsertAsync(pos);

            pos = await ctrlPositions.CreateAsync();
            pos.Order = 2;
            pos.Text = "PC-Systeme";
            pos.Quantity = 45;
            pos.Price = 4399.0;
            pos.Tax = 24;
            pos.InvoiceHeadId = head.Id;

            await ctrlPositions.InsertAsync(pos);
            await ctrlPositions.SaveChangesAsync();
        }
    }
}
