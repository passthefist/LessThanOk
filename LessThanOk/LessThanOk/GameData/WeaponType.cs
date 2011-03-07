using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;

public class WeaponType : GameObjectType
{
	private WarheadType    warhead;
	
	public WarheadType _Warhead
	{
		get {return warhead;}
		set {warhead = value;}
	}
	
	private ProjectileType projectile;
	
	public  ProjectileType _Projectile
	{
		get {return projectile;}
		set {projectile = value;}
	}
	
	static WeaponType()
	{
		initFieldMaps();
	}
	
	private static void initFieldMaps()
	{
		PropertyInfo[] properties = typeof(WeaponType).GetProperties();
		
		ushort id = 0;
        foreach (PropertyInfo property in properties)
        {
            idToPropMap[id] = property;
            fieldNameToIDMap[property.Name] = id;
            id++;
        }
	}
	
	public WeaponType (WarheadType warhead, ProjectileType projectile)
	{
		Warhead war = (Warhead)warhead.create();
		Projectile proj =(Projectile) projectile.create();
		
		protoType = new Weapon(this,war,proj);
	}
	
	override public GameObject create()
	{
		return new Weapon((Weapon)protoType);
	}
}
