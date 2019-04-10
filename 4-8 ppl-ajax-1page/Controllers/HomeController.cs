using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ppl_ajax.data;

namespace _4_8_ppl_ajax_1page.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult AddPerson(Person person)
        {
            DbManager mgr = new DbManager(Properties.Settings.Default.ConStr);
            mgr.AddPerson(person);
            IEnumerable<Person> ppl = mgr.GetPeople();
            return Json(ppl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPpl()
        {
            DbManager mgr = new DbManager(Properties.Settings.Default.ConStr);
            IEnumerable<Person> ppl = mgr.GetPeople();
            return Json(ppl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPerson(int id)
        {
            DbManager mgr = new DbManager(Properties.Settings.Default.ConStr);
            Person person = mgr.GetPerson(id);
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePerson(Person person)
        {
            DbManager mgr = new DbManager(Properties.Settings.Default.ConStr);
            mgr.UpdatePerson(person);
            return Json(mgr.GetPeople(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            DbManager mgr = new DbManager(Properties.Settings.Default.ConStr);
            mgr.Delete(id);
            return Json(mgr.GetPeople(), JsonRequestBehavior.AllowGet);
        }
    }
}