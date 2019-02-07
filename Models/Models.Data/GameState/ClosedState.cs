// <copyright file="ClosedState.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The closed state of game.
    /// </summary>
    public class ClosedState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClosedState"/> class.
        /// </summary>
        public ClosedState()
            : base(false, false, false, false)
        {
        }

        /// <inheritdoc/>
        public override bool TryChangeState(State newState, Game game)
        {
            return false;
        }
    }
}
