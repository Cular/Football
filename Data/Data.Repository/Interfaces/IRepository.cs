// <copyright file="IRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Data;

    /// <summary>
    /// Base repository interface.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IRepository<T, TKey>
        where T : Entity<TKey>
    {
        /// <summary>
        /// Gets the entity by the specified key asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Found entity</returns>
        Task<T> GetAsync(TKey key);

        /// <summary>
        /// Gets the entities asynchronously.
        /// </summary>
        /// <returns>The list of entities.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>List of entities</returns>
        Task<List<T>> GetAllAsync(Func<T, bool> predicate);

        /// <summary>
        /// Creates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Created entity</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates the specified key asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Updated entity</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwize false.
        /// </returns>
        Task<bool> DeleteAsync(TKey key);

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwize false.
        /// </returns>
        Task<bool> DeleteAsync(T entity);
    }
}
