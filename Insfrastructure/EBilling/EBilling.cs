using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.EBilling
{
    public class EBilling
    {
        public string Id { get; set; }
        public string CurrentId { get; set; }
        public string CurrentName { get; set; }
        public DateTime Date { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal ClearPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalPrice { get; set; }

        public EBilling()
        {
            Id = string.Empty;
            CurrentId = string.Empty;
            CurrentName = string.Empty;
            DepartmentId = string.Empty;
            DepartmentName = string.Empty;
        }

        public EBilling(string ıd, string currentId, string currentName, DateTime date, string departmentId, string departmentName, decimal price, decimal discount, decimal clearPrice, decimal vat, decimal totalPrice)
        {
            Id = ıd;
            CurrentId = currentId;
            CurrentName = currentName;
            Date = date;
            DepartmentId = departmentId;
            DepartmentName = departmentName;
            Price = price;
            Discount = discount;
            ClearPrice = clearPrice;
            Vat = vat;
            TotalPrice = totalPrice;
        }
    }
}
