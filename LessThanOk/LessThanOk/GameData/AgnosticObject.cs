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
 * This class provides an agnostic way of setting and getting the properties *
 * of an object. It also provides maps into the properties for quick         *
 * operations.                                                               *
 *                                                                           *
 * Subclasses must have a static initializer that calls their own version    *
 * of initFieldMaps()                                                        *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;

///<summary>
///Provides an agnostic interface for modifying an object. Fields can be
///accessed by their name using the provided functions.
///</summary>
public class AgnosticObject
{
	protected static Dictionary<ushort,PropertyInfo> idToPropMap;
	protected static Dictionary<string,ushort> fieldNameToIDMap;
	
	static AgnosticObject()
	{
		idToPropMap = new Dictionary<ushort, PropertyInfo>();
		fieldNameToIDMap = new Dictionary<string, ushort>();
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(AgnosticObject).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	/// <summary>
	/// Set a field given its name and a new value.
	/// </summary>
	/// <param name="fieldName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="newValue">
	/// A <see cref="System.Object"/>
	/// </param>
	public void setField(string fieldName, object newValue)
	{
		this.GetType().InvokeMember(
			fieldName, BindingFlags.SetProperty, null, this, new object[] { newValue });
	}
	
	/// <summary>
	/// Set a field given its ID and a new value.
	/// </summary>
	/// <param name="fieldID">
	/// A <see cref="System.UInt16"/>
	/// </param>
	/// <param name="newValue">
	/// A <see cref="System.Object"/>
	/// </param>
	public void setField(ushort fieldID, object newValue)
	{
		idToPropMap[fieldID].SetValue(this,newValue,null);
	}
	
	/// <summary>
	/// Get a field's ID for its name
	/// </summary>
	/// <param name="fieldName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="UInt16"/>
	/// </returns>
	public UInt16 getFieldID(string fieldName)
	{
		return fieldNameToIDMap[fieldName];
	}
}
