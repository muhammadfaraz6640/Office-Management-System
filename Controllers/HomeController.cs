using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;

namespace OMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()   //login page
        {            
            return View();                        
        }

        public ActionResult HomeMain()   //home page main
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l)
        {
            OMSLoginModel db = new OMSLoginModel();
            Connection con = new Connection();
            if (ModelState.IsValid)
            {
                con.getConnection();
                bool found = false;
                SqlCommand com = new SqlCommand("select Emp_id,Emp_Name, Emp_Image_Url,Emp_Email,Bid from Employee where Emp_Email = '" + l.email + "' and Emp_Password = '" + l.password + "'", con.getConnection());
                SqlDataReader rd = com.ExecuteReader();
                while(rd.Read())
                {
                    Session["UName"] = rd["Emp_Name"].ToString();
                    Session["Empid"] = rd["Emp_id"].ToString();
                    Session["UImg"] = rd["Emp_Image_Url"].ToString();
                    Session["Email"] = rd["Emp_Email"].ToString();
                    Session["Bid"] = rd["Bid"].ToString();
                    TempData["msg"] = "<script>alert('Successfully Login');</script>";
                    found = true;
                    return RedirectToAction("Index", "Main");  //home page redirect
                }

                if(found == false)
                {
                    TempData["msg"] = "<script>alert('Login Failed');</script>";
                }
                
                //else
                //{
                //    TempData["msg"] = "<script>alert('Login Failed');</script>";
                //    return RedirectToAction("Index");
                //}
                //var data = db.Logins.Where(Model => Model.email.Equals(l.email) && Model.password.Equals(l.password)).ToList();
                //if (data.Count() > 0)
                //{
                //    Connection con = new Connection();
                //    con.getConnection();
                //    SqlCommand com = new SqlCommand("select Emp_Name,Emp_Image_Url from Employee where Emp_Email='" + l.email + "' and Emp_Password = '" + l.password + "'", con.getConnection());
                //    SqlDataReader rd = com.ExecuteReader();
                //    while (rd.Read())
                //    {
                //        Session["UName"] = rd["Emp_Name"].ToString();
                //        Session["UImg"] = rd["Emp_Image_Url"].ToString();
                //    }
                //    Session["Email"] = data.FirstOrDefault().email.ToString();
                //    Session["Bid"] = Convert.ToInt32( data.FirstOrDefault().Branch_id);
                //    Session["BName"] = Models.Login.GetBranchName(Convert.ToInt32(data.FirstOrDefault().Branch_id));
                //    TempData["msg"] = "<script>alert('Successfully Login');</script>";

                //    return RedirectToAction("Index","Main");  //home page redirect
                //}
                //else
                //{                    
                //    TempData["msg"] = "<script>alert('Login Failed');</script>";
                //    return RedirectToAction("Index");
                //}
            }
            return View("Index");
        }
        
        public ActionResult Logout()
        {
            Session["Email"] = null;
            Session["UName"] =null;
            Session["UImg"] = null;
            Session["Empid"] = null;
            Session["Bid"] = null;
            Session["BName"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}
