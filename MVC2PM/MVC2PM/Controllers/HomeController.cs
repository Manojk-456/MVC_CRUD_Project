using MVC2PM.DB_Context_EF;
using MVC2PM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2PM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            customerEntities obj = new customerEntities();
            List<CustomerModel> cobj = new List<CustomerModel>();
            var res = obj.customers.ToList();
            foreach (var item in res)
            {
                cobj.Add(new CustomerModel
                {
                    id = item.id,
                    name=item.name,
                    email=item.email
                });

            }
            return View(cobj);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerModel cobj)
        {
            customerEntities obj = new customerEntities();
            customer tblobj = new customer();
            tblobj.id = cobj.id;
            tblobj.name = cobj.name;
            tblobj.email = cobj.email;


            if (cobj.id == 0)
            {
                obj.customers.Add(tblobj);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }
            //return View(tblobj);
            return RedirectToAction("Index","Home");
        }
        public ActionResult Edit(int id)
        {
            customerEntities obj = new customerEntities();
            CustomerModel cobj = new CustomerModel();
            var Edititem = obj.customers.Where(b => b.id == id).First();
            cobj.id = Edititem.id;
            cobj.name = Edititem.name;
            cobj.email = Edititem.email;
            ViewBag.id = Edititem.id;

            return View("AddCustomer",cobj);
        }
        public ActionResult Delete(int id)
        {
            customerEntities obj = new customerEntities();
            var del = obj.customers.Where(m => m.id == id).First();
            obj.customers.Remove(del);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}