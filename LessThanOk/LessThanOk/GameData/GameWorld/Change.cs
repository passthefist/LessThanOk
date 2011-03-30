using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects.Tiles;

namespace LessThanOk.GameData.GameWorld
{
    public class Change
    {
        protected TimeSpan tstamp;
        public TimeSpan TimeStamp
        { get; private set;}
    }

    public class AdditionChange : Change
    {
        private GameObject added;
        public GameObject AddedObject
        { get { return added; } private set { added = value; } }

        private GameObject adder;
        public GameObject ParentObject
        { get { return adder; } private set { added = value; } }

        public AdditionChange(TimeSpan time, GameObject child, GameObject parent)
        {
            tstamp = time;
            added = child;
            adder = parent;
        }
    }

    public class RemovalChange : Change
    {
        private GameObject removed;
        public GameObject RemovedObject
        { get { return removed; } private set { removed = value; } }   

        public RemovalChange(TimeSpan time, GameObject rem)
        {
            tstamp = time;
            removed = rem;
        }
    }

    public class SetValueChange : Change
    {
        private GameObject target;
        public GameObject Target
        { get; private set; }

        private KeyValuePair<UInt16, UInt32> setPair;
        public KeyValuePair<UInt16,UInt32> SetPair
        { get { return setPair; } private set { setPair = value; } }

        public SetValueChange(TimeSpan time, GameObject targ, KeyValuePair<UInt16, UInt32> pair)
        {
            tstamp = time;
            target = targ;
            setPair = pair;
        }
    }
}
