using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.DynamoDB.Lambda.Domain.Models;

namespace HelloWorld.DynamoDB.Lambda.Business
{
    public interface IFooService
    {
        Task<SomeFoo> GetItemAsync(int id);

        Task<IEnumerable<SomeFoo>> GetByNumAndTextAsync(int num, string startsWith);

        Task<IEnumerable<SomeFoo>> SearchAsync(int? id = null, int? num = null, string containsText = null);
    }
}
