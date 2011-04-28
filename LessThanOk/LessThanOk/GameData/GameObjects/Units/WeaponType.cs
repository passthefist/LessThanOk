using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameObjects.Units
{
    public class WeaponType : GameObjectType
    {

        private WarheadType warhead;

        public WarheadType _Warhead
        {
            get { return warhead; }
            set { warhead = value; }
        }

        static WeaponType()
        {
            AgnosticObject.initFieldMaps(typeof(WeaponType));
        }

        public WeaponType(WarheadType warhead)
        {
            init(warhead);
        }

        public WeaponType(string warhead)
        {
            WarheadType wt = (WarheadType)GameObjectFactory.The.getType(warhead);

            init(wt);
        }

        private void init(WarheadType war)
        {
            warhead = war;

            protoType = new Weapon(this);
        }

        override public GameObject create()
        {
            return new Weapon((Weapon)protoType);
        }
    }
}