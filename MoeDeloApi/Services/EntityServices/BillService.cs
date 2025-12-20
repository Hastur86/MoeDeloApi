using System;
using System.Collections.Generic;
using System.Net;
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
            List<BillCollectionItemRepresentation> allOperations = new List<BillCollectionItemRepresentation>();
            int currentPage = 1;
            bool hasMorePages = true;
            int maxRetries = 2;
            string numberFilter = "";
            string kontragentId = "";
            string projectId = "";
            DateTime startDate = DateTime.Parse(args[0]);
            DateTime endDate = DateTime.Parse(args[1]);
            if (args.Length >= 5) numberFilter = args[4];
            if (args.Length >= 6) kontragentId = args[5];
            if (args.Length >= 7) projectId = args[6];

            while (hasMorePages)
            {
                Logger.Log(string.Format("Запрос страницы {0}...", currentPage));

                bool success = false;
                int retryCount = 0;
                List<BillCollectionItemRepresentation> operations = new List<BillCollectionItemRepresentation>();

                // Повторные попытки при ошибках
                while (!success && retryCount < maxRetries)
                {
                    try
                    {
                        operations = GetOperationsPage(currentPage, 1000, startDate, endDate, numberFilter, kontragentId, projectId);
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

        private List<BillCollectionItemRepresentation> GetOperationsPage(int pageNo, int pageSize, DateTime startDate, DateTime endDate, string numberFilter, string kontragentId, string projectId)
        {
            try
            {
                IMoeDeloApiGetCommand<BillRepresentationCollection> command
                    = new IMoeDeloApiGetCommand<BillRepresentationCollection>(MainUrl + Urls.GetList, ApiKey, Logger);

                string[] param = { "pageNo", "pageSize", "number", "docAfterDate", "docBeforeDate", "kontragentId", "projectId" };
                string[] arg = { pageNo.ToString(), pageSize.ToString(), numberFilter,
                    startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"),
                    kontragentId, projectId
                };
                return command.Get(param, arg).ResourceList;

            }
            catch (WebException webEx)
            {
                Logger.Log(string.Format("Ошибка при запросе к API: {0}", webEx.Message));
                throw;
            }
        }
    }
    }
}