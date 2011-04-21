using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.States.Events.Args;

namespace LessThanOk.States.Events
{
    public sealed class StateChangeEvents
    {
        public event EventHandler<HomeStateEventArgs> HomeState;
        public event EventHandler<LobbyStateEventArgs> LobbyState;
        public event EventHandler<GameStateEventArgs> GameState;
        public event EventHandler<PostGameStateEventArgs> PostGameState;

        static readonly StateChangeEvents the = new StateChangeEvents();
        public static StateChangeEvents The { get { return the; } }
        static StateChangeEvents() { }

        public void TiggerHomeState(object sender, HomeStateEventArgs args)
        {
            HomeState.Invoke(sender, args);
        }
        public void TiggerLobbyState(object sender, LobbyStateEventArgs args)
        {
            LobbyState.Invoke(sender, args);
        }
        public void TiggerGameState(object sender, GameStateEventArgs args)
        {
            GameState.Invoke(sender, args);
        }
        public void TiggerPostGameState(object sender, PostGameStateEventArgs args)
        {
            PostGameState.Invoke(sender, args);
        }
    }
}
