using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LessThanOk.BufferedCommunication;
using LessThanOk.UI;
using LessThanOk.Input.Events.Args;
using LessThanOk.Input.Events;
using LessThanOk.UI.Events;
using LessThanOk.UI.Events.Args;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.UI;


namespace LessThanOk.Network.Commands
{
    class CommandRequester
    {
        private TileMap _map;
        private ActiveGameObject _selectedObject;
        private HashSet<Keys> _hotKeys;

        public CommandRequester()
        {
            InputEvents.The.LeftClickEvent += new EventHandler<MouseEventArgs>(this.LeftClickHandler);
            InputEvents.The.MouseMoved += new EventHandler<MouseEventArgs>(this.MouseMovedHandler);
            InputEvents.The.RightClickEvent += new EventHandler<MouseEventArgs>(this.RightClickHandler);
            InputEvents.The.KeyStrokeEvent += new EventHandler<KeyBoardEventArgs>(this.KeyStrokeHandler);
            UIElementEvents.ButtonPress += new EventHandler<ButtonEventArgs>(this.ButtonPressedHandler);
            _hotKeys = new HashSet<Keys>();
        }

        private void LeftClickHandler(object sender, MouseEventArgs args)
        {
            if (BlackBoard.getTileMap(out _map))
            {
                ActiveGameObject element = _map.getObjectAtPoint(new Vector2(args.MouseState.X, args.MouseState.Y));
                if (element == null)
                    return;
                _selectedObject = element;
            }
        }
        private void RightClickHandler(object sender, MouseEventArgs args)
        {
            if (BlackBoard.getTileMap(out _map))
            {
                ActiveGameObject element = _map.getObjectAtPoint(new Vector2(args.MouseState.X, args.MouseState.Y));
                if (element == null)
                    return;
                //TODO: set agressive and issue move command.
            }
        }
        private void MouseMovedHandler(object sender, MouseEventArgs args)
        {

        }
        private void ButtonPressedHandler(object sender, ButtonEventArgs args)
        {
            if (args.Element.Name == "add")
            {
                if (_selectedObject != null)
                {
                    Command command;
                    GameObjectType type = GameObjectFactory.The.getType("TestUnit");
                    command = new Command_Add(_selectedObject.ID, 0, type.ID, new TimeSpan());
                    GlobalRequestQueue.The.push(command);
                }
            }
        }
        private void KeyStrokeHandler(object sender, KeyBoardEventArgs args)
        {
            if (_hotKeys.Contains(args.Key))
                _hotKeys.Remove(args.Key);
            else
                _hotKeys.Add(args.Key);
        }
    }
}
