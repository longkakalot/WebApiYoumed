using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNet5.Models
{
    public class DanToc
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Nation
    {
        public List<DanToc> Nations { get; set; }
    }
}
