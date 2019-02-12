// <copyright file="OpenState.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    /// <summary>
    /// The public state of game.
    /// </summary>
    public class OpenState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenState"/> class.
        /// </summary>
        public OpenState()
            : base(true, true, true, true)
        {
        }

        /// <inheritdoc/>
        public override bool TryChangeState(State newState, Game game)
        {
            if (newState is ChatOnlyState || newState is ClosedState)
            {
                game.State = newState;
                return true;
            }

            return false;
        }
    }
}
