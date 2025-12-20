namespace MoeDeloApi.Services
{
    public interface IMoeDeloEntity<T>
    {
        T Get(string id);
        bool Set(T entity);
    }
}