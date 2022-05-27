using System.Collections.Generic;
using System.Linq;

namespace TechApp
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        T Create(T entity);

        IEnumerable<T> Create(IEnumerable<T> entities);

        /// <summary>
        /// Gets an Queryable set of entities
        /// </summary>
        /// <returns>The Queryable set of entities</returns>
        IQueryable<T> Get();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        T Update(T entity);

        IEnumerable<T> Update(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
    }
}
