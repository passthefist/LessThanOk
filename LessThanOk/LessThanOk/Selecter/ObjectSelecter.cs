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

        public ObjectSelector The { get { return the; } }
        static readonly ObjectSelector the = new ObjectSelector();
        static ObjectSelector()
        {
            InputManager.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(MouseUpHandler);
        }
        
        private static void MouseUpHandler(object sender, MouseEventArgs args)
        {
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
