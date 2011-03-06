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
 * The type of engine.                                                       *
 *                                                                           *
 * See GameObject, GameObjectType, GameObjectFactory, Engine, Unit           *
 *                                                                           *
\*---------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;


/// <summary>
/// The type of engine.
/// </summary>
public class EngineType : GameObjectType
{
	
	private float maxSpeed;
	public  float MaxSpeed
	{
		get{return maxSpeed;}
		set{maxSpeed = value;}
	}
	
	private float accel;
	public  float Accel
	{
		get{return accel;}
		set{accel = value;}
	}
	
	private float decel;
	public  float Decel
	{
		get{return decel;}
		set{decel = value;}
	}
	
	static EngineType()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(EngineType).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	/// <summary>
	/// Create an engine type.
	/// </summary>
	/// <param name="maxspeed">
	/// Maximum Speed <see cref="System.Single"/>
	/// </param>
	/// <param name="acceleration">
	/// acceleration rate <see cref="System.Single"/>
	/// </param>
	/// <param name="deceleration">
	/// breaking rate <see cref="System.Single"/>
	/// </param>
	public EngineType (float maxspeed, float acceleration, float deceleration)
	{
		maxSpeed = maxspeed;
		accel    = acceleration;
		decel    = deceleration;
		
		protoType = new Engine(this);
	}
	
	/// <summary>
	/// Create an engine of this type.
	/// </summary>
	/// <returns>
	/// A <see cref="GameObject"/>
	/// </returns>
	override public GameObject create()
	{
		return new Engine((Engine)protoType);
	}
}
