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
        private static Queue<AdditionChange> addsList;
        private static Queue<RemovalChange> remsList;
        private static Queue<SetValueChange> setsList;

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

        public static bool pullAdd(out AdditionChange change) {
            if (addsList.Count == 0)
            {
                change = null;
                return false;
            }

            change = addsList.Dequeue();
            return true;
        }

        public static bool pullRem(out RemovalChange change) {
            if (remsList.Count == 0)
            {
                change = null;
                return false;
            }

            change = remsList.Dequeue();
            return true;
        }

        public static bool pullSet(out SetValueChange change) {
            if (setsList.Count == 0)
            {
                change = null;
                return false;
            }

            change = setsList.Dequeue();
            return true;
        }
    }
}