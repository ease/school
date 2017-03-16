using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Database
{
    public class Invoice : Basic
    {
        public Invoice()
        {
            Items = new List<Item>();
        }

        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime ShippedOn { get; set; }
        public Status Status { get; set; }
        [NotMapped]
        public double SubTotal
        {
            get
            {
                double sum = 0;
                foreach (Item item in Items) sum += item.SubTotal;
                return Math.Round(sum, 2);
            }
        }
        public double Vat { get; set; }
        [NotMapped]
        public double VatAmount { get { return Math.Round(SubTotal * Vat / 100, 2); } }
        public double Shipping { get; set; }
        [NotMapped]
        public double Total { get { return Math.Round(SubTotal + VatAmount + Shipping, 2); } }

        [Required]
        public virtual Agent Agent { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }
        [Required]
        public virtual Shipper Shipper { get; set; }

        public virtual List<Item> Items { get; set; }
    }
}
