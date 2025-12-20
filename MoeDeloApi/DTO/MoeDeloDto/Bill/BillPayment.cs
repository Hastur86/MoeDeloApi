namespace MoeDeloApi.MoeDeloDto.Bill
{
    public class BillPayment
    {
        public string Number { get; set; } 
        public string Date { get; set; } 
        public float? Sum { get; set; }
        public int? Id { get; set; }
    }
}