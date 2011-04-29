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
 *                                                                           *
\*---------------------------------------------------------------------------*/
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
        public event EventHandler ReplayGame;

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
                else if (element.Name == "replay")
                {
                    b.ButtonClickedEvent += new EventHandler<ButtonEventArgs>(FireReplayEvent);
                }
            }
            base.addElement(element);
        }

        void FireReplayEvent(object sender, ButtonEventArgs e)
        {
            if (ReplayGame != null)
                ReplayGame.Invoke(this, EventArgs.Empty);
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
