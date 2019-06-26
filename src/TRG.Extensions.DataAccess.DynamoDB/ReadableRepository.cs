using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using TRG.Extensions.Logging;

namespace TRG.Extensions.DataAccess.DynamoDB
{
    public abstract class ReadableRepository<TEntity> :
        RepositoryBase,
        IReadableRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected ReadableRepository(ILogger logger, IDbContext context)
            : base(logger, context)
        {
        }

        protected QueryBuilder Query()
        {
            return new QueryBuilder(Context);
        }

        protected ScanBuilder Scan()
        {
            return new ScanBuilder(Context);
        }

        public class QueryBuilder
        {
            private readonly IDbContext _context;
            private readonly ICollection<Action<QueryFilter>> _collection = new List<Action<QueryFilter>>();
            private string _indexName;
            private int? _limit;

            public QueryBuilder(IDbContext context)
            {
                _context = context;
            }

            public QueryBuilder Filter(Action<QueryFilter> filterAction)
            {
                if (filterAction == null) throw new ArgumentNullException(nameof(filterAction));

                _collection.Add(filterAction);
                return this;
            }

            public QueryBuilder UseIndex(string indexName)
            {
                _indexName = indexName ?? throw new ArgumentNullException(nameof(indexName));
                return this;
            }

            public QueryBuilder Limit(int limit)
            {
                _limit = limit;
                return this;
            }

            public async Task<IEnumerable<TEntity>> ExecuteAsync()
            {
                var query = new QueryOperationConfig();

                foreach (var filterAction in _collection)
                {
                    filterAction(query.Filter);
                }

                if (_limit != null)
                    query.Limit = _limit.Value;

                if (_indexName != null)
                    query.IndexName = _indexName;

                var search = _context.FromQueryAsync<TEntity>(query);

                return await search.GetRemainingAsync();
            }
        }

        public class ScanBuilder
        {
            private readonly IDbContext _context;
            private readonly ICollection<Action<ScanFilter>> _collection = new List<Action<ScanFilter>>();
            private string _indexName;
            private int? _limit;

            public ScanBuilder(IDbContext context)
            {
                _context = context;
            }

            public ScanBuilder Filter(Action<ScanFilter> filterAction)
            {
                if (filterAction == null) throw new ArgumentNullException(nameof(filterAction));

                _collection.Add(filterAction);
                return this;
            }

            public ScanBuilder UseIndex(string indexName)
            {
                _indexName = indexName ?? throw new ArgumentNullException(nameof(indexName));
                return this;
            }

            public ScanBuilder Limit(int limit)
            {
                _limit = limit;
                return this;
            }

            public async Task<IEnumerable<TEntity>> ExecuteAsync()
            {
                var scan = new ScanOperationConfig();

                foreach (var filterAction in _collection)
                {
                    filterAction(scan.Filter);
                }

                if (_limit != null)
                    scan.Limit = _limit.Value;

                if (_indexName != null)
                    scan.IndexName = _indexName;

                var search = _context.FromScanAsync<TEntity>(scan);

                return await search.GetRemainingAsync();
            }
        }

    }
}