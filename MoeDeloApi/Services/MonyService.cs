using System;
using System.Collections.Generic;
using System.Threading;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Mony;

namespace MoeDeloApi.Services
{
    public class MonyService : IMoeDeloEntity<OperationResponseDto>
    {
        private string MainUrl = "";
        private string ApiKey = "";
        private ILogger Logger;

        public MonyService(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }

        public OperationResponseDto Get(string id)
        {
            try
            {
                IMoeDeloApiGetCommand<OperationResponseDto> command
                    = new IMoeDeloApiGetCommand<OperationResponseDto>(MainUrl + "/money/api/v1/PaymentOrders", ApiKey, Logger);

                string[] param = { "/" };
                string[] arg = { id };
                return command.Get(param, arg);
            }
            catch (Exception e)
            {
                Logger.Log("Ошибка получения операции с {"+id+"} "+ e.Message);
                return null;
            }
        }

        public List<OperationResponseDto> GetList(string[] args)
        {
            throw new System.NotImplementedException();
        }

        public bool Set(OperationResponseDto entity)
        {
            return false;
        }
    }
}