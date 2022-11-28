using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpDeptProject.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.Department.GetAll();
           
            return View(departments);
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            var category = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
            return View(category);//model binding
        }
        //create get
       
        //create post
        [HttpPost]
        [ValidateAntiForgeryToken]//anyone can not copy your url and login into it 
        //antifogorytoken asp.net
        public IActionResult Create(Department catObj)
        {
            
            if (ModelState.IsValid)//server side validation
            {
                _unitOfWork.Department.Add(catObj);
                _unitOfWork.Save();
                TempData["Success"] = "Category added Successfully";

                return RedirectToAction(nameof(Index));
            }


            return View(catObj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var categoryFirstOrDefault = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
            if (categoryFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(categoryFirstOrDefault);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]//anyone can not copy your url and login into it 
        [ActionName("Edit")] //for routing AN will be this
        //antifogorytoken asp.net
        public IActionResult Edit(Department catObj)
        {
          
            if (ModelState.IsValid)//server side validation
            {
                _unitOfWork.Department.Update(catObj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Edited Successfuly";
                // return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));
            }


            return View(catObj);

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           
            var categoryFromDb = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
     
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
            var obj = _unitOfWork.Department.GetFirstOrDefault(c => c.DeptId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Department.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Department Deleted Successfuly";
            return RedirectToAction(nameof(Index));
        }

    }
}
