using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData
{
    public sealed class PlayerList
    {
        private static readonly PlayerList the = new PlayerList();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static PlayerList()
        {
        }

        public static PlayerList The
        {
            get
            {
                return the;
            }
        }

        private List<Player> players;

        private PlayerList()
        {
            players = new List<Player>();
        }

        public void addPlayer(Player p)
        {
            players.Add(p);
        }

        public Player getPlayer(int ID)
        {
            foreach (Player p in players)
            {
                if (p.PlayerID == ID)
                {
                    return p;
                }
            }
            return null;
        }

        public void update(GameTime elps)
        {
            foreach (Player p in players)
            {
                p.update(elps);
            }
        }
    }
}