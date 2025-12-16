using System.Collections.Generic;

namespace MoeDeloApi.Services
{
    public interface IMoeDeloEntity<T>
    {
        T Get(string id);
        List<T> GetList(string[] args);
        bool Set(T entity);
    }
}