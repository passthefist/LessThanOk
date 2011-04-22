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
using LessThanOk.Selecter.Events;
using LessThanOk.Selecter.Events.Args;
using LessThanOk.UI.Events;
using LessThanOk.UI.Events.Args;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.UI;
using LessThanOk.Network.Commands.Decorators;


namespace LessThanOk.Network.Commands
{
    class CommandRequester
    {
        private TileMap _map;
        private List<ActiveGameObject> _selectedObjects;
        private HashSet<Keys> _hotKeys;

        public CommandRequester()
        {
            InputEvents.The.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(this.LeftClickHandler);
            InputEvents.The.MouseMoved += new EventHandler<MouseEventArgs>(this.MouseMovedHandler);
            InputEvents.The.RightMouseUpEvent += new EventHandler<MouseEventArgs>(this.RightClickHandler);
            InputEvents.The.KeyStrokeEvent += new EventHandler<KeyBoardEventArgs>(this.KeyStrokeHandler);
            UIElementEvents.ButtonPress += new EventHandler<ButtonEventArgs>(this.ButtonPressedHandler);
            SelectedEvents.The.GameObjectsSelected += new EventHandler<SelectedEventArgs>(this.GameObjectsSeleted);

            _hotKeys = new HashSet<Keys>();
        }

        private void GameObjectsSeleted(object sencer, SelectedEventArgs args)
        {
            _selectedObjects = args.Objects;
        }

        private void LeftClickHandler(object sender, MouseEventArgs args)
        {
            
        }
        private void RightClickHandler(object sender, MouseEventArgs args)
        {
            if (_selectedObjects == null)
                return;
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
        }
        private void MouseMovedHandler(object sender, MouseEventArgs args)
        {

        }
        private void ButtonPressedHandler(object sender, ButtonEventArgs args)
        {
            if (args.Element.Name == "add")
            {
                if (_selectedObjects != null)
                {
                    Command command;
                    GameObjectType type = GameObjectFactory.The.getType("TestUnit");
                    foreach (ActiveGameObject o in _selectedObjects)
                    {
                        command = new AddDecorator(o.ID, 0, type.ID, new TimeSpan(), new Command());
                        GlobalRequestQueue.The.push(command);
                    }
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
