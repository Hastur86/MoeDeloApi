namespace MoeDeloApi.MoeDeloDto.Mony
{
    public class OperationResponseDto
    {
        public ContractorDto Contractor { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int? Direction { get; set; }
        public int? DocumentBaseId { get; set; }
        public bool? IsMainContractor { get; set; }
        public bool? IsPaid { get; set; }
        public string KontragentSettlementAccountId { get; set; }
        public string ModifyDate { get; set; }
        public NdsDto Nds { get; set; }
        public string Number { get; set; }
        public int? OperationType { get; set; }
        public string PatentId { get; set; }
        public SourceDto Source { get; set; }
        public float? Sum { get; set; }
        public string TaxationSystemType { get; set; }
    }
}