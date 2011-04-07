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
        private WeaponType type;

        public WeaponType Type
        {
            get { return type; }
            private set { type = value; }
        }

        static Weapon()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(Weapon).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }

        protected Weapon() : base() { }

        internal Weapon(WeaponType t)
            : base()
        {
            type = t;
        }

        public Weapon(Weapon w)
            : base()
        {
            this.type = w.Type;
        }

        /*
            WeaponFire fire()
            {
                return new WeaponFire(this);
            }
        */
    }
}