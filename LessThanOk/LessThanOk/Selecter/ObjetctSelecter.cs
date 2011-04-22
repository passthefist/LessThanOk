using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects;
using LessThanOk.GameData.GameWorld;
using LessThanOk.Selecter.Events;
using LessThanOk.Selecter.Events.Args;
using LessThanOk.Input.Events.Args;
using LessThanOk.Input.Events;
using LessThanOk.BufferedCommunication;
using Microsoft.Xna.Framework;

namespace LessThanOk.Selecter
{
    class ObjectSelector
    {
        private TileMap _map;
        public ObjectSelector()
        {
            InputEvents.The.LeftMouseUpEvent += new EventHandler<MouseEventArgs>(this.MouseUpHandler);
        }
        
        private void MouseUpHandler(object sender, MouseEventArgs args)
        {
            if (!BlackBoard.getTileMap(out _map))
                return;
            ActiveGameObject obj = _map.getObjectAtPoint(new Vector2(args.MouseState.X, args.MouseState.Y));
            if (obj == null)
                return;
            List<ActiveGameObject> objs = new List<ActiveGameObject>();
            objs.Add(obj);

            SelectedEvents.The.TriggerGameObjectsSelected(this, new SelectedEventArgs(objs));
        }
    }
}
