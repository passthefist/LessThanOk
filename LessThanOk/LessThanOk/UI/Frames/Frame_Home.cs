using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames.UIElements;
using LessThanOk.UI.Events;

namespace LessThanOk.UI.Frames
{
    public class Frame_Home: Frame
    {
        public event EventHandler CreateGame;
        public event EventHandler JoinGame;

        public Frame_Home(int width, int height)
            : base(width, height)
        {

        }

        public override void addElement(UIElement element)
        {
            if (element is Button)
            {
                Button b = (Button)element;
                if (b.Name == "create")
                {
                    b.ButtonClickedEvent += new EventHandler<ButtonEventArgs>(FireCreateEvent);
                }
                else if (element.Name == "join")
                {
                    b.ButtonClickedEvent += new EventHandler<ButtonEventArgs>(FireJoinEvent);
                }
            }
            base.addElement(element);
        }

        void FireJoinEvent(object sender, ButtonEventArgs e)
        {
            if(JoinGame != null)
                JoinGame.Invoke(this, EventArgs.Empty);
        }

        void FireCreateEvent(object sender, LessThanOk.UI.Events.ButtonEventArgs e)
        {
            if(CreateGame != null)
                CreateGame.Invoke(this, EventArgs.Empty);
        }


    }
}
