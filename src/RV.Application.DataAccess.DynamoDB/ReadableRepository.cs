using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using RV.Application.Logging;

namespace RV.Application.DataAccess.DynamoDB
{
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
            private IEnumerable<string> _propertiesToGet;
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

            public QueryBuilder Properties(params string[] properties)
            {
                if (properties == null) throw new ArgumentNullException(nameof(properties));
                if (properties.Length == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(properties));
                _propertiesToGet = properties;
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

                if (_propertiesToGet != null)
                {
                    query.AttributesToGet = _propertiesToGet.ToList();
                    query.Select = SelectValues.SpecificAttributes;
                }

                var search = _context.FromQueryAsync<TEntity>(query);

                return await search.GetRemainingAsync();
            }
        }

        public class ScanBuilder
        {
            private readonly IDbContext _context;
            private readonly ICollection<Action<ScanFilter>> _collection = new List<Action<ScanFilter>>();
            private IEnumerable<string> _propertiesToGet;
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

            // TODO: convert to projection expression or based on reflection 
            public ScanBuilder Properties(params string[] properties)
            {
                if (properties == null) throw new ArgumentNullException(nameof(properties));
                if (properties.Length == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(properties));
                _propertiesToGet = properties;
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

                if (_propertiesToGet != null)
                {
                    scan.AttributesToGet = _propertiesToGet.ToList();
                    scan.Select = SelectValues.SpecificAttributes;
                }

                var search = _context.FromScanAsync<TEntity>(scan);

                return await search.GetRemainingAsync();
            }
        }
    }
}