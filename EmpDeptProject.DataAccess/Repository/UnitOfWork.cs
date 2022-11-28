using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork

    {
        private EmpDeptContext _db;

        public UnitOfWork(EmpDeptContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            Department = new DepartmentRepository(_db);
        }
       

        public IEmployeeRepository Employee { get; private set; }

        public IDepartmentRepository Department { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
