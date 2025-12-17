using System;
using System.Collections.Generic;
using System.Linq;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Mony;

namespace MoeDeloApi.Services.Repositories
{
    public class OperationRepository : IMoeDeloRepository<OperationResponseDto>
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }
        public IMoeDeloDatabase<OperationResponseDto> Db { get; set; }
        public List<OperationResponseDto> Entities { get; set; }

        public OperationRepository(
            string mainUrl,
            string apiKey,
            ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
            Db = new MoeDeloFileDatabase<OperationResponseDto>(logger);

            // Заполнение Entities данными из Db при инициализации
            Entities = Db.Load();
        }

        public List<OperationResponseDto> Get(string[] args)
        {
            try
            {
                Logger.Log($"Запрос операций с параметрами: {string.Join(", ", args)}");

                if (args == null || args.Length < 2)
                {
                    Logger.Log("Ошибка: недостаточно параметров для фильтрации");
                    return new List<OperationResponseDto>();
                }

                // Парсим даты из параметров
                DateTime startDate = DateTime.Parse(args[0]);
                DateTime endDate = DateTime.Parse(args[1]);

                // Получаем тип операции, если указан
                string operationType = args.Length > 2 ? args[2] : null;

                // Фильтруем сущности
                var filteredEntities = Entities.Where(entity =>
                {
                    // Проверяем, что Date не null и парсим дату операции
                    if (string.IsNullOrEmpty(entity.Date))
                        return false;

                    DateTime operationDate;
                    if (!DateTime.TryParse(entity.Date, out operationDate))
                        return false;

                    // Проверяем попадание в период
                    if (operationDate < startDate || operationDate > endDate)
                        return false;

                    // Если указан тип операции, проверяем соответствие
                    if (!string.IsNullOrEmpty(operationType))
                    {
                        int operationTypeFilter;
                        if (int.TryParse(operationType, out operationTypeFilter))
                        {
                            if (entity.OperationType != operationTypeFilter)
                                return false;
                        }
                    }

                    return true;
                }).ToList();

                Logger.Log($"Найдено {filteredEntities.Count} операций после фильтрации");
                return filteredEntities;
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при выполнении Get: {ex.Message}");
                return new List<OperationResponseDto>();
            }
        }

        public bool Update(string[] args)
        {
            try
            {
                if (args == null || args.Length < 2)
                {
                    Logger.Log("Ошибка: для обновления необходимо указать начало и конец периода");
                    return false;
                }

                DateTime startDate = DateTime.Parse(args[0]);
                DateTime endDate = DateTime.Parse(args[1]);

                Logger.Log($"Обновление операций за период с {startDate:yyyy-MM-dd} по {endDate:yyyy-MM-dd}");

                // Создаем сервис для работы с API
                var moneyService = new MonyService(MainUrl, ApiKey, Logger);

                // Подготавливаем параметры для GetList MonyService: {начало периода, конец периода, "1"}
                string[] serviceArgs =
                {
                    startDate.ToString("yyyy-MM-dd"),
                    endDate.ToString("yyyy-MM-dd"),
                    "1"  // OperationSource = "1" как указано в требованиях
                };

                // Получаем операции через сервис
                var newOperations = moneyService.GetList(serviceArgs);

                // Удаляем старые операции за этот период из Entities
                Entities = Entities.Where(entity =>
                {
                    if (string.IsNullOrEmpty(entity.Date))
                        return true;

                    DateTime operationDate;
                    if (!DateTime.TryParse(entity.Date, out operationDate))
                        return true;

                    return operationDate < startDate || operationDate > endDate;
                }).ToList();

                // Добавляем новые операции
                Entities.AddRange(newOperations);

                Logger.Log($"Обновление завершено. Всего операций в репозитории: {Entities.Count}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при выполнении Update: {ex.Message}");
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                Logger.Log($"Сохранение {Entities.Count} операций в базу данных");
                return Db.Save(Entities);
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при сохранении: {ex.Message}");
                return false;
            }
        }
    }
}