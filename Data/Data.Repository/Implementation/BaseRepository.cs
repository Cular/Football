// <copyright file="BaseRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Data.Repository.TypeHandlers;
    using Football.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;
    using Npgsql;

    /// <summary>
    /// The base implementation of repository.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class BaseRepository<T, TKey> : IRepository<T, TKey>
    where T : Entity<TKey>, new()
    {
        /// <summary>
        /// The slim
        /// </summary>
        private static readonly ReaderWriterLockSlim Slim = new ReaderWriterLockSlim();

        /// <summary>
        /// The schemas
        /// </summary>
        private static readonly Dictionary<Type, Schema> Schemas = new Dictionary<Type, Schema>();

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T, TKey}"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        internal IDbConnection Connection => new NpgsqlConnection(this.connectionString);

        /// <summary>
        /// Creates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Created entity
        /// </returns>
        public virtual async Task<T> CreateAsync(T entity) => await this.CreateQueryAsync(entity);

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwise false.
        /// </returns>
        public virtual async Task DeleteAsync(TKey key)
        {
            var schema = GetSchema(typeof(T));

            using (var connection = this.Connection)
            {
                var query = $"DELETE FROM {schema.SchemaName}.{schema.TableName} WHERE {schema.KeyName} = @Key";

                connection.Open();
                await connection.ExecuteAsync(query, new { Key = key });
                connection.Close();
            }
        }

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// true if entity has been successfully deleted, otherwise false.
        /// </returns>
        public async Task DeleteAsync(T entity)
        {
            var schema = GetSchema(typeof(T));

            var key = schema.Columns.Single(x => x.Name == schema.KeyName).Getter(entity);

            using (var connection = this.Connection)
            {
                var query = $"DELETE FROM {schema.SchemaName}.{schema.TableName} WHERE {schema.KeyName} = @Key";

                connection.Open();
                await connection.ExecuteAsync(query, new { Key = key });
                connection.Close();
            }
        }

        /// <summary>
        /// Gets the entity by the specified key asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// Found entity
        /// </returns>
        public virtual async Task<T> GetAsync(TKey key)
        {
            return await this.GetEntityAsync<T>(key);
        }

        /// <summary>
        /// Gets the entities asynchronously.
        /// </summary>
        /// <param name="where">The where statement.</param>
        /// <returns>
        /// The list of entities.
        /// </returns>
        public virtual async Task<List<T>> GetAllAsync(string where)
        {
            var schema = GetSchema(typeof(T));

            using (var connection = this.Connection)
            {
                var query = $"SELECT {string.Join(", ", schema.Columns.Select(p => $"{p.Name} as {p.Property}"))} FROM {schema.SchemaName}.{schema.TableName} WHERE {where.Replace("WHERE", string.Empty, StringComparison.OrdinalIgnoreCase)}";

                connection.Open();
                var result = (await connection.QueryAsync<T>(query)).ToList();
                connection.Close();

                return result;
            }
        }

        /// <summary>
        /// Gets the entities asynchronously.
        /// </summary>
        /// <returns>
        /// The list of entities.
        /// </returns>
        public virtual async Task<List<T>> GetAllAsync()
        {
            var schema = GetSchema(typeof(T));

            using (var connection = this.Connection)
            {
                var query = $"SELECT {string.Join(", ", schema.Columns.Select(p => $"{p.Name} as {p.Property}"))} FROM {schema.SchemaName}.{schema.TableName}";

                connection.Open();
                var result = (await connection.QueryAsync<T>(query)).ToList();
                connection.Close();

                return result;
            }
        }

        /// <summary>
        /// Updates the specified key asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Updated entity
        /// </returns>
        public virtual async Task<T> UpdateAsync(T entity) => await this.UpdateQueryAsync(entity);

        /// <summary>
        /// Creates many sntities asynchronously.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>
        /// Void.
        /// </returns>
        public virtual async Task CreateManyAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                await this.CreateAsync(entity);
            }
        }

        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The schema.</returns>
        /// <exception cref="ArgumentException">The argument exception.</exception>
        protected static Schema GetSchema(Type type)
        {
            try
            {
                Slim.EnterUpgradeableReadLock();
                if (Schemas.ContainsKey(type))
                {
                    return Schemas[type];
                }

                try
                {
                    Slim.EnterWriteLock();
                    var schema = new Schema();
                    var attr = type.GetCustomAttribute<TableAttribute>();
                    if (attr == null)
                    {
                        throw new ArgumentException($"Type {type.FullName} is not a model type");
                    }

                    schema.TableName = attr.Name;
                    schema.SchemaName = attr.Schema;
                    schema.Columns = new List<Column>();
                    foreach (var property in type.GetProperties())
                    {
                        var c = new Column();

                        var colInfo = property.GetCustomAttribute<ColumnAttribute>();
                        if (colInfo == null)
                        {
                            continue;
                        }

                        c.Name = colInfo.Name;
                        if (property.GetCustomAttribute<KeyAttribute>() != null)
                        {
                            schema.KeyName = c.Name;
                        }

                        c.Setter = (entity, value) => { property.SetValue(entity, value); };
                        c.Getter = entity => property.GetValue(entity);
                        c.Property = property.Name;
                        schema.Columns.Add(c);
                    }

                    Schemas.Add(type, schema);
                    return schema;
                }
                finally
                {
                    Slim.ExitWriteLock();
                }
            }
            finally
            {
                Slim.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Prepares the query.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Insert query
        /// </returns>
        protected StringBuilder PrepareInsertQuery<TEntity>(TEntity entity, out DynamicParameters parameters)
        {
            var schema = GetSchema(typeof(TEntity));
            var query = new StringBuilder(1000);

            query.Append($"INSERT INTO {schema.SchemaName}.{schema.TableName} ");
            query.Append("(" + string.Join(",", schema.Columns.Select(column => column.Name)) + ")");
            query.Append(" VALUES ");
            query.Append("(" + string.Join(",", schema.Columns.Select(column => "@" + column.Name)) + ")");

            parameters = new DynamicParameters();
            var dynamicParameters = parameters;
            schema.Columns.ForEach(column => dynamicParameters.Add(column.Name, column.Getter(entity)));
            return query;
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="where">The where.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The list of entities.</returns>
        protected virtual async Task<List<TEntity>> GetAsync<TEntity>(string where, object parameters)
        {
            var schema = GetSchema(typeof(TEntity));

            using (var connection = this.Connection)
            {
                var query = $"SELECT {string.Join(", ", schema.Columns.Select(p => $"{p.Name} as {p.Property}"))} FROM {schema.SchemaName}.{schema.TableName} {where}";

                connection.Open();
                var result = await connection.QueryAsync<TEntity>(query, parameters);
                connection.Close();

                return result.ToList();
            }
        }

        /// <summary>
        /// Gets the entity asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The entity.</returns>
        protected async Task<TEntity> GetEntityAsync<TEntity>(TKey key)
        {
            var schema = GetSchema(typeof(TEntity));

            using (var connection = this.Connection)
            {
                var query =
                    $"SELECT {string.Join(", ", schema.Columns.Select(p => $"{p.Name} as {p.Property}"))} FROM {schema.SchemaName}.{schema.TableName} WHERE {schema.KeyName} = @Key";

                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<TEntity>(query, new { Key = key });
                connection.Close();

                return result;
            }
        }

        /// <summary>
        /// Updates the query asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The updated entity.</returns>
        protected async Task<TEntity> UpdateQueryAsync<TEntity>(TEntity entity)
            where TEntity : Entity<TKey>
        {
            var schema = GetSchema(typeof(TEntity));

            using (var connection = this.Connection)
            {
                var query = new StringBuilder(1000);

                query.Append($"UPDATE {schema.SchemaName}.{schema.TableName} SET ");
                query.Append(string.Join(",", schema.Columns.Where(x => x.Name != schema.KeyName).Select(column => $"{column.Name} = @{column.Name}")));
                query.Append($" WHERE {schema.KeyName} = @Key");

                var parameters = new DynamicParameters();
                parameters.Add("Key", entity.Id);
                schema.Columns.ForEach(column => parameters.Add(column.Name, column.Getter(entity)));

                try
                {
                    connection.Open();
                    var rowCount = await connection.ExecuteAsync(query.ToString(), parameters);
                    connection.Close();

                    if (rowCount < 1)
                    {
                        throw new NotFoundException();
                    }
                }
                catch (PostgresException postgresException)
                {
                    if (postgresException.SqlState == "23505")
                    {
                        throw new DuplicateException(postgresException.Detail);
                    }

                    if (postgresException.SqlState == "23503")
                    {
                        throw new NotFoundException($"Violates foreign key constraint {postgresException.ConstraintName}");
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// Creates the query asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The created entity.</returns>
        protected async Task<TEntity> CreateQueryAsync<TEntity>(TEntity entity)
        {
            using (var connection = this.Connection)
            {
                var query = this.PrepareInsertQuery(entity, out var parameters);

                try
                {
                    connection.Open();
                    await connection.ExecuteAsync(query.ToString(), parameters);
                    connection.Close();
                }
                catch (PostgresException postgresException)
                {
                    if (postgresException.SqlState == "23505")
                    {
                        throw new DuplicateException(postgresException.Detail);
                    }

                    if (postgresException.SqlState == "23503")
                    {
                        throw new NotFoundException($"Violates foreign key constraint {postgresException.ConstraintName}");
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

            return entity;
        }
    }
}
