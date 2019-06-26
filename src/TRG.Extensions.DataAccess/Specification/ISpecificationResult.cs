using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TRG.Extensions.DataAccess.Specification
{
    /// <summary>
    /// Generic domain specification result interface that 
    /// contains methods common for filtering the result.
    /// </summary>
    /// <remarks>
    /// If the specification model uses lazy loading using <see cref="System.Linq.IQueryable{T}"/>, 
    /// this is the place where the query is translated and executed by calling for example
    /// <see cref="ToList"/> or <see cref="Single"/> methods.
    /// 
    /// The interface is very similar to IQueryable interface. It is basically
    /// the wrapper over the IQueryable if used under the hood. The main goal is to
    /// provide set of common methods used by all specifications. Other methods that
    /// normally IQueryable supports should be implemented in <see cref="ISpecification{TEntity}"/>
    /// interface using strong domain names of the method (e.g. joins, grouping, aggregate functions).
    /// 
    /// Note it is perfectly fine if from domain perspective the specification already
    /// uses the functionality of this specification result. The specification should represents
    /// one query that can be customized using fluent interface but in general it should be
    /// strongly domain focused.
    /// </remarks>
    public interface ISpecificationResult<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Takes given count of domain objects.
        /// </summary>
        ISpecificationResult<TEntity> Take(int count);

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns 
        /// the remaining elements.
        /// </summary>
        /// <param name="count">The number of elements to skip before returning 
        /// the remaining elements.</param>
        ISpecificationResult<TEntity> Skip(int count);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the function that is 
        /// represented by keySelector.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        ISpecificationResult<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by the function that is 
        /// represented by keySelector.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        ISpecificationResult<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        ISpecificationResult<TEntity> Distinct();

        /// <summary>
        /// Gets the list of domain objects the specification represents.
        /// </summary>
        List<TEntity> ToList();

        /// <summary>
        /// Gets the list of domain objects the specification represents.
        /// </summary>
        Task<List<TEntity>> ToListAsync();

        List<TResult> ToList<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null);

        Task<List<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null);

        List<TResult> ToDistinctList<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null);

        Task<List<TResult>> ToDistinctListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, int? maxResults = null);

        /// <summary>
        /// Gets single domain object the specification represents.
        /// </summary>
        TEntity Single();

        /// <summary>
        /// Gets single domain object the specification represents.
        /// </summary>
        Task<TEntity> SingleAsync();

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; 
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        TEntity SingleOrDefault();

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; 
        /// this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        Task<TEntity> SingleOrDefaultAsync();

        /// <summary>
        /// Gets the first domain object the specification represents.
        /// </summary>
        TEntity First();

        /// <summary>
        /// Gets the first domain object the specification represents.
        /// </summary>
        Task<TEntity> FirstAsync();

        /// <summary>
        /// Gets the first domain object the specification represents. If none, it returns the default value.
        /// </summary>
        TEntity FirstOrDefault();

        /// <summary>
        /// Gets the first domain object the specification represents. If none, it returns the default value.
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// Returns the number of elements the specification represents.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Returns the number of elements the specification represents.
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        int DistinctCount<T>(Expression<Func<TEntity, T>> selector);

        Task<int> DistinctCountAsync<T>(Expression<Func<TEntity, T>> selector);

        double Average(Expression<Func<TEntity, int>> selector);

        double? Average(Expression<Func<TEntity, int?>> selector);

        double Average(Expression<Func<TEntity, long>> selector);

        double? Average(Expression<Func<TEntity, long?>> selector);

        double Average(Expression<Func<TEntity, double>> selector);

        double? Average(Expression<Func<TEntity, double?>> selector);

        decimal Average(Expression<Func<TEntity, decimal>> selector);

        decimal? Average(Expression<Func<TEntity, decimal?>> selector);

        float Average(Expression<Func<TEntity, float>> selector);

        float? Average(Expression<Func<TEntity, float?>> selector);

        int Max(Expression<Func<TEntity, int>> selector);

        int? Max(Expression<Func<TEntity, int?>> selector);

        /// <summary>
        /// Returns the maximum value according to the property selected, all nulls are treated as <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        int? Max(Expression<Func<TEntity, int?>> selector, int defaultValue);

        long Max(Expression<Func<TEntity, long>> selector);

        long? Max(Expression<Func<TEntity, long?>> selector);

        /// <summary>
        /// Returns the maximum value according to the property selected, all nulls are treated as <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        long? Max(Expression<Func<TEntity, long?>> selector, long defaultValue);

        double Max(Expression<Func<TEntity, double>> selector);

        double? Max(Expression<Func<TEntity, double?>> selector);

        /// <summary>
        /// Returns the maximum value according to the property selected, all nulls are treated as <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        double? Max(Expression<Func<TEntity, double?>> selector, double defaultValue);

        decimal Max(Expression<Func<TEntity, decimal>> selector);

        decimal? Max(Expression<Func<TEntity, decimal?>> selector);

        /// <summary>
        /// Returns the maximum value according to the property selected, all nulls are treated as <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        decimal? Max(Expression<Func<TEntity, decimal?>> selector, decimal defaultValue);

        float Max(Expression<Func<TEntity, float>> selector);

        float? Max(Expression<Func<TEntity, float?>> selector);

        /// <summary>
        /// Returns the maximum value according to the property selected, all nulls are treated as <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        float? Max(Expression<Func<TEntity, float?>> selector, float defaultValue);

        TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);

        int Sum(Expression<Func<TEntity, int>> selector);

        int? Sum(Expression<Func<TEntity, int?>> selector);

        long Sum(Expression<Func<TEntity, long>> selector);

        long? Sum(Expression<Func<TEntity, long?>> selector);

        double Sum(Expression<Func<TEntity, double>> selector);

        double? Sum(Expression<Func<TEntity, double?>> selector);

        decimal Sum(Expression<Func<TEntity, decimal>> selector);

        decimal? Sum(Expression<Func<TEntity, decimal?>> selector);

        float Sum(Expression<Func<TEntity, float>> selector);

        float? Sum(Expression<Func<TEntity, float?>> selector);

        List<TResult> GroupBy<TElement, TKey, TResult>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TElement>> elementSelector,
            Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector);

        Task<List<TResult>> GroupByAsync<TElement, TKey, TResult>(
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, TElement>> elementSelector,
            Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector);

        /// <summary>
        /// Returns whether the specification provided contains any results.
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// Returns whether the specification provided contains any results.
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Returns a bitwise OR (operator |) of the values the specification represents.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        TResult CombineFlag<TResult>(Expression<Func<TEntity, TResult>> selector)
            where TResult : struct, IConvertible, IComparable, IFormattable;

        /// <summary>
        /// Combines all values that the specification represents in a bitwise OR (operator |) and returns whether they satisfy the flag provided.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        bool HasFlagCombined<TResult>(Expression<Func<TEntity, TResult>> selector, TResult flag)
            where TResult : struct, IConvertible, IComparable, IFormattable;
    }
}