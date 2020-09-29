using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Library.Internal
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        Task<IEnumerable<T>> QueryAsync<T, U>(string storedProcedure, U parameters);
    }
}