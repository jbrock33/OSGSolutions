using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Models.Domain
{
    public class UploadFile
    {
        public byte[] ByteArray { get; set; }
        public string Extension { get; set; }
        public string FileUploadName { get; set; }
        public string Location { get; set; }
    }
}
