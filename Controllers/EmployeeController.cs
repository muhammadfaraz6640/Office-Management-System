using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OMS.Models;
using System.Web.Mvc;
using System.Data.SqlClient;
using OMS.Models.Employees;

namespace OMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeAddView()
        {
            GetEmpAndBranch();
            return View();
        }
        public void GetEmpAndBranch()
        {            
            using (EmpEntities dbEnt = new EmpEntities())
            {
                ViewBag.EmpList = dbEnt.EmployeeTypes.ToList<EmployeeType>();
                ViewBag.BranchList = dbEnt.Branches.ToList<Branch>();
            }
        }

        public ActionResult Employees()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select e.Emp_id as ID,e.Emp_Name as Name,e.Emp_Contact as Contact,e.Emp_Email as Email,e.Emp_Address as Address,e.Emp_Image_Url as Image,e.Emp_Gender as Gender,t.Emp_Type as Type,b.Branch_Name as Branch from Employee e,EmployeeType t, Branch b where e.Bid = b.Bid and e.Emp_Type_id = t.Emp_Type_id", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Employee>();
            while (rd.Read())
            {
                var Temp_Emp = new Employee();
                Temp_Emp.Emp_id = Convert.ToInt32(rd["ID"].ToString());
                Temp_Emp.Emp_Name = rd["Name"].ToString();
                Temp_Emp.Emp_Contact = rd["Contact"].ToString();
                Temp_Emp.Emp_Email = rd["Email"].ToString();
                Temp_Emp.Emp_Gender = rd["Gender"].ToString();
                Temp_Emp.Emp_Image_Url = rd["Image"].ToString();
                Temp_Emp.Emp_Address = rd["Address"].ToString();
                Temp_Emp.Branch_Name = rd["Branch"].ToString();
                Temp_Emp.Type_Name = rd["Type"].ToString();
                model.Add(Temp_Emp);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult EmployeeAdd(Employee emp,HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile.FileName != null)
                {
                    string FilePath = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string FileExtention = Path.GetExtension(ImageFile.FileName);
                    FilePath = FilePath + FileExtention;
                    emp.Emp_Image_Url = "~/EmpImages/" + FilePath;
                    FilePath = Path.Combine(Server.MapPath("~/EmpImages/"), FilePath);
                    ImageFile.SaveAs(FilePath);

                }
                using (var context = new EmpEntities())
                {
                    context.Employees.Add(emp);

                    context.SaveChanges();
                }
                Login NewLogin = new Login();
                NewLogin.email = emp.Emp_Email;
                NewLogin.password = emp.Emp_Password;
                NewLogin.Branch_id = emp.Bid;
                using (var context = new OMSLoginModel())
                {
                    context.Logins.Add(NewLogin);

                    context.SaveChanges();                    
                }
                TempData["msgEmployeeCreation"] = "<script>alert('Successfully Created Employee');</script>";
                return View("EmployeeMessage");
            }
            else
            {
                ViewBag.EmpList = "";
                ViewBag.BranchList = "";
                GetEmpAndBranch();
            }
            return View("EmployeeAddView");
        }
    }
}