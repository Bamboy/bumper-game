using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	public class EntitySenses
	{
		/*
			Holds arrays of entities based on vision. 
			One of these per EntityLiving.
			array seenEntitiesarray unseenEntitiesbool canSee( ENTITY )Checks both arrays for ENTITY, if not found, does a raycast from our eyes, to ENTITY's eyes and adds ENTITY to the appropriate array. (Uses a EntityLiving function)
		 */
		private EntityLiving myEntity;
		public EntitySenses( EntityLiving me )
		{
			myEntity = me;
		}
	}
}