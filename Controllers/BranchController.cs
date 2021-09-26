using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;
using OMS.Models.Branches;

namespace OMS.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public ActionResult Index()
        {

            GetCityAndCountry();
            return View("BranchView");
        }


        public ActionResult Branches()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select b.Bid as ID,b.Branch_Address as Address,b.Branch_Contact as Contact,b.Branch_Name as Name,b.Branch_Type as Type,c.City_Name as City,co.Country_Name as Country from Branch b,City c, Country co where b.City_id = c.City_id and b.Country_id = co.Country_id", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Models.Branches.Branch>();
            while (rd.Read())
            {
                var Temp_Br = new Models.Branches.Branch();
                Temp_Br.Bid = Convert.ToInt32(rd["ID"].ToString());
                Temp_Br.Branch_Name = rd["Name"].ToString();
                Temp_Br.Branch_Contact = rd["Contact"].ToString();                
                Temp_Br.CountryName = rd["Country"].ToString();                
                Temp_Br.Branch_Address = rd["Address"].ToString();
                Temp_Br.CItyName = rd["City"].ToString();
                Temp_Br.Branch_Type = rd["Type"].ToString();
                model.Add(Temp_Br);
            }

            return View(model);
        }


        public void GetCityAndCountry()
        {
            Models.Branches.Branch NewBranch = new Models.Branches.Branch();
            var TypeList = new List<string>() { "Sub" };
            ViewBag.TypeList = TypeList;
            using (BranchEntities1 dbEnt = new BranchEntities1())
            {
                ViewBag.CityList = dbEnt.Cities.ToList<City>();
                ViewBag.CountryList = dbEnt.Countries.ToList<Country>();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(Models.Branches.Branch NewBranch)
        {           
                if (ModelState.IsValid)
            {
                using (var context = new BranchEntities1())
                {
                    context.Branches.Add(NewBranch);

                    context.SaveChanges();
                    TempData["msgBranchCreation"] = "<script>alert('Successfully Created Branch');</script>";
                }
                return View("Branch");
            }
            else
            {
                ViewBag.TypeList = "";
                ViewBag.CityList = "";
                ViewBag.CountryList = "";
                GetCityAndCountry();
            }            
            return View("BranchView");
        }
    }
}