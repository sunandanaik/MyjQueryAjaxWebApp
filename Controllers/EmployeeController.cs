using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MyjQueryAjaxWebApp.Models;
using System.Data.Entity;

namespace MyjQueryAjaxWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        //To return all employee records
        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }

        //Function to Return list of employee collections
        IEnumerable<Employee> GetAllEmployee()
        {
            using(jQueryAjaxDBEntities db = new jQueryAjaxDBEntities())
            {
                return db.Employee.ToList<Employee>();
            }
        }
        //Before Inserting data into Db, Get data for edit.
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if (id == 0)
            {
                Employee emp = new Employee();
                return View(emp);
            }
            else
            {
                using (jQueryAjaxDBEntities db = new jQueryAjaxDBEntities())
                {
                    return View(db.Employee.Where(e => e.EmployeeID == id).FirstOrDefault<Employee>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {
            try
            {
                //Check if file is selected for upload
                if (emp.ImageUpload != null)
                {
                    //Retrieve the uploaded file path
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImageUpload.FileName);
                    string extension = Path.GetExtension(emp.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //Now to Save the image into Database field of ImagePath.
                    emp.ImagePath = "~/AppFiles/Images/" + fileName;
                    //Now we need to save this image into images folder
                    emp.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }
                using (jQueryAjaxDBEntities db = new jQueryAjaxDBEntities())
                {
                    if(emp.EmployeeID == 0)
                    {
                        db.Employee.Add(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Record Submitted Successfully !!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Record Updated Successfully !!" }, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                //return RedirectToAction("ViewAll");
                //return Json(new { success = true, html = GlobalClass.RenderViewToString(this, "ViewAll", GetAllEmployee()), message = "Record Added Successfully !!" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (jQueryAjaxDBEntities db = new jQueryAjaxDBEntities())
                {
                    Employee emp = db.Employee.Where(e => e.EmployeeID == id).FirstOrDefault<Employee>();
                    db.Employee.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, message = "Record Deleted Successfully !!" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}