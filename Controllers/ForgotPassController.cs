using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;

namespace OMS.Controllers
{
    public class ForgotPassController : Controller
    {
        // GET: ForgotPass
        public ActionResult Index()
        {
            return View("ForgotPass");  //view
        }


        [HttpPost]
        
        public ActionResult GetPass(ForgetPass fp)
        {
            try
            {
                Models.Email ee = new Models.Email();
                Models.Connection con = new Models.Connection();
                SqlCommand com = new SqlCommand("select email,password from Login", con.getConnection());
                SqlDataReader rd = com.ExecuteReader();
                while (rd.Read())
                {
                    ee.email = fp.Email;
                    string em = rd["email"].ToString();
                    string pass = rd["password"].ToString();
                    if (ee.email == em)
                    {
                        ee.Body = pass;
                        ee.Subject = "Password Recovery";             
                        ee.SendEmail(ee);                        
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");                
            }
            return RedirectToAction("Index", "ForgotPass");
        }
    }
}