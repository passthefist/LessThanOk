using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;
using LessThanOk.Input.Events.Args;

namespace LessThanOk.Input.Events
{
    public delegate void UIEventHandler(object sender, MouseEventArgs e);

    public sealed class InputEvents
    {
        public event EventHandler<MouseEventArgs> MouseMoved;
        public event EventHandler<MouseEventArgs> LeftClickEvent;
        public event EventHandler<MouseEventArgs> RightClickEvent;
        public event EventHandler<KeyBoardEventArgs> KeyStrokeEvent;

        public static InputEvents The { get { return the; } }
        private static InputEvents the = new InputEvents();
        static InputEvents(){}

        public void TriggerMouseMoved(object sender, MouseEventArgs args)
        {
            MouseMoved.Invoke(sender, args);
        }
        public void TriggerLeftClick(object sender, MouseEventArgs args)
        {
            LeftClickEvent.Invoke(sender, args);
        }
        public void TriggerRightClick(object sender, MouseEventArgs args)
        {
            RightClickEvent.Invoke(sender, args);
        }
        public void TriggerKeyStroke(object sender, KeyBoardEventArgs args)
        {
            KeyStrokeEvent.Invoke(sender, args);
        }
    }
}
