/*---------------------------------------------------------------------------*\
 *                         LessThanOK Engine                                 *
 *                                                                           *
 *          Copyright (C) 2011-2012 by Robert Goetz, Anthony Lobono          *
 *                                                                           *
 *   authors:  Robert Goetz (rdgoetz@iastate.edu)                            *
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
 * This class is the bread and butter of LessThanOk's data aspect. Units are *
 * GameObjects, Armor is a game object, weapons, etc.                        *
 *                                                                           *
 * Game objects are created from the factory and an ascociated type.         *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, AgnosticObject         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;


/// <summary>
/// A user defined data container.
/// </summary>
public abstract class GameObject : AgnosticObject
{
	
	static GameObject()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(GameObject).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	public GameObject()
	{
	}
}
