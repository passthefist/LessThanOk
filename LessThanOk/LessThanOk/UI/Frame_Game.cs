/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Anthony LoBono (ajlobono@gmail.com)                           *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                                License                                    *
 *                                                                           *
 * This library is free software; you can redistribute it and/or modify it   *
 * under the terms of the MIT Liscense.                                      *
 *                                                                           *
 * This library is distributed in the hope that it will be useful, but       *
 * WITHOUT ANY WARRANTY; without even the implied warranty of                *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.                      *
 *                                                                           *
 * You should have received a copy of the MIT Liscense with this library, if *
 * not, visit http://www.opensource.org/licenses/mit-license.php.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
/*---------------------------------------------------------------------------*\
 *                            Class Overview                                 *
 *                                                                           *
 * This is a subclass of the Frame class. It is responsible for drawing the  *
 * main game window.                                                         *
 *                                                                           *   
 * See: Frame.cs                                                             *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LessThanOk.Sprites;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.GameData.GameObjects.Tiles;
using LessThanOk.GameData.GameObjects.Units;

namespace LessThanOk.UI
{
    class Frame_Game: Frame
    {
        private GameWorld gw;
        private Rectangle drawBox = new Rectangle();
        public Frame_Game(Vector2 n_origin, Vector2 n_size,
                        Sprite_2D n_background, GameWorld n_gw)
        {
            drawBox.X = 0;
            drawBox.Y = 0;
            elements = null;
            frames = null;
            background = n_background;
            origin = n_origin;
            size = n_size;
            gw = n_gw;

        }
        public GameWorld getGameWorld()
        {
            return gw;
        }
        public void addFrame(Frame element)
        {
            throw new NotImplementedException();
        }
        public void addElement(Vector2 n_origin, Element element)
        {
            throw new NotImplementedException();
        }
        public override void draw(SpriteBatch spriteBatch)
        {
            TileMap tileMap = gw.getTileMap();
            List<Tile> tiles = tileMap.getTilesInRect(new Rectangle(0, 0, 800, 500));
            foreach (Tile t in tiles)
            {
                drawBox.Width = (int)t.Sprite.Size().X;
                drawBox.Height = (int)t.Sprite.Size().Y;
                if (t.Sprite is Sprite_2D)
                    spriteBatch.Draw(((Sprite_2D)t.Sprite).Texture, t._Position, drawBox,
                        Color.White);
                //else
                    //throw new Exception("Tile has Sprite not of Sprit_2D");
            }
            foreach(Tile t in tiles)
            {

                foreach(Unit u in t.InternalUnits)
                {
                    u._Position += new Vector2(0.002f, 0.002f);
                    drawBox.Width = (int)u.Type.getImage().Size().X;
                    drawBox.Height = (int)u.Type.getImage().Size().Y;
                    if (u.Type.getImage() is Sprite_2D)
                        spriteBatch.Draw(((Sprite_2D)u.Type.getImage()).Texture, u._Position, drawBox,
                            Color.White);
                    else
                        throw new Exception("Unit has Sprite not of Sprit_2D");
                }

            }
        }
        public override void update(GameTime gameTime){}
        
        public override Element findElement(Microsoft.Xna.Framework.Input.MouseState mouseState)
        {
            return null;
        }
    }
}
