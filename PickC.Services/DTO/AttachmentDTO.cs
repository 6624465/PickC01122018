using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
   public class AttachmentDTO
    {
        public string attachmentId { get; set; }
        public string operatorId { get; set; }
        public string lookupCode { get; set; }
        public string imagePath { get; set; }
    }
}
