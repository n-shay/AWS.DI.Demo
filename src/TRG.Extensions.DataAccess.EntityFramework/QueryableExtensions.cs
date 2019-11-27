namespace TRG.Extensions.DataAccess.EntityFramework
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, int startAt, int maxResults)
        {
            return query
                .Skip(startAt)
                .Take(maxResults);
        }

        public static IQueryable<T> PagedWithCount<T>(this IQueryable<T> query, int startAt, int maxResults, out int total)
        {
            // TODO: Review this. Results in two queries.
            total = query.Count();

            return query.Paged(startAt, maxResults);
        }

        public static IQueryable<T> ExcludeDeleted<T>(this IQueryable<T> query) where T : IEntity
        {
            return query.Where(ExcludeDeletedExpression<T>());
        }

        public static Expression<Func<T, bool>> ExcludeDeletedExpression<T>() where T : IEntity
        {
            var parameterExpression = Expression.Parameter(typeof(T));
            var propertyExpression = Expression.Property(parameterExpression,
                "IsDeleted");
            var notExpression = Expression.Not(propertyExpression);
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(notExpression,
                parameterExpression);

            return lambdaExpression;
        }

        public static IQueryable<T> DefineIncludes<T>(
            this IQueryable<T> query,
            Func<IQueryable<T>, IQueryable<T>> defineIncludesAction) where T : IEntity
        {
            return defineIncludesAction(query);
        }
    }
}
