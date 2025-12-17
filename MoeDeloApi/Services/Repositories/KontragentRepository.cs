using System;
using System.Collections.Generic;
using System.Linq;
using MoeDeloApi.Logger;
using MoeDeloApi.MoeDeloDto.Kontragent;

namespace MoeDeloApi.Services.Repositories
{
    public class KontragentRepository : IMoeDeloRepository<KontragentDto>
    {
        public string MainUrl { get; set; }
        public string ApiKey { get; set; }
        public ILogger Logger { get; set; }
        public IMoeDeloDatabase<KontragentDto> Db { get; set; }
        public List<KontragentDto> Entities { get; set; }

        public KontragentRepository(
            string mainUrl,
            string apiKey,
            ILogger logger)
        {
            MainUrl = mainUrl;
            ApiKey = apiKey;
            Logger = logger;
            Db = new MoeDeloFileDatabase<KontragentDto>(logger);

            // Заполнение Entities данными из Db при инициализации
            Entities = Db.Load();
        }

        public List<KontragentDto> Get(string[] args)
        {
            try
            {
                Logger.Log($"Запрос контрагентов с параметрами: {string.Join(", ", args)}");

                if (args == null || args.Length < 2)
                {
                    Logger.Log("Ошибка: недостаточно параметров для фильтрации");
                    return new List<KontragentDto>();
                }

                // Получаем тип операции, если указан
                string inn = args.Length >= 1 ? args[0] : null;
                string name = args.Length >= 2 ? args[1] : null;

                // Фильтруем сущности
                var filteredEntities = Entities.Where(entity =>
                {
                    // Если указан ИНН, проверяем соответствие
                    if (!string.IsNullOrEmpty(inn))
                    {
                        if (entity.Inn != inn)
                            return false;
                    }
                    // Если указано имя, проверяем соответствие
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (!(entity.Name.ToLower().Contains(name.ToLower())) && !(entity.ShortName.ToLower().Contains(name.ToLower())))
                            return false;
                    }

                    return true;
                }).ToList();

                Logger.Log($"Найдено {filteredEntities.Count} клиентов после фильтрации");
                return filteredEntities;
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при выполнении Get: {ex.Message}");
                return new List<KontragentDto>();
            }
        }

        public bool Update(string[] args)
        {
            try
            {
                Logger.Log("Обновляем список контрагентов");

                // Создаем сервис для работы с API
                var kontragentService = new KontragentService(MainUrl, ApiKey, Logger);

                // Получаем операции через сервис
                var newKontragents = kontragentService.GetList(args);

                // Очищаем список клиентов
                Entities.Clear();
                Entities.AddRange(newKontragents);

                Logger.Log($"Обновление завершено. Всего клиентов в репозитории: {Entities.Count}");
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