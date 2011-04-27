using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;

using LessThanOk.BufferedCommunication;
namespace LessThanOk.UI
{
    class Frame_Game : Frame
    {
        private TileMap _map;
        private Rectangle _view;

        public Frame_Game(int width, int height)
        {
            _width = width;
            _height = height;
            _view = new Rectangle(0, 0, 800, 400);
            _elements = new Dictionary<LessThanOk.UI.WindowDefinitions.BUTTON, Button>();
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            Boolean success = BlackBoard.getTileMap(out _map);
            foreach (Button e in _elements.Values)
                e.draw(spriteBatch);
            foreach (Tile t in _map.getTilesInRect(_view))
                t.draw(spriteBatch);
            foreach (Tile t in _map.getTilesInRect(_view))
                foreach (Unit u in t.InternalUnits)
                    u.draw(spriteBatch);
        }
        public override void update(GameTime gameTime)
        {
            bool success;
            //success = BlackBoard.getTileMap(out _map);
        }
        public ActiveGameObject getObjectAt(Vector2 pos)
        {
            Boolean success = BlackBoard.getTileMap(out _map);
            return _map.getObjectAtPoint(pos);
        }

        internal ActiveGameObject getObjectAt(int p, int p_2)
        {
            throw new NotImplementedException();
        }
    }
}
