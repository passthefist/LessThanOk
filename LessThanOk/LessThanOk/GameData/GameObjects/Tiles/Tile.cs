 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.GameData;
using LessThanOk.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace LessThanOk.GameData.GameObjects.Tiles
{
    public class Tile : ActiveGameObject
    {

        private bool isWalkable;

        public bool Walkable
        {
            get { return isWalkable; }
            private set { isWalkable = value; }
        }

        private Vector2 position;

        public override Vector2 getPosition()
        {
            return position;
        }

        protected override void setNewPosition(Vector2 pos)
        {
            position = pos;
        }

        static Tile()
        {
            AgnosticObject.initFieldMaps(typeof(Tile));
        }

        private Tile()
        {
            
        }

        public Tile(TileType type)
        {
            initTile(type);
        }

        internal Tile(Tile src)
        {
            this.position = src.position;
            this.ID = src.ID;
            initTile((TileType)src.Type);
        }

        private void initTile(TileType tType)
        {
            this.image = (Sprite_2D)tType.getImage();
            Type = tType;
            //this.image.Position = this.position;
        }

        override public void update(GameTime gameTime)
        {
            //update sprite animations
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(image.Texture, this.position, Color.White);
        }
    }
}