// <copyright file="BaseRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The base implementation of repository.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class BaseRepository<T, TKey> : IRepository<T, TKey>
        where T : Entity<TKey>
    {
#pragma warning disable SA1401 // Fields must be private
                              /// <summary>
                              /// The context
                              /// </summary>
        protected readonly FootballContext context;
#pragma warning restore SA1401 // Fields must be private

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T, TKey}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public BaseRepository(FootballContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Created entity
        /// </returns>
        public async Task<T> CreateAsync(T entity)
        {
            var entry = await this.context.Set<T>().AddAsync(entity);
            await this.context.SaveChangesAsync();

            return entry.Entity;
        }

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwize false.
        /// </returns>
        public async Task<bool> DeleteAsync(TKey key)
        {
            T entity = await this.GetAsync(key);
            if (entity == null)
            {
                return false;
            }

            return await this.DeleteAsync(entity);
        }

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwize false.
        /// </returns>
        public async Task<bool> DeleteAsync(T entity)
        {
            bool result = this.context.Set<T>().Remove(entity).State == EntityState.Deleted;
            await this.context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Gets the entities asynchronously.
        /// </summary>
        /// <returns>
        /// The list of entities.
        /// </returns>
        public async Task<List<T>> GetAllAsync()
        {
            var result = await this.context.Set<T>().ToListAsync();
            return result;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The list of entities.</returns>
        public async Task<List<T>> GetAllAsync(Func<T, bool> predicate)
        {
            var result = await (this.context.Set<T>().Where(predicate) as IQueryable<T>).ToListAsync();
            return result;
        }

        /// <summary>
        /// Gets the entity by the specified key asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// Found entity
        /// </returns>
        public async Task<T> GetAsync(TKey key)
        {
            var result = await this.context.Set<T>().FindAsync(key);
            return result;
        }

        /// <summary>
        /// Updates the specified key asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Updated entity
        /// </returns>
        public async Task<T> UpdateAsync(T entity)
        {
            var dbEntity = await this.GetAsync(entity.Id);
            if (dbEntity == null)
            {
                return null;
            }

            this.context.Entry(dbEntity).CurrentValues.SetValues(entity);
            await this.context.SaveChangesAsync();

            return dbEntity;
        }
    }
}
