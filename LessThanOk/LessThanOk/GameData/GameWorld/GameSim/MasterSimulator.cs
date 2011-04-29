using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Network.Commands;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData.GameObjects;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands.Decorators;

namespace LessThanOk.GameData.GameWorld.GameSim
{
    class MasterSimulator : GameSimulator
    {
        //Changes to be done
        protected List<Command> changes;

        //History of set values
        protected Dictionary<KeyValuePair<UInt16, UInt16>, UInt32> setChanges;

        //Set value for health
        protected Dictionary<Unit, ushort> battleChanges;

        public MasterSimulator() : base()
        {
            changes = new List<Command>();
            setChanges = new Dictionary<KeyValuePair<ushort, ushort>, uint>();
            battleChanges = new Dictionary<Unit, ushort>();
        }
        public List<Command> collectChanges()
        {
            //iterate over sets
            return changes; 
        }

        protected override void preUpdate(GameTime elps)
        {
            changes.Clear();
            battleChanges.Clear();
            setChanges.Clear();
        }

        protected ActiveGameObject aquireTarget(Vector2 point)
        {
            return null;
        }

        protected override void postUpdate(GameTime elps)
        {
            doBattle(elps);
            pruneDeadUnits();
        }

        protected override Unit createNewUnit(ushort toAddID, ushort type, ushort adderID)
        {
            return (Unit)GameObjectFactory.The.createGameObject(type);
        }

        protected override void removeObjectFromWorld(Unit toRemove)
        {
            units.Remove(toRemove);
        }

        /// <summary>
        /// Do battle simulation. This function will act on all aggressive
        /// units. It will:
        /// 1. Aquire a target for the unit if it does not already have one.
        /// 2. Make the target null if the health is lessthan or equal to zero
        /// 3. If possible, fire the unit's weapon on the target.
        /// 4. Apply damage to the target
        /// 5. If the target's health drops below zero, flag for removal.
        /// </summary>
        /// <param name="elps"></param>
        protected void doBattle(GameTime elps)
        {
            foreach (Unit u in units)
            {
                if (u.isAggressive())
                {
                    if (u.Target == null)
                    {
                        u.setTarget(aquireTarget(u.getPosition()));
                        if (u.Target == null)
                        {
                            continue;
                        }
                    }
                    if (u.Target.Health > 0)
                    {
                        if (u.canFireWeapon())
                        {
                            //fire weapon
                        }
                    }
                    else
                    {
                        u.idle();
                    }
                }
            }
        }

        /// <summary>
        /// Iterate over the set of units flagged for removal and:
        /// 1. remove them from the simulators set of units
        /// 2. post the change to the list
        /// 3. call unit.removeObject()
        /// </summary>
        private void pruneDeadUnits()
        {
        }

        protected override void handleMoveCommand(Command moveCommand)
        {
            MoveDecorator mov = new MoveDecorator(moveCommand);
            Vector2 position = new Vector2((float)mov.X, (float)mov.Y);

            Unit u = (Unit)GameObjectFactory.The.getGameObject(mov.UnitID);

            u.forceFinishAction();
            u.moveTo(position);
        }

        protected override void makeUnitIdle(Unit u)
        {
            u.forceFinishAction();
            u.idle();


        }
    }
}
