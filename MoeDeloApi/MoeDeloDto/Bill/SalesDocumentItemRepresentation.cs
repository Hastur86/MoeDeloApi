namespace MoeDeloApi.MoeDeloDto.Bill
{
    public class SalesDocumentItemRepresentation
    {
        public float DiscountRate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } 
        public float Count { get; set; }
        public string Unit { get; set; } 
        public int Type { get; set; }
        public int? ActivityAccountCode { get; set; }
        public float Price { get; set; }
        public int NdsType { get; set; }
        public float? SumWithoutNds { get; set; }
        public float? NdsSum { get; set; }
        public float? SumWithNds { get; set; }
        public int StockProductId { get; set; }
        public string Country { get; set; } 
        public string CountryIso { get; set; }
    }
}