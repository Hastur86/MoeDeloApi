using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public abstract class MoeDeloBase
    {
        string MainUrl { get; set; }
        string ApiKey { get; set; }
        ILogger Logger { get; set; }

        protected MoeDeloBase(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }
    }
}