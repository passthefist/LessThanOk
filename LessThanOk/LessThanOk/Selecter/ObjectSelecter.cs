/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
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
 * ObjectSeleter listenst to InputManager's Events and checks the TileMap    *
 * for what was seleted.                                                     *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.Selecter.Events;
using LessThanOk.Input.Events;
using Microsoft.Xna.Framework;
using LessThanOk.Input;

namespace LessThanOk.Selecter
{
    public sealed class ObjectSelector
    {
        public static event EventHandler<SelectedEventArgs> ObjectSelectedEvent;

        private static TileMap _map;

        public static ObjectSelector The { get { return the; } }
        static readonly ObjectSelector the = new ObjectSelector();
        static ObjectSelector()
        {
            InputManager.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(MouseUpHandler);
        }
        /// <summary>
        /// Initialize TileMap
        /// </summary>
        /// <param name="map">TileMap to select from</param>
        public static void Initialize(TileMap map)
        {
            _map = map;
        }
        /// <summary>
        /// Handler for the LeftClick event.
        /// </summary>
        /// <param name="sender">Objected that triggered the event.</param>
        /// <param name="args">Arguments for the event. </param>
        private static void MouseUpHandler(object sender, MouseEventArgs args)
        {
            // TODO: Re-impliment.
            /*
            if (!BlackBoard.getTileMap(out _map))
                return;
            ActiveGameObject obj = _map.getObjectAtPoint(new Vector2(args.MouseState.X, args.MouseState.Y));
            if (obj == null)
                return;
            List<ActiveGameObject> objs = new List<ActiveGameObject>();
            objs.Add(obj);

            SelectedEvents.The.TriggerGameObjectsSelected(this, new SelectedEventArgs(objs));
            */
        }
    }
}
