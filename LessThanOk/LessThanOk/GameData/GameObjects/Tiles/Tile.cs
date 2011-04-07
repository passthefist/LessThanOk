using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace LessThanOk.GameData.GameObjects.Tiles
{
    public class Tile : ActiveGameObject
    {
        private List<Unit> internalUnits;

        public List<Unit> InternalUnits 
        { 
            get { return internalUnits; } 
            private set { internalUnits = value; } 
        }

        private bool hasUnits;

        public bool HasUnits
        {
            get { return hasUnits; }
            private set { hasUnits = value; }
        }

        private TileType type;

        public TileType Type
        {
            get { return type; }
            private set { type = value; }
        }

        static Tile()
        {
            initFieldMaps();
        }

        private static void initFieldMaps()
        {
            PropertyInfo[] properties = typeof(Tile).GetProperties();

            ushort id = 0;
            foreach (PropertyInfo property in properties)
            {
                idToPropMap[id] = property;
                fieldNameToIDMap[property.Name] = id;
                id++;
            }
        }
        private Tile()
        {
            
        }

        public Tile(TileType type)
        {
            initTile(new List<Unit>(), false, type);
        }

        internal Tile(Tile src)
        {
            initTile(src.internalUnits, src.hasUnits, src.type);
        }

        private void initTile(List<Unit> unit, bool has, TileType tType)
        {
            this.hasUnits = has;
            this.internalUnits = new List<Unit> (unit);
            this.type = tType;
            this.image = tType.getImage();

        }

        override public void update(GameTime gameTime)
        {
            //update sprite animations
        }

        public void clear() 
        {
            hasUnits = false;
            internalUnits.Clear(); 
        }

        public void addUnit(Unit u)
        {
            internalUnits.Add(u);
            hasUnits = true;
        }

    }
}