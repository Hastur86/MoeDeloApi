using System.Collections.Generic;

namespace MoeDeloApi.MoeDeloDto.Bill
{
    public class BillRepresentationCollection
    {
        public int? Count { get; set; }
        public List<BillCollectionItemRepresentation> ResourceList { get; set; }
        public int? TotalCount { get; set; }
    }
}