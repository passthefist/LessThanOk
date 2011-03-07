using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;

namespace LessThanOk.GameData
{
	public class MasterGameWorld
	{
		private List<Change> changeList;

        public void update(GameTime elps, Queue<Command> commands) { }
		public List<Change> getChanges(){return changeList;}

	}
}
