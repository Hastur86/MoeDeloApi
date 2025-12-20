using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Mony;

namespace MoeDeloApi.Services
{
    public class MonyService : IMoeDeloEntity<OperationResponseDto>
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }

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
            List<OperationResponseDto> allOperations = new List<OperationResponseDto>();
            int currentPage = 1;
            bool hasMorePages = true;
            int maxRetries = 2;
            DateTime startDate = DateTime.Parse(args[0]);
            DateTime endDate = DateTime.Parse(args[1]);
            string operationSource = args[2];
            string operationTypes = "";
            if(args.Length == 4) operationTypes = args[3];

            while (hasMorePages)
            {
                Logger.Log(string.Format("Запрос страницы {0}...", currentPage));

                bool success = false;
                int retryCount = 0;
                List<OperationResponseDto> operations = new List<OperationResponseDto>();

                // Повторные попытки при ошибках
                while (!success && retryCount < maxRetries)
                {
                    try
                    {
                        operations = GetOperationsPage(startDate, endDate, operationSource, operationTypes, currentPage, 1000, (currentPage - 1) * 1000);
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

        private List<OperationResponseDto> GetOperationsPage(DateTime startDate, DateTime endDate, string operationSource, string operationTypes, int page, int limit, int offset)
        {
            try
            {
                IMoeDeloApiGetCommand<ApiPageResponseDto<OperationResponseDto>> command
                    = new IMoeDeloApiGetCommand<ApiPageResponseDto<OperationResponseDto>>(MainUrl + "/money/api/v1/Registry", ApiKey, Logger);

                string[] param = { "Offset", "Limit", "StartDate", "EndDate", "OperationSource", "OperationTypes" };
                string[] arg = { offset.ToString(), limit.ToString(), startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), operationSource, operationTypes };
                return command.Get(param, arg).Data;

            }
            catch (WebException webEx)
            {
                Logger.Log(string.Format("Ошибка при запросе к API: {0}", webEx.Message));
                throw;
            }
        }


        public bool Set(OperationResponseDto entity)
        {
            return false;
        }
    }
}