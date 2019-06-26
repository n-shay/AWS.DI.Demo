using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.DynamoDB.Lambda.Domain;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using TRG.Extensions.DataAccess;
using TRG.Extensions.Logging;

namespace HelloWorld.DynamoDB.Lambda.Business.Concrete
{
    public class FooService : IFooService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public FooService(ILogger logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<SomeFoo> GetItemAsync(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create<IFooUnitOfWork>())
            {
                _logger.Information($"Querying {nameof(unitOfWork.Foos.GetByKeyAsync)}...");

                var result = await unitOfWork.Foos.GetByKeyAsync(id);

                _logger.Debug(result == null
                    ? "No results found!"
                    : $"Result={result}");

                _logger.Information("Finished querying!");

                return result;
            }
        }

        public async Task<IEnumerable<SomeFoo>> GetByNumAndTextAsync(int num, string startsWith)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create<IFooUnitOfWork>())
            {
                _logger.Information($"Querying {nameof(unitOfWork.Foos.QueryByNumAndTextAsync)}...");

                var result = (await unitOfWork.Foos.QueryByNumAndTextAsync(num, startsWith)).ToList();

                _logger.Debug($"{result.Count} records found.");
                var i = 0;
                foreach (var r in result)
                {
                    _logger.Debug($"Result[{i}]={r}");
                    i++;
                }

                _logger.Information("Finished querying!");

                return result;
            }
        }

        public async Task<IEnumerable<SomeFoo>> SearchAsync(int? id = null, int? num = null, string containsText = null)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create<IFooUnitOfWork>())
            {
                _logger.Information($"Scanning {nameof(unitOfWork.Foos.ScanAsync)}...");

                var result = (await unitOfWork.Foos.ScanAsync(id, num, containsText)).ToList();

                _logger.Debug($"{result.Count} records found.");
                var i = 0;
                foreach (var r in result)
                {
                    _logger.Debug($"Result[{i}]={r}");
                    i++;
                }

                _logger.Information("Finished scanning!");

                return result;
            }
        }
    }
}