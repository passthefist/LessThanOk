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
 * CommandRequester listens to IntputManagers events, Selector's events as   *
 * well as to Interface events such as button clicks, and constructs Commands*
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LessThanOk.UI;
using LessThanOk.Input.Events;
using LessThanOk.Selecter.Events;
using LessThanOk.Selecter;
using LessThanOk.UI.Events;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.Network.Commands.Decorators;
using LessThanOk.Input;
using LessThanOk.Network.Commands.Events;


namespace LessThanOk.Network.Commands
{
    class CommandRequester
    {
        public EventHandler AddButtonHandler { get { return this.AddHandler; } }
        public event EventHandler<NewCommandEventArgs> NewCommandEvent;

        private TileMap _map;
        private List<ActiveGameObject> _selectedObjects;
        private HashSet<Keys> _hotKeys;
        /// <summary>
        /// Constructor for CommandRequester.  Hooks InputManager's Events. Hooks Selector's Events.
        /// </summary>
        public CommandRequester()
        {
            InputManager.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(LeftClickHandler);
            InputManager.MouseMovedEvent += new EventHandler<MouseEventArgs>(MouseMovedHandler);
            InputManager.RightMouseUpEvent += new EventHandler<MouseEventArgs>(RightClickHandler);
            InputManager.KeyStrokeEvent += new EventHandler<KeyBoardEventArgs>(KeyStrokeHandler);
            ObjectSelector.ObjectSelectedEvent += new EventHandler<SelectedEventArgs>(GameObjectsSeleted);


            _hotKeys = new HashSet<Keys>();
        }
        /// <summary>
        /// Handler for GameObjectSelected event.
        /// </summary>
        /// <param name="sencer">Object that triggered the event.</param>
        /// <param name="args">Event arguments for the GameObjectSeleted event.</param>
        private void GameObjectsSeleted(object sencer, SelectedEventArgs args)
        {
            _selectedObjects = args.Objects;
        }
        /// <summary>
        /// Handler for left click.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="args">Arguments for the LeftClickEvent.</param>
        private void LeftClickHandler(object sender, MouseEventArgs args)
        {
            
        }
        /// <summary>
        /// Handler for the RightClickEvent.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="args">Arguments for the RightClickEvent.</param>
        private void RightClickHandler(object sender, MouseEventArgs args)
        {
            if (_selectedObjects == null)
                return;
            // TODO: replace move logic
            /*
            if (BlackBoard.getTileMap(out _map))
            {
                int x = args.MouseState.X;
                int y = args.MouseState.Y;
                ActiveGameObject element = _map.getObjectAtPoint(new Vector2(x, y));

                if (element == null)
                    return;

                Command command;
                foreach (ActiveGameObject o in _selectedObjects)
                {
                    command = new MoveDecorator(o.ID, element.ID, (ushort)x, (ushort)y, new TimeSpan(), new Command());
                    GlobalRequestQueue.The.push(command);
                }
            }
            */
        }
        private void MouseMovedHandler(object sender, MouseEventArgs args)
        {

        }
        /// <summary>
        /// Handler for the add unit button.
        /// </summary>
        /// <param name="sender">Object that triggered the event.</param>
        /// <param name="args">Arguments for the AddButtonClicked Event.</param>
        private void AddHandler(object sender, EventArgs args)
        {
            Command command;
            GameObjectType type = GameObjectFactory.The.getType("TestUnit");
            foreach (ActiveGameObject o in _selectedObjects)
            {
                command = new AddDecorator(o.ID, 0, type.ID, new TimeSpan(), new Command());
                //if(NewCommandEvent != null)
                    //NewCommandEvent.Invoke(this, new NewCommandEventArgs(command));
            }
        }

        public void KeyStrokeHandler(object sender, KeyBoardEventArgs args)
        {
            if (_hotKeys.Contains(args.Key))
                _hotKeys.Remove(args.Key);
            else
                _hotKeys.Add(args.Key);
        }
    }
}
