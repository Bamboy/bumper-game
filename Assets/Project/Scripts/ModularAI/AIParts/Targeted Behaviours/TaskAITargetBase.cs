using UnityEngine;
using System.Collections;
using Excelsion.ModularAI;

namespace Excelsion.ModularAI.Tasks
{
	//'EntityAITarget' in MC sourcecode
	public abstract class TaskAITargetBase : EntityAIBase 
	{
		protected EntityCreature taskOwner; //'parent' as EntityCreature

		//Do we need to have line of sight to the target?
		protected bool needLOS;
		//Only target entities that can be easily reached?
		protected bool needNearby;
		//When above is true:
		//	0: No target, OK to search.
		//	1: Nearby target found.
		//	2: Target too far.
		private int targetSearchStatus;
		//Limits how many times we search, to avoid excessive pathfinding operations.
		private int targetSearchDelay;
		//How many frames to continue to persue our target after losing LOS, after this we search for a new target.
		private int persueTimeout;
		//Constructors
		//public TaskAITargetBase( EntityLiving owner, bool needLOS ) : base(owner)
		//{
		//	this( owner, needLOS, false );
		//}
		public TaskAITargetBase( EntityLiving owner, bool needLOS, bool needNearby ) : base(owner)
		{
			this.taskOwner = owner as EntityCreature;
			if( taskOwner == null )
			{
				Debug.LogError("Owner of a TaskAITarget needs to be a creature!");
				Debug.Break();
			}
			this.needLOS = needLOS;
			this.needNearby = needNearby;
		}


		//We verify if our target is still valid here...
		public override bool ContinueExecuting()
		{ 
			EntityLivingBase curTarget = taskOwner.GetTarget() as EntityLivingBase;
			if( curTarget == null )
				return false;
			else if( curTarget.IsAlive() == false )
				return false;
			else
			{
				if( taskOwner.GetDistanceToTarget() > taskOwner.GetSenses().Range ) //Are we out of range?
				{
					return false;
				}
				else
				{
					if( needLOS )
					{
						if( taskOwner.GetSenses().CanSee( curTarget ) )
						{
							persueTimeout = 0;
						}
						else if( persueTimeout++ >= 60 ) //We persue without LOS for 60 frames.
						{
							return false;
						}
					}
					return true;
				}
			}
		}
		public override void StartExecuting()
		{
			this.targetSearchStatus = 0;
			this.targetSearchDelay = 0;
			this.persueTimeout = 0;
		}
		public override void ResetTask()
		{
			taskOwner.SetTarget( null as EntityLivingBase );
		}

		protected bool IsSuitableTarget( EntityLivingBase target ) //Also requires an input boolean?
		{
			if( target == null )				return false;
			else if( target == this.taskOwner )	return false;
			else if( target.IsAlive() == false )return false;
			//else if( //CANNOT ATTACK CLASS// )return false;
			else
			{
				if( this.needLOS && taskOwner.GetSenses().CanSee( target as Entity ) == false ) //Make sure we can see, as long as we should check at all.
					return false;
				else
				//{
					//Do needNearby pathfinding checks here...

					return true; //There's additional code missing here!

				//}
			}

		}


















	}
}