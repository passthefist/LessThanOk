/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*   authors:  Anthony LoBono (ajlobono@gmail.com),                          *
*             Robert Goetz   (rdgoetz@iastate.edu)                          *
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
 *                                                                           *
\*---------------------------------------------------------------------------*/         
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld;

namespace LessThanOk.BufferedCommunication
{
    public sealed class ChangeList
    {
        private static Queue<AdditionChange> addsList = new Queue<AdditionChange>();
        private static Queue<RemovalChange> remsList = new Queue<RemovalChange>();
        private static Queue<SetValueChange> setsList = new Queue<SetValueChange>();

        public static ChangeList The { get { return The; } }
        static readonly ChangeList the = new ChangeList();
        static ChangeList() { }

        public static bool pushAdd(ref AdditionChange change)
        {
            addsList.Enqueue(change);
            change = null;
            return true;
        }

        public static bool pushRem(ref RemovalChange change)
        {
            remsList.Enqueue(change);
            change = null;
            return true;
        }

        public static bool pushSet(ref SetValueChange change)
        {
            setsList.Enqueue(change);
            change = null;
            return true;
        }

        public static bool pullAdd(out List<AdditionChange> change) 
        {
            if (addsList.Count == 0)
            {
                change = null;
                return false;
            }
            change = new List<AdditionChange>(addsList);
            addsList.Clear();
            return true;
            
        }

        public static bool pullRem(out List<RemovalChange> change) 
        {
            if (remsList.Count == 0)
            {
                change = null;
                return false;
            }
            change = new List<RemovalChange>(remsList);
            remsList.Clear();
            return true;
        }

        public static bool pullSet(out List<SetValueChange> change) 
        {
            if (setsList.Count == 0)
            {
                change = null;
                return false;
            }

            change = new List<SetValueChange>(setsList);
            setsList.Clear();
            return true;
        }
    }
}