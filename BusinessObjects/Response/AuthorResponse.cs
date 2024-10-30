using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Response
{
    public class AuthorResponse
    {
        public List<Author> Authors { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }
}
