using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public abstract class MoeDeloBase
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }

        protected MoeDeloBase(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }
    }
}