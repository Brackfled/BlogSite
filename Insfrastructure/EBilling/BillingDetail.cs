using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.EBilling
{
    public class BillingDetail
    {
        public string StockId { get; set; }
        public string StockName { get; set; }
        public int Amount { get; set; }
        public double VatRate { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalUnitPrice { get; set; }
        public double DiscountRate { get; set; }
        public decimal ClearPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public BillingDetail()
        {
            StockId = string.Empty;
            StockName = string.Empty;
            Unit = string.Empty;
        }

        public BillingDetail(string stockId, string stockName, int amount, double vatRate, string unit, decimal unitPrice, decimal vat, decimal totalUnitPrice, double discountRate, decimal clearPrice, decimal totalPrice)
        {
            StockId = stockId;
            StockName = stockName;
            Amount = amount;
            VatRate = vatRate;
            Unit = unit;
            UnitPrice = unitPrice;
            Vat = vat;
            TotalUnitPrice = totalUnitPrice;
            DiscountRate = discountRate;
            ClearPrice = clearPrice;
            TotalPrice = totalPrice;
        }
    }
}
