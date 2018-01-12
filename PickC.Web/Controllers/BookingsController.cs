using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using Operation.Contract;
using System.Text;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

using PickC.Services;
using PickC.Services.DTO;

namespace PickC.Web.Controllers
{
    [WebAuthFilter]
    [PickCEx]
    public class BookingsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Bookings()
        {
            var bookings = await new BookingService(AUTHTOKEN, p_mobileNo).GetBookingsByMobileNoAsync();
            return View(bookings.OrderByDescending(x => x.BookingDate).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> SaveBooking(Booking booking)
        {
            try
            {
                booking.BookingDate = DateTime.Now;
                booking.CancelTime = DateTime.Now;
                booking.ConfirmDate = DateTime.Now;
                booking.CompleteTime = DateTime.Now;
                booking.CustomerID = p_mobileNo;
                var result = await new BookingService(AUTHTOKEN, p_mobileNo).SaveBookingAsync(booking);

                return RedirectToAction("Bookings");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> ShowMap(string bookingNo)
        {
             var data = await new BookingService(AUTHTOKEN,p_mobileNo).GetBookingsByBookingNoAsync(bookingNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /*
        [HttpGet]
        public ActionResult Pdf()
        {
            var result = ConvertHtmlToPdfAsFile(Server.MapPath("~/images/") + "my.pdf", PartialView("Pdf").RenderToString());
            return View("Pdf");
        }
        */

        [HttpGet]
        public ActionResult EmailInvoice(string bookingNo)
        {
            var emailGen = new PickC.Web.Utilities.EmailGenerator();
            emailGen.ConfigMail(p_emailID, true, "PickC Invoice", "<div>Testing mail</div>", GetPDF(PartialView("Pdf").RenderToString()));
            return RedirectToAction("Bookings");
        }

        [HttpGet]
        public ActionResult PrintInvoice(string bookingNo)
        {
            DownloadPDF(PartialView("Pdf").RenderToString(), bookingNo);
            return RedirectToAction("Bookings");
        }

        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            /*vijay customization starts*/
            iTextSharp.text.Image LocationImage = iTextSharp.text.Image.GetInstance("http://maps.googleapis.com/maps/api/staticmap?autoscale=2&size=600x300&maptype=roadmap&key=AIzaSyB1_yOg6NqJnT7UuVnrTf7TA_lTOt13cPE&format=png&path=color:0xff0000ff&visual_refresh=true&markers=size:mid%7Ccolor:0x008000%7Clabel:A%7Cchanda nagar, Telangana, India&markers=size:mid%7Ccolor:0xff0000%7Clabel:B%7Cnagole, Telangana, India");
            LocationImage.ScaleToFit(650, 650);
            LocationImage.Alignment = iTextSharp.text.Image.UNDERLYING;
            LocationImage.SetAbsolutePosition(0, 0);
            var locationcell = new PdfPCell(LocationImage);
            locationcell.PaddingBottom = 10;
            locationcell.PaddingTop = 10;
            locationcell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            locationcell.Colspan = 3;
            locationcell.Border = 0;
            var Locationtable = new PdfPTable(2);

            //Header Widths     
            float[] headersOI = { 50, 50 };
            //Set the pdf headers
            Locationtable.SetWidths(headersOI);
            //Set the PDF File witdh percentage
            Locationtable.WidthPercentage = 80;
            //Add Title to the PDF file at the top
            Locationtable.AddCell(new PdfPCell(new Phrase(string.Empty))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                FixedHeight = 6,
                MinimumHeight = 4,
                BorderWidth = 0
            });

            Locationtable.AddCell(locationcell);
            doc.Add(Locationtable);
            /*vijay customization ends*/

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }

        public void DownloadPDF(string HtmlContent, string fileName)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(GetPDF(HtmlContent));
            Response.End();
        }

        public ActionResult CustomerBookingHistory()
        {
            BookingHistoryDTO bookingSearchVM = new BookingHistoryDTO();
            bookingSearchVM.BookingHistoryDetails = new List<BookingHistoryDetails>();
            return View("CustomerBookingHistory", bookingSearchVM);
        }
        [HttpPost]
        public async Task<ActionResult> CustomersBookingHistory(BookingHistoryDTO bookingHistoryDTO)
        {
            var data = await new BookingService(AUTHTOKEN, p_mobileNo).GetBookingsHistoryByMobileNoAsync(bookingHistoryDTO.CustomerMobile);
            if (data.Count == 0)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('No Data Found...!');location.href='/Bookings/CustomerBookingHistory';</script>");
            }
                var bookingSearchVM = new BookingHistoryDTO();
                bookingSearchVM.BookingHistoryDetails = data;
                return View("CustomerBookingHistory", bookingSearchVM);         
        }

        [HttpGet]
        public async Task<ActionResult> PrintCustomerHistoryInvoice(string BookingNo)
        {

            var bookingHistoryList = await new CustomerService().TripInvoiceList(BookingNo);

            if (bookingHistoryList != null)
                DownloadPDF2(PartialView("pdf2", bookingHistoryList).RenderToString(), BookingNo);

            return View("Bookings");
        }

        //public JsonResult CustomerBookingHistory(int? page)
        //{


        //}

        public void DownloadPDF2(string HtmlContent, string fileName)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(GetPDF2(HtmlContent));
            Response.End();
        }

