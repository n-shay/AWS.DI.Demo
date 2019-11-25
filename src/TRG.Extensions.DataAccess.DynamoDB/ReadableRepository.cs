namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Amazon.DynamoDBv2.DocumentModel;

    using TRG.Extensions.Logging;

    public abstract class ReadableRepository<TEntity> :
        RepositoryBase,
        IReadableRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected ReadableRepository(ILogger logger, IContextProvider<IDbContext> contextProvider) 
            : base(logger, contextProvider)
        {
        }

        protected QueryBuilder Query()
        {
            return new QueryBuilder(this.Context);
        }

        protected ScanBuilder Scan()
        {
            return new ScanBuilder(this.Context);
        }

        public class QueryBuilder
        {
            private readonly IDbContext context;
            private readonly ICollection<Action<QueryFilter>> collection = new List<Action<QueryFilter>>();
            private IEnumerable<string> propertiesToGet;
            private string indexName;
            private int? limit;

            public QueryBuilder(IDbContext context)
            {
                this.context = context;
            }

            public QueryBuilder Filter(Action<QueryFilter> filterAction)
            {
                if (filterAction == null) throw new ArgumentNullException(nameof(filterAction));

                this.collection.Add(filterAction);
                return this;
            }

            public QueryBuilder UseIndex(string indexName)
            {
                this.indexName = indexName ?? throw new ArgumentNullException(nameof(indexName));
                return this;
            }

            public QueryBuilder Limit(int limit)
            {
                this.limit = limit;
                return this;
            }

            public QueryBuilder Properties(params string[] properties)
            {
                if (properties == null) throw new ArgumentNullException(nameof(properties));
                if (properties.Length == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(properties));
                this.propertiesToGet = properties;
                return this;
            }

            public async Task<IEnumerable<TEntity>> ExecuteAsync()
            {
                var query = new QueryOperationConfig();

                foreach (var filterAction in this.collection)
                {
                    filterAction(query.Filter);
                }

                if (this.limit != null)
                    query.Limit = this.limit.Value;

                if (this.indexName != null)
                    query.IndexName = this.indexName;

                if (this.propertiesToGet != null)
                {
                    query.AttributesToGet = this.propertiesToGet.ToList();
                    query.Select = SelectValues.SpecificAttributes;
                }

                var search = this.context.FromQueryAsync<TEntity>(query);

                return await search.GetRemainingAsync();
            }
        }

        public class ScanBuilder
        {
            private readonly IDbContext context;
            private readonly ICollection<Action<ScanFilter>> collection = new List<Action<ScanFilter>>();
            private IEnumerable<string> propertiesToGet;
            private string indexName;
            private int? limit;

            public ScanBuilder(IDbContext context)
            {
                this.context = context;
            }

            public ScanBuilder Filter(Action<ScanFilter> filterAction)
            {
                if (filterAction == null) throw new ArgumentNullException(nameof(filterAction));

                this.collection.Add(filterAction);
                return this;
            }

            public ScanBuilder UseIndex(string indexName)
            {
                this.indexName = indexName ?? throw new ArgumentNullException(nameof(indexName));
                return this;
            }

            public ScanBuilder Limit(int limit)
            {
                this.limit = limit;
                return this;
            }

            // TODO: convert to projection expression or based on reflection 
            public ScanBuilder Properties(params string[] properties)
            {
                if (properties == null) throw new ArgumentNullException(nameof(properties));
                if (properties.Length == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(properties));
                this.propertiesToGet = properties;
                return this;
            }

            public async Task<IEnumerable<TEntity>> ExecuteAsync()
            {
                var scan = new ScanOperationConfig();

                foreach (var filterAction in this.collection)
                {
                    filterAction(scan.Filter);
                }

                if (this.limit != null)
                    scan.Limit = this.limit.Value;

                if (this.indexName != null)
                    scan.IndexName = this.indexName;

                if (this.propertiesToGet != null)
                {
                    scan.AttributesToGet = this.propertiesToGet.ToList();
                    scan.Select = SelectValues.SpecificAttributes;
                }

                var search = this.context.FromScanAsync<TEntity>(scan);

                return await search.GetRemainingAsync();
            }
        }
    }
}