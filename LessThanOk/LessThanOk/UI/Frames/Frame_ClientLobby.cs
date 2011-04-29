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
using LessThanOk.UI.Events;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI.Frames
{
    public class Frame_ClientLobby : Frame
    {
        public event EventHandler PlayerReady;
        public event EventHandler PlayerNotReady;

        public Frame_ClientLobby(int width, int height)
            : base(width, height)
        {

        }

        public override void addElement(UIElement element)
        {
            if (element is ToggleButton)
            {
                ToggleButton t = (ToggleButton)element;
                if (t.Name == "ready")
                {
                    t.ToggleEvent += new EventHandler<LessThanOk.UI.Events.ToggleEventArgs>(FireReadyEvent);
                }
            }
            base.addElement(element);
        }

        void FireReadyEvent(object sender, ToggleEventArgs e)
        {
            if(e.State == ToggleEventArgs.STATE.UP)
            {
                if(PlayerNotReady != null)
                {
                    PlayerNotReady.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if(PlayerReady != null)
                {
                    PlayerReady.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
