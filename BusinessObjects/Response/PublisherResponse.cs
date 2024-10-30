using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Response
{
    public class PublisherResponse
    {
        public List<Publisher> Publishers { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }
}
