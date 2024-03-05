namespace ReactToDoTask.Server.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T Insert(T item);

        T Update(T item);
        void Delete(int id);

    }
}
