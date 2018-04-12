using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Master.Contract;



namespace PickCInternal2.HtmlHelpers
{
    public static class CustomHelpers
    {

        public static bool checkHasRight(string roleRightKey)
        {
            var securables = (List<Securables>)HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var securableItem = securables
                                    .Where(x => x.SecurableItem == roleRightKey)
                                    .FirstOrDefault();

            if (securableItem != null)
                return true;
            else
                return false;
        }

        public static HtmlString SubmitButton(
            this HtmlHelper htmlHelper,
            string buttonText,
            object htmlAttributes,
            string roleRightKey)
        {
            var hasRight = checkHasRight(roleRightKey);
            StringBuilder submitBtn = new StringBuilder();
            if (hasRight)
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }

                submitBtn.Append("<button type=\"submit\" ");
                if (htmlAttributesString != string.Empty)
                {
                    submitBtn.Append(htmlAttributesString);
                }
                submitBtn.AppendFormat(">{0}</button>", buttonText);
            }

            return new HtmlString(submitBtn.ToString());
        }

        public static HtmlString Button(
            this HtmlHelper htmlHelper,
            string buttonText,
            object htmlAttributes,
            string roleRightKey)
        {
            StringBuilder button = new StringBuilder();
            var hasRight = checkHasRight(roleRightKey);

            if (hasRight)
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }

                button.Append("<button type=\"button\" ");
                if (htmlAttributesString != string.Empty)
                {
                    button.Append(htmlAttributesString);
                }
                button.AppendFormat(">{0}</button>", buttonText);
            }

            return new HtmlString(button.ToString());
        }

        public static HtmlString RmaActionLink(
            this HtmlHelper htmlHelper,
            string url,
            string anchorText,
            object htmlAttributes,
            string roleRightKey
            )
        {
            var hasRight = checkHasRight(roleRightKey);
            StringBuilder anchor = new StringBuilder();
            if (hasRight)
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }


                anchor.Append($"<a href=\"{url}\" ");
                if (htmlAttributesString != string.Empty)
                {
                    anchor.Append(htmlAttributesString);
                }
                anchor.AppendFormat(">{0}</a>", anchorText);
            }

            return new HtmlString(anchor.ToString());
        }

        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string roleRightKey = "")
        {
            var hasRight = checkHasRight(roleRightKey);
            StringBuilder ancor = new StringBuilder();
            if (hasRight)
            {
                string queryString = string.Empty;
                string htmlAttributesString = string.Empty;
                if (routeValues != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(routeValues);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        if (i > 0)
                        {
                            queryString += "?";
                        }
                        queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                    }
                }

                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "='" + d.Values.ElementAt(i) + "'";
                    }
                }

                //What is Entity Framework??

                ancor.Append("<a ");
                if (htmlAttributesString != string.Empty)
                {
                    ancor.Append(htmlAttributesString);
                }
                ancor.Append(" href='");

                var strHref = string.Empty;
                if (controllerName != string.Empty)
                {
                    //ancor.Append("/" + controllerName);
                    strHref = "~/" + controllerName;
                }

                if (actionName != "Index")
                {
                    //ancor.Append("/" + actionName);
                    strHref = strHref + "/" + actionName;
                }
                if (queryString != string.Empty)
                {
                    //ancor.Append("?q=" + Encrypt(queryString));
                    strHref = strHref + "/" + "?q=" + UrlEncryptionHelper.Encrypt(queryString);
                }

                var context = new HttpContextWrapper(HttpContext.Current);
                string hrefUrl = UrlHelper.GenerateContentUrl(strHref, context);
                ancor.Append(hrefUrl);
                ancor.Append("'");
                ancor.Append(">");
                ancor.Append(linkText);
                ancor.Append("</a>");
            }


            return new MvcHtmlString(ancor.ToString());
        }

        public static IDisposable RmaBeginForm(
            this HtmlHelper htmlHelper,
            string url,
            FormMethod method,
            object htmlAttributes,
            string roleRightKey)
        {

            var hasRight = checkHasRight(roleRightKey);

            if (hasRight)
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }

                var writer = htmlHelper.ViewContext.Writer;
                writer.WriteLine(string.Format($"<form action=\"{url}\" method=\"{method}\" {htmlAttributesString}>"));
                return new FormContainer(writer);
            }
            else
            {
                var writer = htmlHelper.ViewContext.Writer;
                return new FormContainer(writer);
            }
        }

        public class FormContainer : IDisposable
        {
            private readonly TextWriter _writer;

            public FormContainer(TextWriter writer)
            {
                _writer = writer;
            }

            public void Dispose()
            {
                _writer.Write("</form>");
            }
        }

        public class DivContainer : IDisposable
        {
            private readonly TextWriter _writer;

            public DivContainer(TextWriter writer)
            {
                _writer = writer;
            }

            public void Dispose()
            {
                _writer.Write("</div>");
            }
        }

    }
}