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

        private ProjectileType projectile;

        public ProjectileType _Projectile
        {
            get { return projectile; }
            set { projectile = value; }
        }

        static WeaponType()
        {
            AgnosticObject.initFieldMaps(typeof(WeaponType));
        }

        public WeaponType(WarheadType warhead, ProjectileType projectile)
        {
            init(warhead, projectile);
        }

        public WeaponType(string warhead, string projectile)
        {
            WarheadType wt = (WarheadType)GameObjectFactory.The.getType(warhead);
            ProjectileType pt = (ProjectileType)GameObjectFactory.The.getType(projectile);

            init(wt, pt);
        }

        private void init(WarheadType war, ProjectileType proj)
        {
            warhead = war;
            projectile = proj;

            protoType = new Weapon(this);
        }

        override public GameObject create()
        {
            return new Weapon((Weapon)protoType);
        }
    }
}