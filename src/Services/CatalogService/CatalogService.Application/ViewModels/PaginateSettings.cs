using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.ViewModels
{
    public class PaginateSettings
    {
        public PaginateSettings(int pageIndex = 0, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
