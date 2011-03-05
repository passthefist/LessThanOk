using System;
using System.Collections;

namespace GameData
{
	public class MasterGameWorld
	{
		private List<Change> changeList;
	
		public void update(TimeSpan elps){}
		public List<Change> getChanges(){return changeList;}
	}
}
