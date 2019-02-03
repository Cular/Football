// <copyright file="ReadOnlyState.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The read only state of game.
    /// </summary>
    public class ReadOnlyState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyState"/> class.
        /// </summary>
        public ReadOnlyState()
            : base(false, true, true)
        {
        }
    }
}
