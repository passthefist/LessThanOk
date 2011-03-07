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
 * A unit. Units have armor, weapons, and engines that allow them to move.   *
 * Units also have a set of commands to execute, and some basic AI           *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, pretty much everything.*
 *                                                                           *
\*---------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using LessThanOk.Network.Commands;

[assembly: InternalsVisibleTo("UnitType")]

/// <summary>
/// A unit. Units have armor, weapons, and engines that allow them to move.
/// </summary>
public class Unit : ActiveGameObject
{
	private UnitType		type;
	
	/// <summary>
	/// The type of this unit
	/// </summary>
	public  UnitType		Type
	{
		get{return type;}
		private set{type = value;}
	}
		
	private Vector3  		velocity;
	
	/// <summary>
	/// The velocity of this unit
	/// </summary>
	public  Vector3			_Velocity
	{
		get{return velocity;}
		private set{velocity = value;}
	}
		
	private UInt32			hp;
	
	/// <summary>
	/// How much health this unti has.
	/// </summary>
	public  UInt32			_hp
	{
		get{return hp;}
		set{hp = value;}
	}
	
	
	private List<Weapon> 	weapons;

	private Armor    		armor;
	private Engine  		engine;
	
	private bool     		aggressive;
	private bool     		pursue;
	
	private Queue<Command> 	commands;
	
	static Unit()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(Unit).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	protected Unit():base(){init();}
	
	internal Unit(UnitType t, List<Weapon> weps, Armor arm, Engine e):base()
	{
		type    = t;
		weapons = weps;
		armor   = arm;
	}

	/// <summary>
	/// Copy ctor
	/// </summary>
	/// <param name="u">
	/// A <see cref="Unit"/>
	/// </param>
	public Unit(Unit u):base()
	{
		
		init();
		
		this.type = u.type;
		this.weapons = u.weapons;
		this.armor = u.armor;
		this.engine = u.engine;
	}
	
	private void init()
	{
		velocity   = new Vector3();
		commands   = new Queue<Command>();
		aggressive = false;
		pursue     = false;
	}
	
	/// <summary>
	/// Update the unit.
	/// </summary>
	/// <param name="elps">
	/// A <see cref="GameTime"/>
	/// </param>
	override public void  update(GameTime elps)
	{
		
	}
	
//	public WeaponFire fireWeapon()
}
