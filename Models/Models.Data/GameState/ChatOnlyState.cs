// <copyright file="ChatOnlyState.cs" company="Yarik Home">
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
    public class ChatOnlyState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatOnlyState"/> class.
        /// </summary>
        public ChatOnlyState()
            : base(false, true, true, true)
        {
        }

        /// <inheritdoc/>
        public override bool TryChangeState(State newState, Game game)
        {
            if (newState is ClosedState)
            {
                game.State = newState;
                return true;
            }

            return false;
        }
    }
}
