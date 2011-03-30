using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;

using LessThanOk.GameData.GameObjects;namespace LessThanOk.GameData.GameObjects.Units
{

    /// <summary>
    /// Type of warhead
    /// </summary>
    public class WarheadType : GameObjectType
    {
        /// <summary>
        /// Types of weapons
        /// </summary>
        public enum Types
        {
            NONE,
            BALlISTIC,
            PLASMA,
            ENERGY,
            LASER,
            CONCUSSIVE,
            CORROSIVE,
            FLAME,
            LIGHTNING,
            EXPLOSIVE,
            GASEOUS,
            BIO,
            MEDICAL,
            NUCLEAR,
            LARGE,
            POISON,
            SPIRIT,
            PIERCING,
            ICE,
            FREEZING,
            CYBER,
            QUANTUM,
            SONIC,
            PSYCHIC,
            FECAL
        };

        private byte damage;

        /// <summary>
        /// The one-time damage of this warhead
        /// </summary>
        public byte Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

        private byte dps;

        /// <summary>
        /// The damage over time
        /// </summary>
        public byte DPS
        {
            get
            {
                return dps;
            }
            set
            {
                dps = value;
            }
        }

        private byte size;

        /// <summary>
        /// The size of the warhead. Used with armor
        /// to determine armor damage.
        /// </summary>
        public byte Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        private byte splashDam;
        /// <summary>
        /// How much damage is done in the splash.
        /// Splah drops off quadratic by the distance
        /// </summary>
        public byte SplashDam
        {
            get
            {
                return splashDam;
            }
            set
            {
                splashDam = value;
            }
        }

        private byte splashRad;
        /// <summary>
        /// The radius of the splash that damage is done
        /// </summary>
        public byte SplashRad
        {
            get
            {
                return splashRad;
            }
            set
            {
                splashRad = value;
            }
        }

        private Types primary;
        /// <summary>
        /// The primary type. Used against armor's
        /// strong resistances.
        /// </summary>
        public Types Primary
        {
            get
            {
                return primary;
            }
            set
            {
                primary = value;
            }
        }

        private Types secondary;
        /// <summary>
        /// The secondary type. used agains armor's
        /// weak resistances.
        /// </summary>
        public Types Secondary
        {
            get
            {
                return secondary;
            }
            set
            {
                secondary = value;
            }
        }

        static WarheadType()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(WarheadType).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }

        /// <summary>
        /// Make a new WarheadType
        /// </summary>
        /// <param name="dam">
        /// The amount of damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="siz">
        /// The size. <see cref="System.Byte"/>
        /// </param>
        /// <param name="t">
        /// The primary type <see cref="Types"/>
        /// </param>
        public WarheadType(byte dam, byte siz, Types t)
        {
            damage = dam;
            size = siz;
            splashDam = 0;
            splashRad = 0;
            primary = t;
            secondary = WarheadType.Types.NONE;

            protoType = new Warhead(this);
        }

        /// <summary>
        /// Create a new warheadType
        /// </summary>
        /// <param name="dam">
        /// Damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="siz">
        /// size <see cref="System.Byte"/>
        /// </param>
        /// <param name="t">
        /// primary type <see cref="Types"/>
        /// </param>
        /// <param name="t2">
        /// secondary type <see cref="Types"/>
        /// </param>
        public WarheadType(byte dam, byte siz, Types t, Types t2)
        {
            damage = dam;
            size = siz;
            splashDam = 0;
            splashRad = 0;
            primary = t;
            secondary = t2;

            protoType = new Warhead(this);
        }

        /// <summary>
        /// Create a new WarheadType with splash damage.
        /// </summary>
        /// <param name="dam">
        /// One-time damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="siz">
        /// size <see cref="System.Byte"/>
        /// </param>
        /// <param name="splshd">
        /// splash damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="splshrd">
        /// splash radius <see cref="System.Byte"/>
        /// </param>
        /// <param name="t">
        /// primary type <see cref="Types"/>
        /// </param>
        public WarheadType(byte dam, byte siz, byte splshd, byte splshrd, Types t)
        {
            damage = dam;
            size = siz;
            splashDam = splshd;
            splashRad = splshrd;
            primary = t;

            protoType = new Warhead(this);
        }

        /// <summary>
        /// Create a new WarheadType with splash damage.
        /// </summary>
        /// <param name="dam">
        /// One-time damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="siz">
        /// size <see cref="System.Byte"/>
        /// </param>
        /// <param name="splshd">
        /// splash damage <see cref="System.Byte"/>
        /// </param>
        /// <param name="splshrd">
        /// splash radius <see cref="System.Byte"/>
        /// </param>
        /// <param name="t">
        /// primary type <see cref="Types"/>
        /// </param>
        /// <param name="t2">
        /// wecondary type<see cref="Types"/>
        /// </param>
        public WarheadType(byte dam, byte siz, byte splshd, byte splshrd, Types t, Types t2)
        {
            damage = dam;
            size = siz;
            splashDam = splshd;
            splashRad = splshrd;
            primary = t;
            secondary = t2;

            protoType = new Warhead(this);
        }

        /// <summary>
        /// Create a warhead of this type.
        /// </summary>
        /// <returns>
        /// A <see cref="GameObject"/>
        /// </returns>
        override public GameObject create()
        {
            return new Warhead((Warhead)protoType);
        }
    }
}