        public byte[] GetPDF2(string pHTML)
        {
            try
            {
                byte[] bPDF = null;

                MemoryStream ms = new MemoryStream();
                TextReader txtReader = new StringReader(pHTML);

                // 1: create object of a itextsharp document class
                Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

                // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
                PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

                // 3: we create a worker parse the document
                HTMLWorker htmlWorker = new HTMLWorker(doc);

                // 4: we open document and start the worker on the document
                doc.Open();
                htmlWorker.StartDocument();

                // 5: parse the html into the document
                htmlWorker.Parse(txtReader);
                //iTextSharp.text.Image LocationImage = iTextSharp.text.Image.GetInstance("http://ragsarma-001-site13.htempurl.com/PickCWeb/images/logo.png");
                //LocationImage.ScaleToFit(50, 60);
                //LocationImage.Alignment = iTextSharp.text.Image.UNDERLYING;
                //var locationcell = new PdfPCell(LocationImage);
                //locationcell.PaddingBottom = 10;
                //locationcell.PaddingTop = 10;
                //locationcell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //locationcell.VerticalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //locationcell.Colspan = 3;
                //locationcell.Border = 0;
                //var Locationtable = new PdfPTable(2);

                ////Header Widths     
                //float[] headersOI = { 50, 50 };
                ////Set the pdf headers
                //Locationtable.SetWidths(headersOI);
                ////Set the PDF File witdh percentage
                //Locationtable.WidthPercentage = 10;
                //Locationtable.AddCell(new PdfPCell(new Phrase(string.Empty))
                //{
                //    Colspan = 0,
                //    HorizontalAlignment = Element.ALIGN_LEFT,
                //    VerticalAlignment = Element.ALIGN_TOP,
                //    FixedHeight = 5,
                //    MinimumHeight = 1,
                //    BorderWidth = 0

                //});
                //Locationtable.AddCell(locationcell);
                //doc.Add(Locationtable);               
                htmlWorker.EndDocument();
                htmlWorker.Close();
                doc.Close();

                bPDF = ms.ToArray();

                return bPDF;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /*
        public static ReturnValue ConvertHtmlToPdfAsBytes(string HtmlData)
        {
            // variables  
            ReturnValue Result = new ReturnValue();

            // do some additional cleansing to handle some scenarios that are out of control with the html data  
            HtmlData = HtmlData.Replace("<br>", "<br />");

            // convert html to pdf  
            try
            {
                // create a stream that we can write to, in this case a MemoryStream  
                using (var stream = new MemoryStream())
                {
                    // create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF  
                    using (var document = new Document())
                    {
                        // create a writer that's bound to our PDF abstraction and our stream  
                        using (var writer = PdfWriter.GetInstance(document, stream))
                        {
                            // open the document for writing  
                            document.Open();

                            // read html data to StringReader  
                            using (var html = new StringReader(HtmlData))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
                            }

                            // close document  
                            document.Close();
                        }
                    }

                    // get bytes from stream  
                    Result.Data = stream.ToArray();

                    // success  
                    Result.Success = true;
                }
            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = ex.Message;
            }

            // return  
            return Result;
        }

        public static ReturnValue ConvertHtmlToPdfAsFile(string FilePath, string HtmlData)
        {
            // variables  
            ReturnValue Result = new ReturnValue();

            try
            {
                // convert html to pdf and get bytes array  
                Result = ConvertHtmlToPdfAsBytes(HtmlData: HtmlData);

                // check for errors  
                if (!Result.Success)
                {
                    return Result;
                }

                // create file  
                System.IO.File.WriteAllBytes(path: FilePath, bytes: Result.Data);

                // result  
                Result.Success = true;
            }
            catch (Exception ex)
            {
                Result.Success = false;
                Result.Message = ex.Message;
            }

            // return  
            return Result;
        }
        */
        /*
        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult Pdf()
        {
            return PartialView();
        }

        public static string RenderPartialToString(string controlName, object viewData)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

            viewPage.ViewData = new ViewDataDictionary(viewData);
            viewPage.Controls.Add(viewPage.LoadControl(controlName));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewPage.RenderControl(tw);
                }
            }

            return sb.ToString();
        }
        */
    }

    public static class ViewExtensions
    {
        public static string RenderToString(this PartialViewResult partialView)
        {
            var httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }

            return sb.ToString();
        }
    }

    public class ReturnValue
    {
        public ReturnValue()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

        public bool Success = false;
        public string Message = string.Empty;
        public Byte[] Data = null;
    }


}