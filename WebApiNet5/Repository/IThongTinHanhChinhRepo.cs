using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNet5.Models;

namespace WebApiNet5.Repository
{
    public interface IThongTinHanhChinhRepo
    {
        Task<IList<DanToc>> GetDanTocs();
    }
}
