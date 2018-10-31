using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Bill
    {
        public int CreditTimeLimit { get; set; }
        public int InvoiceIdInvoice { get; set; }
        public int PaymentIdPayment { get; set; }
        public int SaleConditionIdCondition { get; set; }
        public string ClientEntityIdEntity { get; set; }
        public string CompanyInformationEntityIdEntity { get; set; }

        public Client ClientEntityIdEntityNavigation { get; set; }
        public CompanyInformation CompanyInformationEntityIdEntityNavigation { get; set; }
        public Invoice InvoiceIdInvoiceNavigation { get; set; }
        public Payment PaymentIdPaymentNavigation { get; set; }
        public SaleCondition SaleConditionIdConditionNavigation { get; set; }
    }
}
