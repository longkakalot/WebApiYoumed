using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNet5.Services
{
    public interface IDapperService
    {
        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<int> InsertList<T>(string sp, List<DynamicParameters> parms, CommandType commandType = CommandType.StoredProcedure);

        Task<int> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

    }
}
