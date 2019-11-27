using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRG.Extensions.DataAccess.Specification;

namespace TRG.Extensions.DataAccess.EntityFramework.Specification
{
    /// <summary>
    /// Specification result class contains common functionality for filtering result.
    /// </summary>
    /// <typeparam name="TEntity">Domain entity.</typeparam>
    public class QueryableSpecificationResult<TEntity> : ISpecificationResult<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public QueryableSpecificationResult(IQueryable<TEntity> queryable)
        {
            this.Queryable = queryable;
        }

        /// <summary>
        /// Gets or sets IQueryable interface for domain entity.
        /// </summary>
        protected IQueryable<TEntity> Queryable { get; set; }

        public IQueryable<TEntity> AsQueryable()
        {
            return this.Queryable;
        }

        /// <summary>
        /// Takes given count of the records represented by the specification.
        /// </summary>
        public ISpecificationResult<TEntity> Take(int count)
        {
            this.Queryable = this.Queryable.Take(count);
            return this;
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns 
        /// the remaining elements.
        /// </summary>
        public ISpecificationResult<TEntity> Skip(int count)
        {
            this.Queryable = this.Queryable.Skip(count);
            return this;
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        public ISpecificationResult<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            this.Queryable = this.Queryable.OrderBy(keySelector);
            return this;
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        public ISpecificationResult<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            this.Queryable = this.Queryable.OrderByDescending(keySelector);
            return this;
        }

        public ISpecificationResult<TEntity> Distinct()
        {
            this.Queryable = this.Queryable.Distinct();
            return this;
        }

        /// <summary>
        /// Executes the specification and query behind it and returns list of records
        /// that matches criteria.
        /// </summary>
        public List<TEntity> ToList()
        {
            return this.Queryable.ToList();
        }

        /// <summary>
        /// Executes the specification and query behind it and returns list of records
        /// that matches criteria.
        /// </summary>
        public Task<List<TEntity>> ToListAsync()
        {
            return this.Queryable.ToListAsync();
        }

        public List<TResult> ToList<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null)
        {
            var query = this.ToListInternal(selector, maxResults);
            return query.ToList();
        }

