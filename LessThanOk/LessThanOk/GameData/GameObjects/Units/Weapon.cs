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

        private Warhead warhead;

        public Warhead _Warhead
        {
            get { return warhead; }
            private set { warhead = value; }
        }

        private Projectile projectile;

        public Projectile _Projectile
        {
            get { return projectile; }
            private set { projectile = value; }
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

        internal Weapon(WeaponType t, Warhead wd, Projectile pd)
            : base()
        {
            type = t;
            warhead = wd;
            projectile = pd;
        }

        public Weapon(Weapon w)
            : base()
        {
            this.type = w.Type;
            this.warhead = w._Warhead;
            this.projectile = w._Projectile;
        }

        /*
            WeaponFire fire()
            {
                return new WeaponFire(this);
            }
        */
    }
}