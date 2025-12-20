using MoeDeloApi.DTO.UrlDto;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public abstract class MoeDeloEntityBase<T> : MoeDeloBase
    {
        private MdUrlBase MainUrls;
        private GetMdEntityByIdCommand<T> GetMethot { get; set; }

        public MoeDeloEntityBase(string mainUrl, string apiKey, ILogger logger, MdUrlBase urls) : base(mainUrl, apiKey, logger)
        {
            MainUrls = urls;
            GetMethot = new GetMdEntityByIdCommand<T>(MainUrl, ApiKey, Logger, MainUrls.GetById);
        }

        public T Get(string id)
        {
            return GetMethot.Get(id);
        }

        public bool Set(T entity)
        {
            return false;
        }
    }
}