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
 * The type of a Unit.                                                       *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory                         *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;


/// <summary>
/// The type of unit.
/// </summary>
public class UnitType : GameObjectType
{
	private List<WeaponType> weapons;
	private ArmorType        armor;
	private EngineType		 engine;
	
	private int maxHp;
	
	static UnitType()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(UnitType).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	/// <summary>
	/// Create a new unit type
	/// </summary>
	/// <param name="weps">
	/// The weapons <see cref="List<WeaponType>"/>
	/// </param>
	/// <param name="a">
	/// The armor <see cref="ArmorType"/>
	/// </param>
	/// <param name="e">
	/// the engine <see cref="EngineType"/>
	/// </param>
	public UnitType (List<WeaponType> weps, ArmorType a, EngineType e)
	{
		weapons = weps;
		armor = a;
		engine = e;

//TODO: I really hate this. Find a way to not allocate directly?
//		even though create() allocates?
		
		List<Weapon> w = new List<Weapon>(1);
		
		foreach(WeaponType t in weapons)
		{
			w.Add((Weapon)t.create());
		}
		
		Armor arm = (Armor)armor.create();
		Engine eng = (Engine)engine.create();
		
		protoType = new Unit(this,w,arm,eng);
	}
	
	/// <summary>
	/// create a new unit.
	/// </summary>
	/// <returns>
	/// A <see cref="GameObject"/>
	/// </returns>
	override public GameObject create()
	{
		return new Unit((Unit)protoType);
	}
}
