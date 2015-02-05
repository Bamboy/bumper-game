using UnityEngine;
using System.Collections;
using Excelsion.Units;
namespace Excelsion.ModularAI
{
	public abstract class EntityLiving : EntityLivingBase
	{
		//public Unit unit;
		public EntityTaskManager tasks = new EntityTaskManager();
		public EntitySenses senses;

		public override void Start()
		{
			base.Start();
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
			//
			//targetSelector

			//Goal selector
			tasks.OnUpdateTasks();
			//Navigation

			//Mobtick

			//Controls

			//Move

			//Look

			//Jump

		}


	}
}
