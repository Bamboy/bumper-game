using UnityEngine;
using System.Collections;
using Excelsion.ModularAI;

public class Damage
{
	Entity source;
	float time;
	DamageType type;

	int amount;
	Vector2 force;

	public Damage( Entity theSource, int theAmount )
	{ 
		this.source = theSource;
		this.amount = theAmount;
		this.type = DamageType.Health;
		this.time = Time.time;
	}
	public Damage( Entity theSource, int theAmount, DamageType theType )
	{
		this.source = theSource;
		this.amount = theAmount;
		this.type = theType;
		this.time = Time.time;
	}

	public Damage( Entity theSource, Vector2 theForce )
	{
		this.source = theSource;
		this.force = theForce;
		this.type = DamageType.Force;
		this.time = Time.time;
	}
	public Damage( Entity theSource, Vector2 theForce, DamageType theType )
	{
		this.source = theSource;
		this.force = theForce;
		this.type = theType;
		this.time = Time.time;
	}
}

public enum DamageType
{
	Health,
	Force
}