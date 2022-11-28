using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.DataAccess.Repository
{
  
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly EmpDeptContext _db;
            internal DbSet<T> dbSet;
            public Repository(EmpDeptContext db)
            {
                _db = db;
                this.dbSet = _db.Set<T>();
            }
            public void Add(T entity)
            {
                _db.Add(entity);
            }

            public IEnumerable<T> GetAll()
            {
                IQueryable<T> query = dbSet;//select query
                return query.ToList();//select query returns list thats why it's to List
            }
     

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
            {
                IQueryable<T> query = dbSet;
                query = query.Where(filter);
                return query.FirstOrDefault();
            }

            public void Remove(T entity)
            {
                dbSet.Remove(entity);
            }



            public void RemoveRange(IEnumerable<T> entity)
            {
                dbSet.RemoveRange(entity);
            }
        }
    }