        public Task<List<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null)
        {
            var query = this.ToListInternal(selector, maxResults);
            return query.ToListAsync();
        }

        private IQueryable<TResult> ToListInternal<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults)
        {
            var query = this.Queryable.Select(selector);
            if (maxResults != null)
                query = query.Take(maxResults.Value);
            return query;
        }

        public List<TResult> ToDistinctList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            int? maxResults = null)
        {
            var query = this.ToDistinctListInternal(selector, maxResults);
            return query.ToList();
        }

        public Task<List<TResult>> ToDistinctListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null)
        {
            var query = this.ToDistinctListInternal(selector, maxResults);
            return query.ToListAsync();
        }

        private IQueryable<TResult> ToDistinctListInternal<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults)
        {
            var query = this.Queryable.Select(selector).Distinct();
            if (maxResults != null)
                query = query.Take(maxResults.Value);
            return query;
        }

        /// <summary>
        /// Executes the specification and query behind it and returns the only record
        /// of a sequence. Throws if there is not exactly one element in the sequence.
        /// </summary>
        public TEntity Single()
        {
            return this.Queryable.Single();
        }

        /// <summary>
        /// Executes the specification and query behind it and returns the only record
        /// of a sequence. Throws if there is not exactly one element in the sequence.
        /// </summary>
        public Task<TEntity> SingleAsync()
        {
            return this.Queryable.SingleAsync();
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; 
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        public TEntity SingleOrDefault()
        {
            return this.Queryable.SingleOrDefault();
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; 
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        public Task<TEntity> SingleOrDefaultAsync()
        {
            return this.Queryable.SingleOrDefaultAsync();
        }

        public TEntity First()
        {
            return this.Queryable.First();
        }

        public Task<TEntity> FirstAsync()
        {
            return this.Queryable.FirstAsync();
        }

        public TEntity FirstOrDefault()
        {
            return this.Queryable.FirstOrDefault();
        }

        public Task<TEntity> FirstOrDefaultAsync()
        {
            return this.Queryable.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the number of elements the specification represents.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this.Queryable.Count();
        }

        /// <summary>
        /// Returns the number of elements the specification represents.
        /// </summary>
        /// <returns></returns>
        public Task<int> CountAsync()
        {
            return this.Queryable.CountAsync();
        }

        public int DistinctCount<T>(Expression<Func<TEntity, T>> selector)
        {
            return this.Queryable.Select(selector).Distinct().Count();
        }

        public Task<int> DistinctCountAsync<T>(Expression<Func<TEntity, T>> selector)
        {
            return this.Queryable.Select(selector).Distinct().CountAsync();
        }

        public double Average(Expression<Func<TEntity, int>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public double? Average(Expression<Func<TEntity, int?>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public double Average(Expression<Func<TEntity, long>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public double? Average(Expression<Func<TEntity, long?>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public double Average(Expression<Func<TEntity, double>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public double? Average(Expression<Func<TEntity, double?>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public decimal Average(Expression<Func<TEntity, decimal>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public decimal? Average(Expression<Func<TEntity, decimal?>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public float Average(Expression<Func<TEntity, float>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public float? Average(Expression<Func<TEntity, float?>> selector)
        {
            return this.Queryable.Average(selector);
        }

        public int Max(Expression<Func<TEntity, int>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public int? Max(Expression<Func<TEntity, int?>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public int? Max(Expression<Func<TEntity, int?>> selector, int defaultValue)
        {
            return this.Queryable.Select(selector).Max(o => (int?)(o ?? defaultValue));
        }

        public long Max(Expression<Func<TEntity, long>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public long? Max(Expression<Func<TEntity, long?>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public long? Max(Expression<Func<TEntity, long?>> selector, long defaultValue)
        {
            return this.Queryable.Select(selector).Max(o => (long?)(o ?? defaultValue));
        }

        public double Max(Expression<Func<TEntity, double>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public double? Max(Expression<Func<TEntity, double?>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public double? Max(Expression<Func<TEntity, double?>> selector, double defaultValue)
        {
            return this.Queryable.Select(selector).Max(o => (double?)(o ?? defaultValue));
        }

        public decimal Max(Expression<Func<TEntity, decimal>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public decimal? Max(Expression<Func<TEntity, decimal?>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public decimal? Max(Expression<Func<TEntity, decimal?>> selector, decimal defaultValue)
        {
            return this.Queryable.Select(selector).Max(o => (decimal?)(o ?? defaultValue));
        }

        public float Max(Expression<Func<TEntity, float>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public float? Max(Expression<Func<TEntity, float?>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public float? Max(Expression<Func<TEntity, float?>> selector, float defaultValue)
        {
            return this.Queryable.Select(selector).Max(o => (float?)(o ?? defaultValue));
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return this.Queryable.Max(selector);
        }

        public int Sum(Expression<Func<TEntity, int>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public int? Sum(Expression<Func<TEntity, int?>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public long Sum(Expression<Func<TEntity, long>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public long? Sum(Expression<Func<TEntity, long?>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public double Sum(Expression<Func<TEntity, double>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public double? Sum(Expression<Func<TEntity, double?>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public decimal Sum(Expression<Func<TEntity, decimal>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public decimal? Sum(Expression<Func<TEntity, decimal?>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public float Sum(Expression<Func<TEntity, float>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public float? Sum(Expression<Func<TEntity, float?>> selector)
        {
            return this.Queryable.Sum(selector);
        }

        public List<TResult> GroupBy<TElement, TKey, TResult>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TElement>> elementSelector,
            Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
        {
            return this.Queryable.GroupBy(keySelector, elementSelector, resultSelector).ToList();
        }

        public Task<List<TResult>> GroupByAsync<TElement, TKey, TResult>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TElement>> elementSelector,
            Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
        {
            return this.Queryable.GroupBy(keySelector, elementSelector, resultSelector).ToListAsync();
        }

        public bool Any()
        {
            return this.Queryable.Any();
        }

        public Task<bool> AnyAsync()
        {
            return this.Queryable.AnyAsync();
        }

        public TResult CombineFlag<TResult>(Expression<Func<TEntity, TResult>> selector) where TResult : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TResult).IsEnum)
                throw new ArgumentException("TResult must be an enumerated type");

            return
                (TResult)
                    Convert.ChangeType(
                        this.Queryable.Select(selector)
                            .AsEnumerable()
                            .Select(v => Convert.ToInt64(v))
                            .Aggregate((long) 0, (v1, v2) => v1 | v2),
                        typeof (TResult));
        }

        public bool HasFlagCombined<TResult>(Expression<Func<TEntity, TResult>> selector, TResult flag)
            where TResult : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof (TResult).IsEnum)
                throw new ArgumentException("TResult must be an enumerated type");

            var iFlag = Convert.ToInt64(flag);
            return this.Queryable.Select(selector)
                .AsEnumerable()
                .Select(v => Convert.ToInt64(v))
                .Aggregate((long) 0, (v1, v2) => v1 | v2, v => (v & iFlag) == iFlag);
        }
    }
}