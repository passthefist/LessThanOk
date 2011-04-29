/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                  *
*                                                                            *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono           *
*                                                                            *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                     *
*                                                                            *
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
 * AddButton is a Button used for adding units to the game. It contains a    *
 * String representing the UnitType to be added.                             *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.Sprites;

namespace LessThanOk.UI.Frames.UIElements
{
    class AddButton : Button
    {
        public String Type { get { return _type; } }

        private String _type;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the button. For AddButton it should always be "add"</param>
        /// <param name="image">Sprite for displaying the button.</param>
        /// <param name="x">Possition in the x dimention.</param>
        /// <param name="y">Possition in the y dimention.</param>
        /// <param name="type">String identifying the UnitType.</param>
        public AddButton(String name, Sprite image, int x, int y, String type)
            : base("add", image, x, y)
        {
            _type = type;
        }
    }
}
