using Insfrastructure.EBillingUBLTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.EBilling
{
    public class EBillingHelper
    {

        public async Task CreateEBilling(EBilling eBilling, List<BillingDetail> billingDetails)
        {
            var invoice = new InvoiceType
            {
                UBLVersionID = new UBLVersionIDType { Value= "2.1"},
                CustomizationID = new CustomizationIDType { Value = "TR1.2"},
                ProfileID =  new ProfileIDType { Value = "EARSIVFATURA" },
                ID = new IDType { Value = eBilling.Id},
                CopyIndicator = new CopyIndicatorType { Value = false },
                UUID = new UUIDType { Value = Guid.NewGuid().ToString() },
                IssueDate = new IssueDateType { Value = eBilling.Date },
                IssueTime = new IssueTimeType { Value = eBilling.Date},
                InvoiceTypeCode = new InvoiceTypeCodeType { Value = "SATIS"},
                Note = new [] {new NoteType
                {
                    Value = "Bu Notu Ben Yazdım Aga 28. Satırdayım!"
                } },
                DocumentCurrencyCode = new DocumentCurrencyCodeType { Value = "TRY"},
                LineCountNumeric = new LineCountNumericType { Value = billingDetails.Count},

            };
        }

    }
}
