using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Excelsion.ModularAI;

namespace Excelsion.ModularAI.Tasks
{
	//EntityAINearestAttackableTarget in MC sourcecode
	public class TaskAITargetNearest : TaskAITargetBase 
	{
		private string targetType;//TODO - make this a builtin array of POSSIBLE target types. Or, just make additional target tasks..?
		private int targetChance;
		private Entity target;
		public TaskAITargetNearest( EntityLiving owner, bool needLOS, bool needNearby,
		                          string target, int targetChance ) : base(owner, needLOS, needNearby)
		{
			targetType = target;
			targetChance = Mathf.Clamp(targetChance, 0, 100);

			this.MutexBits = 1;
		}

		public override bool ShouldExecute ()
		{
			if(Random.Range(0,100) <= targetChance)
				return false;
			else 
			{
				float closestDist = Mathf.Infinity;
				Entity closestTarget = null;
				List<Entity> targets = taskOwner.GetSenses().GetEntitiesInRange();//Get all entities in our sensing radius.
				if( targets == null ) return false;
				//List<Entity> discardedEnts = new List<Entity>();
				foreach( Entity ent in targets )
				{
					if( !ent.IsType( targetType ) ) //Remove any elements NOT of this type. (Maybe just do 'contunue'?)
					{
						//discardedEnts.Add( ent );
						continue;
					}
					else
					{
						float dist = EntityLiving.GetDistance(taskOwner as Entity, ent);
						if( dist < closestDist ) //Get the closest entity to us.
						{
							closestDist = dist;
							closestTarget = ent;
						}
					}
				}

				if( closestTarget == null )
					return false;
				else
				{
					target = closestTarget;
					return true;
				}
			}
		}
		public override void StartExecuting()
		{
			taskOwner.SetTarget(this.target);
			base.StartExecuting();
		}
		public override void UpdateTask()
		{
			return;
		}






	}
}