using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeTeam.Patterns.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        T Get(T entity);
        List<T> GetAllBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        T Add(T entity);
        bool Update(T entity, T dbEntity);
        bool Delete(T entity);
        bool DeleteById(int id);
        IQueryable<T> GetAllNoConsolidate();
        IQueryable<T> GetFromSQL(string sql);
        List<dynamic> GetFromSQLToDynamic(string sql);
        void InsertFromSQL(string sql);
        Task SaveChangesAsync();
    }
}
