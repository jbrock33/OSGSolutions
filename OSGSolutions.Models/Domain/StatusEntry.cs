using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Models.Domain
{
    public class StatusEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public DateTime Modified { get; set; }
    }
}
