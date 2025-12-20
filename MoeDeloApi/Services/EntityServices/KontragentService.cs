using System;
using System.Collections.Generic;
using System.Net;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Kontragent;

namespace MoeDeloApi.Services
{
    public class KontragentService : IMoeDeloEntity<KontragentDto>
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }

        public KontragentService(string mainUrl, string apiKey, ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
        }
        public KontragentDto Get(string id)
        {
            try
            {
                IMoeDeloApiGetCommand<KontragentDto> command
                    = new IMoeDeloApiGetCommand<KontragentDto>(MainUrl + "/kontragents/api/v1/kontragent", ApiKey, Logger);

                string[] param = { "/" };
                string[] arg = { id };
                return command.Get(param, arg);
            }
            catch (Exception e)
            {
                Logger.Log("Ошибка получения контрагента с {" + id + "} " + e.Message);
                return null;
            }
        }

        public List<KontragentDto> GetList(string[] args)
        {
            List<KontragentDto> allOperations = new List<KontragentDto>();
            int currentPage = 1;
            bool hasMorePages = true;
            int maxRetries = 2;
            string inn = "";
            string name = "";
            if (args.Length >= 1) inn = args[0];
            if (args.Length == 2) name = args[1];

            while (hasMorePages)
            {
                Logger.Log(string.Format("Запрос страницы {0}...", currentPage));

                bool success = false;
                int retryCount = 0;
                List<KontragentDto> operations = new List<KontragentDto>();

                // Повторные попытки при ошибках
                while (!success && retryCount < maxRetries)
                {
                    try
                    {
                        operations = GetOperationsPage(currentPage, 1000, inn, name);
                        success = true;
                    }
                    catch (WebException webEx)
                    {
                        retryCount++;
                        Logger.Log(string.Format("Ошибка WebException (попытка {0}/{1}): {2}",
                            retryCount, maxRetries, webEx.Message));

                        if (retryCount >= maxRetries)
                            throw;

                        // Пауза перед повторной попыткой
                        System.Threading.Thread.Sleep(1000 * retryCount);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Другая ошибка: " + ex.Message);
                        throw;
                    }
                }

                if (operations.Count > 0)
                {
                    allOperations.AddRange(operations);
                    currentPage++;
                }
                else
                {
                    hasMorePages = false;
                }

                // Если получено меньше запрошенного лимита
                if (operations.Count < 1000)
                {
                    hasMorePages = false;
                }

                // Небольшая задержка между запросами
                System.Threading.Thread.Sleep(200);
            }

            Logger.Log(string.Format("Получено {0} операций из API", allOperations.Count));
            return allOperations;
        }

        private List<KontragentDto> GetOperationsPage(int pageNo, int pageSize, string inn, string name)
        {
            try
            {
                IMoeDeloApiGetCommand<KontragentRepresentationCollection> command
                    = new IMoeDeloApiGetCommand<KontragentRepresentationCollection>(MainUrl + "/kontragents/api/v1/kontragent", ApiKey, Logger);

                string[] param = { "pageNo", "pageSize", "inn", "name" };
                string[] arg = { pageNo.ToString(), pageSize.ToString(), inn, name };
                return command.Get(param, arg).ResourceList;

            }
            catch (WebException webEx)
            {
                Logger.Log(string.Format("Ошибка при запросе к API: {0}", webEx.Message));
                throw;
            }
        }

        public bool Set(KontragentDto entity)
        {
            throw new System.NotImplementedException();
        }
    }
}