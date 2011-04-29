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
 * Arguments for button toggled event. Contains the UIElement that was       *
 * toggled and the state of the toggle button.                               *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.UI.Frames.UIElements;

namespace LessThanOk.UI.Events
{
    class ToggleEventArgs : EventArgs
    {

        public STATE State { get { return _state; } }
        public ToggleButton Element { get { return _element; } }

        private ToggleButton _element;
        private STATE _state;

        public enum STATE
        {
            UP,
            DOWN
        }

        public ToggleEventArgs(ToggleButton element, STATE state)
        {
            _element = element;
            _state = state;
        }
    }
}
