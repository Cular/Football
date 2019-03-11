// <copyright file="GameStateHandler.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.TypeHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using Dapper;
    using Models.Data.GameState;

    /// <summary>
    /// Game state auto mapping handler for Dapper.
    /// </summary>
    public class GameStateHandler : SqlMapper.TypeHandler<State>
    {
        /// <summary>
        /// Parse a database value back to a typed value
        /// </summary>
        /// <param name="value">The value from the database</param>
        /// <returns>
        /// The typed value
        /// </returns>
        public override State Parse(object value)
        {
            return ((GameStateEnum)value).ToState();
        }

        /// <summary>
        /// Assign the value of a parameter before a command executes
        /// </summary>
        /// <param name="parameter">The parameter to configure</param>
        /// <param name="value">Parameter value</param>
        public override void SetValue(IDbDataParameter parameter, State value)
        {
            parameter.Value = (int)value.ToEnum();
        }
    }
}
