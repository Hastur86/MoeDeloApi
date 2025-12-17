using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using MoeDeloApi.Logger;

namespace MoeDeloApi.Services
{
    public class MoeDeloFileDatabase<T> : IMoeDeloDatabase<T>
    {
        private ILogger _logger;
        private readonly string _dataDirectory;
        private readonly string _filePath;

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public MoeDeloFileDatabase(ILogger logger = null)
        {
            _logger = logger;

            // Определяем путь к папке проекта
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _dataDirectory = Path.Combine(projectDirectory, "MdDataBase");

            // Формируем имя файла на основе имени типа
            string typeName = typeof(T).Name;
            string fileName = $"{typeName}List.data";
            _filePath = Path.Combine(_dataDirectory, fileName);

            LogInfo($"Инициализация базы данных для типа {typeName}");
            LogInfo($"Путь к файлу данных: {_filePath}");
        }

        public List<T> Load()
        {
            try
            {
                LogInfo($"Начало загрузки данных из файла: {_filePath}");

                // Проверяем существование файла
                if (!File.Exists(_filePath))
                {
                    LogWarning($"Файл {_filePath} не найден. Возвращается пустой список.");
                    return new List<T>();
                }

                // Создаем папку если её нет
                EnsureDirectoryExists();

                // Загружаем данные из файла
                using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    List<T> data = (List<T>)formatter.Deserialize(fs);

                    LogInfo($"Успешно загружено {data.Count} записей из файла: {_filePath}");
                    return data;
                }
            }
            catch (Exception ex)
            {
                LogError($"Ошибка при загрузке данных из файла {_filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        public bool Save(List<T> data)
        {
            try
            {
                if (data == null)
                {
                    LogWarning("Попытка сохранить null список. Данные не сохранены.");
                    return false;
                }

                LogInfo($"Начало сохранения {data.Count} записей в файл: {_filePath}");

                // Создаем папку если её нет
                EnsureDirectoryExists();

                // Сохраняем данные в файл
                using (FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, data);
                }

                LogInfo($"Успешно сохранено {data.Count} записей в файл: {_filePath}");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"Ошибка при сохранении данных в файл {_filePath}: {ex.Message}");
                return false;
            }
        }

        private void EnsureDirectoryExists()
        {
            try
            {
                if (!Directory.Exists(_dataDirectory))
                {
                    LogInfo($"Создание директории: {_dataDirectory}");
                    Directory.CreateDirectory(_dataDirectory);
                    LogInfo($"Директория успешно создана: {_dataDirectory}");
                }
            }
            catch (Exception ex)
            {
                LogError($"Ошибка при создании директории {_dataDirectory}: {ex.Message}");
                throw;
            }
        }

        private void LogInfo(string message)
        {
            if (_logger != null)
            {
                _logger.Log($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        private void LogWarning(string message)
        {
            if (_logger != null)
            {
                _logger.Log($"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        private void LogError(string message)
        {
            if (_logger != null)
            {
                _logger.Log($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
    }
}