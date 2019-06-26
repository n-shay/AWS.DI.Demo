using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using TRG.Extensions.DataAccess;

namespace HelloWorld.DynamoDB.Lambda.Domain.Repositories
{
    public interface IFooRepository : IReadableKeyedRepository<SomeFoo, int>
    {
        Task<IEnumerable<SomeFoo>> QueryByIdAndTextAsync(int id, string startsWith);

        Task<IEnumerable<SomeFoo>> QueryByNumAndTextAsync(int num, string startsWith);

        Task<IEnumerable<SomeFoo>> ScanAsync(int? id = null, int? num = null, string searchText = null);
    }
}