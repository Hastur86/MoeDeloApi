namespace MoeDeloApi.DTO.UrlDto
{
    public class MdUrlBills : MdUrlBase
    {
        public string LinkedDocuments = "/accounting/api/v1/sales/bill/{id}/LinkedDocuments";

        public MdUrlBills()
        {
            GetById = "/accounting/api/v1/sales/bill";
            GetList = "/accounting/api/v1/sales/bill";
            SetById = "/accounting/api/v1/sales/bill";
            GetEditList = "/accounting/api/v1/sales/bill/byIds";
        }
    }
}