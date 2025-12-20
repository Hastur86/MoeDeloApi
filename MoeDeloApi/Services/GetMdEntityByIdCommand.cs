using System;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public class GetMdEntityByIdCommand<T> : MoeDeloBase
    {
        private string GetUrl { get; set; }

        public GetMdEntityByIdCommand(string mainUrl, string apiKey, ILogger logger, string getUrl) : base(mainUrl,
            apiKey, logger)
        {
            GetUrl = getUrl;
        }

        public T Get(string id)
        {
            try
            {
                IMoeDeloApiGetCommand<T> command
                    = new IMoeDeloApiGetCommand<T>(MainUrl + GetUrl, ApiKey, Logger);

                string[] param = { "/" };
                string[] arg = { id };
                return command.Get(param, arg);
            }
            catch (Exception e)
            {
                Logger.Log($"Ошибка получения "+ typeof(T).Name +" с " + id + " - " + e.Message);
                throw;
            }
        }
    }
}