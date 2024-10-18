using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Response
{
    public class BookRespone
    {
        
            public List<Book> Books { get; set; }
            public int TotalPages { get; set; }
            public int PageIndex { get; set; }
        }
    
}
