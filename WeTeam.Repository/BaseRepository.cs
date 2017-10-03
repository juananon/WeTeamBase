using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTeam.Repository
{
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq.Expressions;
    using WeTeam.Patterns.UnitOfWork;

    public class BaseRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<T> GetAll()
        {
            try
            {
                return _unitOfWork.ContextContainer.Context.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public IQueryable<T> GetAllNoConsolidate()
        {
            try
            {
                return _unitOfWork.ContextContainer.Context.Set<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _unitOfWork.ContextContainer.Context.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public T Get(T entity)
        {
            try
            {
                return _unitOfWork.ContextContainer.Context.Set<T>().Find(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public List<T> GetAllBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {

                return _unitOfWork.ContextContainer.Context.Set<T>().Where(predicate).ToList();

            }
            else
            {
                throw new ArgumentNullException("Predicate value must be passed to FindAllBy<T>.");
            }
        }

        public IQueryable<T> GetFromSQL(string sql)
        {
            return _unitOfWork.ContextContainer.Context.Database.SqlQuery<T>(sql).AsQueryable();
        }

        public void InsertFromSQL(string sql)
        {
            var result = _unitOfWork.ContextContainer.Context.Database.ExecuteSqlCommand(sql);
        }


        public List<dynamic> GetFromSQLToDynamic(string sql)
        {
            return _unitOfWork.ContextContainer.Context.Database.SqlQuery<dynamic>(sql).ToList();
        }

        public T Add(T entity)
        {
            try
            {

                T ent = _unitOfWork.ContextContainer.Context.Set<T>().Add(entity);
                return ent;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public bool Update(T entity, T dbEntity)
        {
            try
            {
                var _entity = _unitOfWork.ContextContainer.Context.Entry(entity);
                var originalValues = _unitOfWork.ContextContainer.Context.Entry(dbEntity).OriginalValues;
                var currentValues = _unitOfWork.ContextContainer.Context.Entry(dbEntity).CurrentValues;
                foreach (var property in _unitOfWork.ContextContainer.Context.Entry(dbEntity).OriginalValues.PropertyNames)
                {
                    var original = originalValues.GetValue<object>(property);
                    var current = currentValues.GetValue<object>(property);
                    if (original != null && !original.Equals(current))
                    {
                        _unitOfWork.ContextContainer.Context.Entry(dbEntity).Property(property).IsModified = true;
                    }

                }
                _unitOfWork.ContextContainer.Context.Entry(dbEntity).CurrentValues.SetValues(entity);
                _unitOfWork.ContextContainer.Context.Set<T>().Attach(dbEntity);
                _unitOfWork.ContextContainer.Context.Entry(dbEntity).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _unitOfWork.ContextContainer.Context.Set<T>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                var entityToRemove = GetById(id);
                _unitOfWork.ContextContainer.Context.Set<T>().Remove(entityToRemove);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.ContextContainer.Context.SaveChangesAsync();
        }
    }
}
