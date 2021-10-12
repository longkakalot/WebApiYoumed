using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApiNet5.Models;
using WebApiNet5.Services;

namespace WebApiNet5.Repository
{
    public class ThongTinHanhChinhRepo : IThongTinHanhChinhRepo
    {
        private readonly IDapperService _dapper;
        private readonly ILogger<ThongTinHanhChinhRepo> _logger;
        public ThongTinHanhChinhRepo(IDapperService dapper, ILogger<ThongTinHanhChinhRepo> logger, ILoggerFactory logFactory)
        {
            _logger = logger;
            _dapper = dapper;
            _logger = logFactory.CreateLogger<ThongTinHanhChinhRepo>();
        }
        public async Task<IList<DanToc>> GetDanTocs()
        {
            try
            {
                var sql = "api_DanhSachDanToc";
                var resultAwait = await _dapper.GetAll<DanToc>(sql, null, CommandType.StoredProcedure);
                var result = resultAwait.ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDanTocs ThongTinHanhChinhRepo" + ex.Message);
                return null;
            }
        }
    }
}
