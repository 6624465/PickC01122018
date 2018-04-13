using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PickCInternal2.Controllers
{
    public class LayoutMenuRights
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public List<SecurablesRights> securablesLst { get; set; }
    }


    public class SecurablesRights
    {
        public SecurablesRights() { }
        public string SecurableItem { get; set; }
        public string GroupID { get; set; }
        public string Description { get; set; }
        public string ActionType { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public bool hasRight { get; set; }
        public Int32 Sequence { get; set; }
        public Int32 ParentSequence { get; set; }
        public List<SecurablesRights> ActionMenus { get; set; }
    }

    public class RoleRightsMenu
    {
        public RoleRightsMenu() { }
        public string RoleCode { get; set; }
        public string SecurableItem { get; set; }
        public bool hasRight { get; set; }
    }

    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RoleRights(string Role = "")
        {
            return View();
        }
    }
}