using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Properties

        public DatabaseContext DatabaseContext { get; set; }

        #endregion

        #region Constructors

        public RepositoryBase(DatabaseContext context) => DatabaseContext = context;

        #endregion
        public void Create(T entity)
        {
            DatabaseContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return DatabaseContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DatabaseContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            DatabaseContext.Set<T>().Update(entity);
        }

        public T? GetById(Guid id)
        {
            return DatabaseContext.Set<T>().Find(id);
        }
    }
}
