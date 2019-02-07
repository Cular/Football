// <copyright file="GameStateEnum.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    /// <summary>
    /// State of game.
    /// </summary>
    public enum GameStateEnum
    {
        /// <summary>
        /// Game is public and active.
        /// Vote = true,
        /// Chat = true,
        /// Delete = true.
        /// Close = true.
        /// </summary>
        Open = 0,

        /// <summary>
        /// Game can be closed, deleted or for chating.
        /// Vote = false,
        /// Chat = true,
        /// Delete = true.
        /// Close = true.
        /// </summary>
        ChatOnly = 1,

        /// <summary>
        /// Game is closed.
        /// Vote = false,
        /// Chat = false,
        /// Delete = false.
        /// Close = false.
        /// </summary>
        Closed = 2,
    }
}
