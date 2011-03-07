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
 * This class is a factory for arbitrarily creating GameObjects.             *
 * GameObjectTypes are added to the factory and associated with a name.      *
 * The factory can then create a GameObject of this type from the given name.*
 *                                                                           *
 * Changing any of the members of a GameObjectType will immediately change   *
 * the respective fields in any existing GameObjects of the type, or any     *
 * GameObject that has a member which is a GameObject of the modified type.  *
 *                                                                           *
 * This class is a singleton, and the factory is accessed with               *
 *      GameObjectFactory.the;                                               *
 *                                                                           *
 * All types must be built bottom up.                                        *
 *                                                                           *
 * See GameObject, GameObjectType                                            *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// <summary>
/// This class is a factory for arbitrarily creating GameObjects from
/// a name or type ID.
/// </summary>

public class GameObjectFactory
{
	private UInt32 numTypes;
	
	private Dictionary<UInt32,GameObjectType>	idToTypeMap;
	private Dictionary<string,UInt32>			stringToIdMap;
	
	public GameObjectFactory()
	{
		numTypes = 0;
		idToTypeMap = new Dictionary<UInt32,GameObjectType>();
		stringToIdMap = new Dictionary<string, uint>();
	}
	
	/// <summary>
	/// add a type to the factory
	/// </summary>
	/// <param name="typeName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="type">
	/// A <see cref="GameObjectType"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool addType(string typeName, GameObjectType type)
	{
		if(numTypes == UInt32.MaxValue)
		{
			Console.WriteLine("FACTORY CANNOT HAVE MORE TYPES");
			return false;
		}
		else
		{
			idToTypeMap[numTypes] = type;
			stringToIdMap[typeName] = numTypes;
			type.Name = typeName;
			
			numTypes++;
		}
		return true;
	}
	
	/// <summary>
	/// create an object given a type id
	/// </summary>
	/// <param name="id">
	/// A <see cref="UInt32"/>
	/// </param>
	/// <returns>
	/// A <see cref="GameObject"/>
	/// </returns>
	public GameObject createGameObject(UInt32 id)
	{
		return idToTypeMap[id].create();
	}
	
	/// <summary>
	/// create an object given a type name
	/// </summary>
	/// <param name="typeName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="GameObject"/>
	/// </returns>
	public GameObject createGameObject(string typeName)
	{
		return createGameObject(stringToIdMap[typeName]);
	}
	
	/// <summary>
	/// get a type given the id
	/// </summary>
	/// <param name="id">
	/// A <see cref="UInt32"/>
	/// </param>
	/// <returns>
	/// A <see cref="GameObjectType"/>
	/// </returns>
	public GameObjectType getType(UInt32 id)
	{
		return idToTypeMap[id];
	}
	
	/// <summary>
	/// get a type given the type name
	/// </summary>
	/// <param name="typeName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="GameObjectType"/>
	/// </returns>
	public GameObjectType getType(string typeName)
	{
		return getType(stringToIdMap[typeName]);
	}

	/// <summary>
	///replace an already existing type with a new type.
	///NOTE: This will not "update" any existing GameObjects
	///      that are of this type or have any members of
	///      this type. Any objects created from the name or
	///      id will reflect the change after calling replaceType()
	/// </summary>
	/// <param name="id">
	/// A <see cref="UInt32"/>
	/// </param>
	/// <param name="newType">
	/// A <see cref="GameObjectType"/>
	/// </param>
	public void replaceType(UInt32 id, GameObjectType newType)
	{
		newType.Name = idToTypeMap[id].Name;
		idToTypeMap[id] = newType;
	}
	
	/// <summary>
	///replace an already existing type with a new type.
	///NOTE: This will not "update" any existing GameObjects
	///      that are of this type or have any members of
	///      this type. Any objects created from the name or
	///      id will reflect the change after calling replaceType()
	/// </summary>
	/// <param name="typeName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="newType">
	/// A <see cref="GameObjectType"/>
	/// </param>
	public void replaceType(string typeName, GameObjectType newType)
	{
		replaceType(stringToIdMap[typeName],newType);
	}
}
