using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using TRG.Extensions.DataAccess.DynamoDB;
using TRG.Extensions.Logging;

namespace HelloWorld.DynamoDB.Lambda.Infrastructure.DynamoDB
{
    public class FooRepository : ReadableKeyedRepository<SomeFoo, int>, IFooRepository
    {
        public FooRepository(ILogger logger, IDbContext context) 
            : base(logger, context)
        {
        }

        public async Task<IEnumerable<SomeFoo>> QueryByIdAndTextAsync(int id, string startsWith)
        {
            var query = Query()
                .Filter(f => f.AddCondition("SomeId", QueryOperator.Equal, new Primitive(id.ToString(), true)));

            if (startsWith != null)
                query = query
                    .Filter(f => f.AddCondition("SomeString", QueryOperator.BeginsWith, new Primitive(startsWith)));

            return await query
                .Limit(1)
                .UseIndex("SomeId-SomeString-index")
                .ExecuteAsync();
        }

        public async Task<IEnumerable<SomeFoo>> QueryByNumAndTextAsync(int num, string startsWith)
        {
            var query = Query()
                .Filter(f => f.AddCondition("SomeInt", QueryOperator.Equal, new Primitive(num.ToString(), true)));

            if (startsWith != null)
                query = query
                    .Filter(f => f.AddCondition("SomeString", QueryOperator.BeginsWith, new Primitive(startsWith)));

            return await query
                .Limit(1)
                .UseIndex("SomeInt-SomeString-index")
                .ExecuteAsync();
        }

        public async Task<IEnumerable<SomeFoo>> ScanAsync(int? id = null, int? num = null, string searchText = null)
        {
            var scan = Scan();

            if (id != null)
                scan = scan
                    .Filter(f =>
                        f.AddCondition("SomeId", ScanOperator.Equal, new Primitive(id.Value.ToString(), true)));

            if (num != null)
                scan = scan
                    .Filter(f =>
                        f.AddCondition("SomeInt", ScanOperator.Equal, new Primitive(num.Value.ToString(), true)));

            if (searchText != null)
                scan = scan.Filter(f => f.AddCondition("SomeString", ScanOperator.Contains, new Primitive(searchText)));

            return await scan.ExecuteAsync();
        }
    }
}