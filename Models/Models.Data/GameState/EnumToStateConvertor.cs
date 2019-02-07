// <copyright file="EnumToStateConvertor.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.GameState
{
    /// <summary>
    /// Convertor for game state from and to enumeration state.
    /// </summary>
    public static class EnumToStateConvertor
    {
        /// <summary>
        /// Converts enum to state.
        /// </summary>
        /// <param name="stateEnumeration">The enum state.</param>
        /// <returns>The state</returns>
        public static State ToState(this GameStateEnum stateEnumeration)
        {
            switch (stateEnumeration)
            {
                case GameStateEnum.Open:
                    return new OpenState();

                case GameStateEnum.ChatOnly:
                    return new ChatOnlyState();

                case GameStateEnum.Closed:
                default:
                    return new ClosedState();
            }
        }

        /// <summary>
        /// Converts state to enum
        /// </summary>
        /// <param name="state">The state of game.</param>
        /// <returns>The enum state.</returns>
        public static GameStateEnum ToEnum(this State state)
        {
            switch (state)
            {
                case OpenState s:
                    return GameStateEnum.Open;

                case ChatOnlyState s:
                    return GameStateEnum.ChatOnly;

                case ClosedState s:
                default:
                    return GameStateEnum.Closed;
            }
        }
    }
}
