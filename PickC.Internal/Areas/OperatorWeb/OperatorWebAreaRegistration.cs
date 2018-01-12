using System.Web.Mvc;

namespace PickC.Internal.Areas.OperatorWeb
{
    public class OperatorWebAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OperatorWeb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OperatorWeb_default",
                "OperatorWeb/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}