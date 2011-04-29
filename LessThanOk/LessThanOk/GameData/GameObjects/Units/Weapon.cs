using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameObjects.Units
{
    public class Weapon : GameObject
    {
        static Weapon()
        {
            AgnosticObject.initFieldMaps(typeof(Weapon));
        }

        protected Weapon() : base() { }

        internal Weapon(WeaponType t)
            : base()
        {
            Type = t;
        }

        public Weapon(Weapon w)
            : base()
        {
            this.Type = w.Type;
        }

        public bool canFire()
        {
            return false;
        }

        public void reload(GameTime elps)
        {
        }
    }
}