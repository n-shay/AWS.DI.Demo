using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using HelloWorld.DynamoDB.Lambda.Domain.Models;
using HelloWorld.DynamoDB.Lambda.Domain.Repositories;
using TRG.Extensions.DataAccess.DynamoDB;
using TRG.Extensions.Logging;

namespace HelloWorld.DynamoDB.Lambda.Infrastructure.DynamoDB
{
    public class FooRepository : WritableKeyedRepository<SomeFoo, int>, IFooRepository
    {
        public FooRepository(ILogger logger, IDbContext context) 
            : base(logger, context)
        {
        }

        public async Task<IEnumerable<SomeFoo>> QueryByIdAndTextAsync(int id, string startsWith)
        {
            var query = new QueryOperationConfig();
            query.Filter
                .AddCondition("SomeId", QueryOperator.Equal,
                    new Primitive(id.ToString(), true));

            if (startsWith != null)
                query.Filter
                    .AddCondition("SomeString", QueryOperator.BeginsWith, new Primitive(startsWith));

            query.Select = SelectValues.SpecificAttributes;
            query.AttributesToGet = new List<string> { "SomeInt" };
            query.Limit = 1;
            query.IndexName = "SomeId-SomeString-index";

            var search = Context.FromQueryAsync<SomeFoo>(query);

            return await search.GetRemainingAsync();
        }

        public async Task<IEnumerable<SomeFoo>> QueryByNumAndTextAsync(int num, string startsWith)
        {
            var query = new QueryOperationConfig();
            query.Filter
                .AddCondition("SomeInt", QueryOperator.Equal,
                    new Primitive(num.ToString(), true));

            if (startsWith != null)
                query.Filter
                    .AddCondition("SomeString", QueryOperator.BeginsWith, new Primitive(startsWith));

            query.Limit = 1;
            query.IndexName = "SomeInt-SomeString-index";

            var search = Context.FromQueryAsync<SomeFoo>(query);

            return await search.GetRemainingAsync();
        }

        public async Task<IEnumerable<SomeFoo>> ScanAsync(int? id = null, int? num = null, string searchText = null)
        {
            var scan = new ScanOperationConfig();
            
            if (id != null)
                scan.Filter
                    .AddCondition("SomeId", ScanOperator.Equal,
                        new Primitive(id.Value.ToString(), true));

            if (num != null)
                scan.Filter
                    .AddCondition("SomeInt", ScanOperator.Equal,
                        new Primitive(num.Value.ToString(), true));

            if (searchText != null)
                scan.Filter
                    .AddCondition("SomeString", ScanOperator.Contains, new Primitive(searchText));
            
            var search = Context.FromScanAsync<SomeFoo>(scan);

            return await search.GetRemainingAsync();
        }
    }
}