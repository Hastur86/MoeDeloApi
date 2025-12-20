using System.Collections.Generic;
using MoeDeloApi.DTO.UrlDto;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Bill;

namespace MoeDeloApi.Services
{
    public class BillService : MoeDeloEntityBase<BillRepresentation>, IMoeDeloListEntity<BillCollectionItemRepresentation>
    {
        private MdUrlBills Urls;

        private GetMdEntityByIdCommand<BillRepresentation> GetMethot { get; set; }

        public BillService(string mainUrl, string apiKey, ILogger logger, MdUrlBills urls) : base(mainUrl, apiKey, logger, urls)
        {
            Urls = urls;
        }

        public List<BillCollectionItemRepresentation> GetList(string[] args)
        {
            
        }
    }
}