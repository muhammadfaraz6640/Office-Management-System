using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Models;
using OMS.Models.Orders;


namespace OMS.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderView()
        {
            GetProducts();
            return View();
        }


        public void GetProducts()
        {
            using (OrderEntities dbEnt = new OrderEntities())
            {
                ViewBag.ProductList = dbEnt.Products1.ToList<Products>();                
            }
        }
        
            public ActionResult OrdersAll()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select o.Order_id as OID,p.Pid as PID,p.Product_Name as P_Name,e.Emp_id as EID,e.Emp_Name as E_Name,b.Bid as BID,b.Branch_Name as B_Name,o.Qty as Qty from Orders o,Products p,Branch b,Employee e where p.Pid = o.product_id and b.Bid = o.branch_id and e.Emp_id = o.Emp_id", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Order>();
            while (rd.Read())
            {
                var Temp_Ord = new Order();
                Temp_Ord.Order_id = Convert.ToInt32(rd["OID"].ToString());
                Temp_Ord.Product_id = Convert.ToInt32(rd["PID"].ToString());
                Temp_Ord.Product_Name = rd["P_Name"].ToString();
                Temp_Ord.Emp_id = Convert.ToInt32(rd["EID"].ToString());
                Temp_Ord.Emp_Name = rd["E_Name"].ToString();
                Temp_Ord.Branch_id = Convert.ToInt32(rd["BID"].ToString());
                Temp_Ord.Branch_Name = rd["B_Name"].ToString();
                Temp_Ord.Qty = Convert.ToInt32(rd["Qty"].ToString());
                model.Add(Temp_Ord);
            }

            return View(model);
        }
        public ActionResult Orders()
        {
            Connection con = new Connection();
            con.getConnection();
            SqlCommand com = new SqlCommand("select o.Order_id as OID,p.Pid as PID,p.Product_Name as P_Name,e.Emp_id as EID,e.Emp_Name as E_Name,b.Bid as BID,b.Branch_Name as B_Name,o.Qty as Qty from Orders o,Products p,Branch b,Employee e where p.Pid = o.product_id and b.Bid = o.branch_id and e.Emp_id = o.Emp_id and o.emp_id = '"+ Convert.ToInt32(Session["Empid"].ToString())+"'", con.getConnection());
            SqlDataReader rd = com.ExecuteReader();
            var model = new List<Order>();
            while (rd.Read())
            {
                var Temp_Ord = new Order();
                Temp_Ord.Order_id = Convert.ToInt32(rd["OID"].ToString());
                Temp_Ord.Product_id = Convert.ToInt32(rd["PID"].ToString());
                Temp_Ord.Product_Name = rd["P_Name"].ToString();
                Temp_Ord.Emp_id = Convert.ToInt32(rd["EID"].ToString());
                Temp_Ord.Emp_Name = rd["E_Name"].ToString();
                Temp_Ord.Branch_id = Convert.ToInt32(rd["BID"].ToString());
                Temp_Ord.Branch_Name = rd["B_Name"].ToString();
                Temp_Ord.Qty = Convert.ToInt32(rd["Qty"].ToString());
                model.Add(Temp_Ord);
            }

            return View(model);
        }



        [HttpPost]
        public ActionResult OrderAdd(Order ord)
        {
            if (ModelState.IsValid)
            {
                using (var context = new OrderEntities())
                {
                    ord.Emp_id = Convert.ToInt32(Session["Empid"].ToString());
                    ord.Branch_id = Convert.ToInt32(Session["Bid"].ToString());                    
                    context.Orders.Add(ord);

                    context.SaveChanges();
                    TempData["msgOrderCreation"] = "<script>alert('Successfully Created Order');</script>";
                }
                return View("OrderSuccess");
            }
            else
            {
                ViewBag.ProductList = "";                
                GetProducts();
            }
            return View("OrderView");            
        }
    }
}