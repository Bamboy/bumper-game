using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Excelsion.ModularAI
{
	public class EntitySenses
	{
		/*
			Holds arrays of entities based on vision. 
			One of these per EntityLiving.
			array seenEntitiesarray unseenEntitiesbool canSee( ENTITY )Checks both arrays for ENTITY, if not found, does a raycast from our eyes, to ENTITY's eyes and adds ENTITY to the appropriate array. (Uses a EntityLiving function)
		 */

		private EntityLiving owner; //This is our parent entity. This should never change after init!
		private float range;
		public EntitySenses( EntityLiving parent, float awarenessRange )
		{
			owner = parent;
			range = Mathf.Abs( awarenessRange );
		}

		public float Range{ get{ return range; } set{ range = Mathf.Abs( value ); } }

		//List of entities that we can currently see.
		List< Entity > seenEnts = new List< Entity >();
		//List of entities that we can't currently see because something is blocking LOS.
		List< Entity > unseenEnts = new List< Entity >();

		//Clears our creature's "memory".
		public void ClearMemory()
		{
			seenEnts.Clear();
			unseenEnts.Clear();
		}

		#region Line of sight
		public bool CanSee( Entity testEnt )
		{
			if( testEnt == owner as Entity )
				return false;
			else if( this.seenEnts.Contains( testEnt ) )
				return true;
			else if( this.unseenEnts.Contains( testEnt ) )
				return false;
			else //We haven't checked this entity yet. Evaluate it.
				return CheckLineOfSight( testEnt );
		}
		//Seperated this part because 2D LOS will behave differently than 3D LOS.
		private bool CheckLineOfSight( Entity testEnt )
		{
			//There are several ways to do LOS. We're going to use raycasts between center of masses.
			Vector2 ownerPos = owner.rigidbody2D.worldCenterOfMass;
			RaycastHit2D data = Physics2D.Raycast(ownerPos, VectorExtras.Direction(ownerPos, testEnt.rigidbody2D.worldCenterOfMass));
			//Debug.DrawRay(VectorExtras.V3FromV2(ownerPos, 0), VectorExtras.V3FromV2(VectorExtras.Direction(ownerPos, testEnt.rigidbody2D.worldCenterOfMass), 0), Color.green, 0.25f);
			Debug.DrawLine(VectorExtras.V3FromV2(ownerPos, 0), VectorExtras.V3FromV2(testEnt.rigidbody2D.worldCenterOfMass, 0), new Color(0.1f,1f,0f,0.32f), 0.0f);
			if( data.collider == null ) //Make sure we hit anything at all. (Hey, it's possible it could happen!)
			{
				Debug.LogWarning("Somehow the raycast2D did not collide with the LOS target, or anything else. Do you have a weird collider?");
				return AddToUnseen( testEnt );
			}
			else if( data.distance > range ) //Make sure the hit distance is under our sight range
			{
				//Debug.Log("Filtered "+testEnt.name+": Position is out of sensor range.");
				return AddToUnseen( testEnt );
			}
			else
			{
				Entity hitEnt = data.transform.GetComponent< Entity >();
				if( hitEnt == null ) //Make sure the hit object has an Entity component at all.
				{
					//Debug.Log("Filtered NULL: NULL.");
					return AddToUnseen( testEnt );
				}
				else if( hitEnt != testEnt ) //Make sure that this is the object we're trying to see.
				{
					//Debug.Log("Filtered "+testEnt.name+": Collider is blocking vision.");
					return AddToUnseen( testEnt );
				}
				else //I think that if we get to this point, we're good! Add the ent to our seenEnts.
				{
					this.seenEnts.Add( testEnt );
					//Debug.Log("Accepted "+testEnt.name+".");
					return true;
				}
			}
		}

		private bool AddToUnseen( Entity ent ) //Used to shorten the above code a bit.
		{
			this.unseenEnts.Add( ent );
			return false;
		}
		#endregion


		#region Entities within radius
		public List<Entity> GetEntitiesInRange() { return GetEntitiesInRange( this.range ); }
		public List<Entity> GetEntitiesInRange( float distance )
		{
			//Collider2D[] colliders = new Collider2D[0]; //This search would probably be faster using OverlapCircleNonAlloc()
			Collider2D[] colliders = Physics2D.OverlapCircleAll( new Vector2(owner.transform.position.x, owner.transform.position.y), distance );
			if( colliders.Length <= 0 )
				return null;
			else
			{
				List<Entity> entities = new List<Entity>();
				foreach( Collider2D col in colliders )
				{
					Entity ent = col.GetComponent< Entity >();
					if( ent == null )
						continue;
					else if( ent == owner as Entity ) //Filter out ourself.
						continue;
					else if( CanSee( ent ) == false )
						continue;
					else
						entities.Add( ent );
				}
				//Debug.Log("Found "+ entities.Count +" entities.");
				return entities;
			}

		}


		#endregion




	}
}