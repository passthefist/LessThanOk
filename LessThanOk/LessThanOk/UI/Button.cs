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
 * This class is a subclass of Element.  It contains a Sprite_Text that is   *
 * the text for the button.                                                  *
 *                                                                           *
 * See: Sprite.cs Sprite_Text.cs Frame.cs                                    *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessThanOk.UI
{
    class Button: Element
    {
        private Sprite_Text textSprite;
        public Vector2 Size{get{return textSprite.Size();}}
        public UIManager.selectedAction Action { get; set; }
        /// <summary>
        /// Constructor for a Button Element.
        /// </summary>
        /// <param name="sprite">Text Sprite for the Button</param>
        /// <param name="n_visible">True if the Button should be visible</param>
        /// <param name="n_action">Handler for the Button.</param>
        /// <param name="n_name">String identifying the Button.</param>
        public Button(Sprite_Text sprite, Boolean n_visible, 
            UIManager.selectedAction n_action, String n_name)
        {
            textSprite = sprite;
            name = n_name;
            this.Action = n_action;
            this.visible = n_visible;
        }
        /// <summary>
        /// Set the possition of the button.  Should Probably be removed?
        /// </summary>
        /// <param name="pos">New global possition</param>
        public void setPosition(Vector2 pos)
        {
            origin = pos;
        }
        /// <summary>
        /// Function called when the Button is clicked.
        /// </summary>
        public override void select()
        {
            Action(this);
        }
        /// <summary>
        /// Not used.
        /// </summary>
        public override void release()
        {

        }
        /// <summary>
        /// Function called when the mouse is over the button.
        /// </summary>
        public override void hover()
        {
            Console.WriteLine("Button Hover");
        }
        /// <summary>
        /// TODO
        /// </summary>
        public override void unHover()
        {

        }
        /// <summary>
        /// Not used.
        /// </summary>
        public override void decrease()
        {

        }
        /// <summary>
        /// Not used.
        /// </summary>
        public override void increase()
        {

        }
        /// <summary>
        /// Check if the x and y coordinate is over the button.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>True if the coordinate is over the button.</returns>
        public override Boolean isOver(int x, int y)
        {
            if (x >= origin.X && x <= (textSprite.Size().X + origin.X))
                if (y >= origin.Y && y <= (textSprite.Size().Y + origin.Y))
                    return true;
            return false;
        }
        /// <summary>
        /// Draw the Sprite for the button.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used for drawing.</param>
        public override void draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.DrawString(textSprite.Font, textSprite.Text, origin,
                new Color(textSprite.Color, textSprite.Alpha), textSprite.Rotation,
                Vector2.Zero, textSprite.Scale, SpriteEffects.None, 0);
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="gameTime">Current GameTime</param>
        public void update(GameTime gameTime) { }
    }
}
