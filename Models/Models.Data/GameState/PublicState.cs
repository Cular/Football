// <copyright file="PublicState.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    /// <summary>
    /// The public state of game.
    /// </summary>
    public class PublicState : State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicState"/> class.
        /// </summary>
        public PublicState()
            : base(true, true, true)
        {
        }
    }
}
