using UnityEngine;
using System.Collections;
using Excelsion.Units;
namespace Excelsion.ModularAI
{
	public abstract class EntityLiving : EntityLivingBase
	{
		//public Unit unit;
		public EntityTaskManager tasks = new EntityTaskManager();
		public EntityTaskManager targeting = new EntityTaskManager();
		public EntitySenses senses;

		public override void Start()
		{
			base.Start();
			AddType( "NPC" );
		}
		public override void Update()
		{
			base.Update();
			UpdateAI ();
		}

		/*
		public void SetInputVelocity( Vector2 vec )
		{
			unit.inputVelocity = vec;
		} */

		#region Accessors
		//public EntityTaskManager taskManager
		//{ get{ return taskManager; } }
		//public EntitySenses sen
		#endregion

		public void UpdateAI()
		{
			//checkdespawn

			//sense
			senses.ClearMemory();
			//targetSelector
			targeting.OnUpdateTasks();
			//Goal selector
			tasks.OnUpdateTasks();
			//Navigation

			//AI tick

			//Controls

			//Move

			//Look

			//Jump

		}

		public EntitySenses GetSenses()
		{
			return senses;
		}

		protected Entity entityToAttack;
		public void SetTarget( Entity newTarget )
		{
			this.entityToAttack = newTarget;
		}
		public Entity GetTarget()
		{
			return this.entityToAttack;
		}
		public float GetDistanceToTarget()
		{
			return Entity.GetDistance(this as Entity, entityToAttack);
		}

	}
}
