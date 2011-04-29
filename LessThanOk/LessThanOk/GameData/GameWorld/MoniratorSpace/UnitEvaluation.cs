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
 * UnitEvaluation contains a list of Dependancies.  This class will          *
 * determain what UnitType the Dependancy List will evalutate to.            *
 *                                                                           *
\*---------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LessThanOk.GameData.GameWorld.MoniratorSpace
{
    public class UnitEvaluation
    {
        public String Name { get { return _name; } }

        private List<Dependency> _dep;
        private String _name;

        public UnitEvaluation(String name)
        {
            _name = name;
        }

        public void addDependency(Dependency dep)
        {
            _dep.Add(dep);
        }
        /// <summary>
        /// Get the UnitType deffined by dependancies.
        /// </summary>
        /// <param name="map">Paramater to value map.</param>
        /// <returns>String identifying a UnitType.</returns>
        public String getType(Dictionary<String, bool> map)
        {
            String retval;
            // Return the first Dependency that returns a string (AKA evalutates to being met).
            foreach (Dependency d in _dep)
            {
                if ((retval = d.Evaluate(map)) != null)
                    return retval;
            }
            return _name;
        }
        
    }
}
