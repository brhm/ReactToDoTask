﻿namespace ReactToDoTask.Server.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T item);

        void Update(T item);
        void Delete(int id);

    }
}
