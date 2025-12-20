using System.Collections.Generic;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Bill;

namespace MoeDeloApi.Services
{
    public class BillService : IMoeDeloEntity<BillCollectionItemRepresentation>
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }

        public BillService(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }

        public BillRepresentation Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<BillCollectionItemRepresentation> GetList(string[] args)
        {
            throw new System.NotImplementedException();
        }

        public bool Set(BillCollectionItemRepresentation entity)
        {
            throw new System.NotImplementedException();
        }
    }
}