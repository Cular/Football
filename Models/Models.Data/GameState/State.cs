// <copyright file="State.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The state of game.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="canVote">Flag of voting</param>
        /// <param name="canChat">Flag of chating</param>
        /// <param name="canDelete">Flag of deleting</param>
        /// <param name="canClose">Flag of closing</param>
        public State(bool canVote, bool canChat, bool canDelete, bool canClose)
        {
            this.CanVote = canVote;
            this.CanChat = canChat;
            this.CanDelete = canDelete;
            this.CanClose = canClose;
        }

        /// <summary>
        /// Gets or sets a value indicating whether признак можно ли голосовать.
        /// </summary>
        public bool CanVote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether признак можно ли переписываться.
        /// </summary>
        public bool CanChat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether признак может ли быть игра удалена.
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can close.
        /// </summary>
        public bool CanClose { get; set; }

        /// <summary>
        /// Tries the state of the change.
        /// </summary>
        /// <param name="newState">The new state.</param>
        /// <param name="game">The game.</param>
        /// <returns>Successness of result.</returns>
        public abstract bool TryChangeState(State newState, Game game);
    }
}
