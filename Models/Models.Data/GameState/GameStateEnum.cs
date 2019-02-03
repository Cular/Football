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
        /// Game is public and active
        /// </summary>
        Public = 0,

        /// <summary>
        /// Game is closed. Allowed chating.
        /// </summary>
        Closed = 1,

        /// <summary>
        /// Game can be deleted or for chating.
        /// </summary>
        ReadOnly = 2
    }
}
