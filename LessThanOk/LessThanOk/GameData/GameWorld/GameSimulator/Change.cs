using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects.Tiles;
using Microsoft.Xna.Framework;

namespace LessThanOk.GameData.GameWorld
{
    public class Change
    {
        protected long tstamp;
        public long TimeStamp
        { get; private set;}
    }

    public class AdditionChange : Change
    {
        private GameObject added;
        private GameObjectType type;

        public GameObjectType Type 
        { get { return type; } set { type = value; } }

        public GameObject AddedObject
        { get { return added; } private set { added = value; } }

        private ActiveGameObject adder;
        public ActiveGameObject ParentObject
        { get { return adder; } private set { adder = value; } }

        private Player owner;
        public Player Owner
        {
            get { return owner; }
        }

        public AdditionChange(long time, ActiveGameObject child, ActiveGameObject parent, Player own)
        {
            tstamp = time;
            added = child;
            adder = parent;
            owner = own;
            // TODO: Fix
            type = child.Type;
        }
    }

    public class IdleChange : Change
    {
        private Unit idled;
        public Unit Idled
        { get { return idled; }}

        public IdleChange(long time, Unit ide)
        {
            tstamp = time;
            idled = ide;
        }
    }

    public class RemovalChange : Change
    {
        private ActiveGameObject removed;
        public ActiveGameObject RemovedObject
        { get { return removed; } private set { removed = value; } }   

        public RemovalChange(long time, ActiveGameObject rem)
        {
            tstamp = time;
            removed = rem;
        }
    }

    public class SetValueChange : Change
    {
        private UInt16 target;
        public UInt16 Target
        {
            get { return target; }
        }

        private UInt16 key;
        public UInt16 Key
        {
            get { return key; }
        }

        private UInt32 value;
        public UInt32 Value
        {
            get { return value; }
        }

        public SetValueChange(long time, UInt16 targ, UInt16 k, UInt32 v)
        {
            tstamp = time;
            target = targ;
            key = k;
            value = v;
        }
    }

    public class MoveChange : Change
    {
        private ActiveGameObject moved;
        public ActiveGameObject Moved
        {
            get { return moved; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public MoveChange(long time, ActiveGameObject obj, Vector2 pos)
        {
            tstamp = time;
            moved = obj;
            position = pos;
        }
    }

    public class AttackChange : Change
    {
        private Unit attacker;
        public Unit Attacker
        {
            get { return attacker; }
        }

        private ActiveGameObject newTarget;
        public ActiveGameObject Target
        {
            get { return newTarget; }
        }

        public AttackChange(long time, Unit atk, ActiveGameObject targ)
        {
            tstamp = time;
            attacker = atk;
            newTarget = targ;
        }
    }

}
