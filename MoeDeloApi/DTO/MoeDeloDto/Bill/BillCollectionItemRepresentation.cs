namespace MoeDeloApi.MoeDeloDto.Bill
{
    public class BillCollectionItemRepresentation
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string DocDate { get; set; } = string.Empty;
        public int? Type { get; set; }
        public int? Status { get; set; }
        public int? KontragentId { get; set; }
        public SettlementAccountModel SettlementAccount { get; set; }
        public int? ProjectId { get; set; }
        public int? StockId { get; set; }
        public string DeadLine { get; set; }
        public string AdditionalInfo { get; set; }
        public string ContractSubject { get; set; }
        public int? NdsPositionType { get; set; }
        public bool? IsCovered { get; set; }
        public float? Sum { get; set; }
        public float? PaidSum { get; set; }
        public string Comment { get; set; }
    }
}