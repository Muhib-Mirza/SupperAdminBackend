using System.Data;

namespace APL.API.Extensions.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">DM</typeparam>
    /// <typeparam name="K">VM</typeparam>
    public interface IRepository<T> where T: class 
    {

        string GetSQLSelect();
        T Add(T input, IDbConnection conn, IDbTransaction transaction);

        T Edit(T input, IDbConnection conn, IDbTransaction transaction);


        bool AddList(IEnumerable<T> input, IDbConnection conn, IDbTransaction transaction);

        T Get(T input, IDbConnection conn, IDbTransaction transaction);

        List<T> GetAll(T input, IDbConnection conn, IDbTransaction transaction);


        bool Remove(T input, IDbConnection conn, IDbTransaction transaction);

        int MarkDelete(T input, IDbConnection conn, IDbTransaction transaction);


    }
}
