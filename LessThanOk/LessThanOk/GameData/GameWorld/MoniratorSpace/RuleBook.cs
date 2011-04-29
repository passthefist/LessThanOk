/*---------------------------------------------------------------------------*\
*                         LessThanOK Engine                                 *
*                                                                           *
*          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
*                                                                           *
*          authors:  Anthony LoBono (ajlobono@gmail.com)                    *
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
 * RuleBook is responsible for holding the dependancies for building units.  *
 * RuleBook is also responsible for maintaining a refrence count to each     *
 * UnitType.                                                                 *
 *                                                                           *
 * This class will be expanded upon as more complexity is added to the game. *
 * May adventually contain GameObjectFactory in some manner.                 *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameWorld.Events;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.GameData.GameWorld.MoniratorSpace
{
    public class RuleBook
    {
        private Dictionary<String, UnitEvaluation> _UnitDependencies;
        private Dictionary<String, int> _refrenceCount;
        public Dictionary<String, bool> _unitExists;

        public RuleBook()
        {
            _UnitDependencies = new Dictionary<string, UnitEvaluation>();
            _unitExists = new Dictionary<string, bool>();
            _refrenceCount = new Dictionary<string, int>();
        }
        /// <summary>
        /// Initialize RuleBook from XML file.
        /// </summary>
        /// <param name="file">Path To XML file.</param>
        public void LoadXMLData(String file)
        {
            // No time to impliment xml parsing.
            // Hard code rule book.
        }
        /// <summary>
        /// Get the Dependancies for a given unit.
        /// </summary>
        /// <param name="name">String identifying the UnitType</param>
        /// <returns>UnitEvaluation for checking what UnitType to build.</returns>
        public UnitEvaluation getDependancy(String name)
        {
            UnitEvaluation retval;
            _UnitDependencies.TryGetValue(name, out retval);
            return retval;
        }
        /// <summary>
        /// Handler for the UnitBuiltEvent.  Used to mantain _refrenceCount.
        /// </summary>
        /// <param name="sender">Objected that tiggered event.</param>
        /// <param name="args">EventArgs for the event.</param>
        public void UnitBuiltEventHandler(object sender, UnitEventArgs args)
        {
            String name = GameObjectFactory.The.getType(args.UnitBuilt.ID).Name;
            incrementRefCount(name);
        }
        /// <summary>
        /// Handler for the UnitRemovedEvent. Used to maintain _refrenceCount.
        /// </summary>
        /// <param name="sender">Objected that triggered event.</param>
        /// <param name="args">Arguments for the event.</param>
        public void UnitRemovedEventHandler(object sender, UnitEventArgs args)
        {
            String name = GameObjectFactory.The.getType(args.UnitBuilt.ID).Name;
            decrementRefCount(name);
        }
        /// <summary>
        /// Increment the refrence count of a unit.
        /// </summary>
        /// <param name="name">String identifying the UnitType</param>
        /// <returns>True if incrementing succeded.</returns>
        private bool incrementRefCount(String name)
        {
            int val;
            // Check if dictionary contians the UnitType
            if (_refrenceCount.TryGetValue(name, out val))
            {
                // Increment the value and replace it.
                val += 1;
                // If value is 0 then update the UnitExists Dictionary.
                if (val > 0)
                {
                    updateUnitExists(name, true);
                }
                _refrenceCount.Remove(name);
                _refrenceCount.Add(name, val);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Method for decrementing the refrence count of a UnitType
        /// </summary>
        /// <param name="name">String identifying the UnitType</param>
        /// <returns>True if decremting refrence count succeded.</returns>
        private bool decrementRefCount(String name)
        {
            int val;
            if (_refrenceCount.TryGetValue(name, out val))
            {
                val -= 1;
                // If refrence count drops to 0 or below update Unit Exists Dictionary.
                if (val <= 0)
                {
                    val = 0;
                    updateUnitExists(name, false);
                }
                _refrenceCount.Remove(name);
                _refrenceCount.Add(name, val);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Update the dictionary for checking if a UnitType exists in the game.
        /// </summary>
        /// <param name="name">String identifying the UnitType</param>
        /// <param name="b">Value to update to.</param>
        private void updateUnitExists(String name, bool b)
        {
            if (_unitExists.ContainsKey(name))
            {
                _unitExists.Remove(name);
                _unitExists.Add(name, b);
            }
            _unitExists.Add(name, b);
        }

 
    }
}
