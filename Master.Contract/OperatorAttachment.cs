using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Master.Contract
{
  public  class OperatorAttachment : IContract
    {
        public string attachmentId { get; set; }
        public string operatorId { get; set; }
        public string lookupCode { get; set; }
        public string imagePath { get; set; }
    }
    //public class  FileModel
    //{
    //   public HttpPostedFileBase[] files { get; set; }
    //}
}
