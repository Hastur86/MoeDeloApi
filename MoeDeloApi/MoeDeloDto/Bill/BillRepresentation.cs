using System.Collections.Generic;

namespace MoeDeloApi.MoeDeloDto.Bill
{
    public class BillRepresentation : BillCollectionItemRepresentation
    {
        public List<SalesDocumentItemRepresentation> Items { get; set; }
        public string Online { get; set; }
        public Context Context { get; set; }
        public List<BillPayment> Payments { get; set; }
        public bool? UseStampAndSign { get; set; }
    }
}