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
 * Type for an Armor.                                                        *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, Armor                  *
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Type for an Armor.
/// </summary>
public class ArmorType : GameObjectType
{
	private int strength;
	
	/// <summary>
	/// Max Strength
	/// </summary>
	public int Strength
	{
		get
		{
			return strength;
		}
		set
		{
			strength = value;
		}
	}
	
	private byte thickness;
	
	/// <summary>
	/// Only bullets larger than thickness can damage
	/// the armor itself. Bullets smaller than thickness
	/// are greatly mitigated increasing with the difference.
	/// </summary>
	public byte Thickness
	{
		get
		{
			return thickness;
		}
		set
		{
			thickness = value;
		}
	}
	
	private List<WarheadType.Types> strongResistances;
	
	/// <summary>
	/// A list of Warhead Types. Damage is reduced 88% if the
	/// damage type is strongly resistant.
	/// </summary>
	public List<WarheadType.Types> StrongResistances
	{
		get
		{
			return strongResistances;
		}
		set
		{
			strongResistances = value;
		}
	}
	
	private List<WarheadType.Types> weakResistances;
	
	/// <summary>
	/// A list of Warhead Types. Damage is reduced 50% if the
	/// damage type is weakly resistant.
	/// </summary>
	public List<WarheadType.Types> WeakResistances
	{
		get
		{
			return weakResistances;
		}
		set
		{
			weakResistances = value;
		}
	}
	
	static ArmorType()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(ArmorType).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	/// <summary>
	/// Create a new armor type with no resistances.
	/// </summary>
	/// <param name="st">
	/// The Max Strength <see cref="System.Int32"/>
	/// </param>
	/// <param name="th">
	/// The thickness <see cref="System.Byte"/>
	/// </param>
	public ArmorType(int st, byte th)
	{
		strength = st;
		thickness = th;
		strongResistances = new List<WarheadType.Types>();
		weakResistances = new List<WarheadType.Types>();
		
		protoType = new Armor(this);
	}
	
	/// <summary>
	/// Create a new armor type with resistances.
	/// </summary>
	/// <param name="st">
	/// The Max Strength <see cref="System.Int32"/>
	/// </param>
	/// <param name="th">
	/// The Thickness <see cref="System.Byte"/>
	/// </param>
	/// <param name="sr">
	/// Strong Resistances <see cref="List<WarheadType.Types>"/>
	/// </param>
	/// <param name="wr">
	/// WeakResistances <see cref="List<WarheadType.Types>"/>
	/// </param>
	public ArmorType(int st, byte th, List<WarheadType.Types> sr, List<WarheadType.Types> wr)
	{
		strength = st;
		thickness = th;
		strongResistances = new List<WarheadType.Types>(sr);
		weakResistances = new List<WarheadType.Types>(wr);
		protoType = new Armor(this);
//		strongResistances.sort();
//		weakResistances.sort();
	}
	
	/// <summary>
	/// Apply an amount of damage through the armor
	/// </summary>
	/// <param name="damage">
	/// Amount of damage<see cref="System.Byte"/>
	/// </param>
	/// <returns>
	/// Amount of damage not absorbed.<see cref="System.Byte"/>
	/// </returns>
	public byte applyDamage(byte damage)
	{
	}
	
	/// <summary>
	/// Create an armor of this type.
	/// </summary>
	/// <returns>
	/// A <see cref="GameObject"/>
	/// </returns>
	override public GameObject create()
	{
		return new Armor((Armor)protoType);
	}
}
