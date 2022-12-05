using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository;
using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmpDeptProject.Web.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public readonly EmpDeptContext _db;
        //IOC and DI goes hand and hand
        public EmployeeController(IUnitOfWork unitOfWork, EmpDeptContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }


        public IActionResult Index()
        {
            var allEmployees = _db.Employees.Include(e => e.Departments);
            //var allemployees = _unitOfWork.Employee.GetAll();
            return View(allEmployees);
        }

        //create get
        public IActionResult Create()
        {
            ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            return View();
        }
        //create post
        [HttpPost]
        [ValidateAntiForgeryToken]//anyone can not copy your url and login into it 
       
        public IActionResult Create(Employee empObj)
        {
            
            if (ModelState.IsValid)//server side validation
            {
                _unitOfWork.Employee.Add(empObj);
                _unitOfWork.Save();
                TempData["Success"] = "Category added Successfully";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            return View(empObj);

        }

        public IActionResult Details(int? id)
        {
            var category = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            return View(category);//model binding
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
         
            var categoryFirstOrDefault = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            if (categoryFirstOrDefault == null)
            {
                return NotFound();
            }
            ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            return View(categoryFirstOrDefault);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//anyone can not copy your url and login into it 
        [ActionName("Edit")] //for routing AN will be this
        //antifogorytoken asp.net
        public IActionResult Edit(Employee catObj)
        {
            
            if (ModelState.IsValid)//server side validation
            {
                _unitOfWork.Employee.Update(catObj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Edited Successfuly";
                 ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
          
                // return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DeptId = new SelectList(_unitOfWork.Department.GetAll(), "DeptId", "DeptName");
            return View(catObj);

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
         
            var categoryFromDb = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
      
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Employee.GetFirstOrDefault(c => c.EmpId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Employee.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }

    }
}
