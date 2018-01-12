using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickC.Services.DTO
{
    public class DriverAttachmentsDTO
    {
        public string attachmentId { get; set; }
        public string driverId { get; set; }
        public string lookupCode { get; set; }
        public string imagePath { get; set; }
    }
}
