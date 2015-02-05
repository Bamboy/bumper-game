using UnityEngine;
using System.Collections;
using Excelsion.Units;
namespace Excelsion.ModularAI.Tasks
{
	public class TaskChasePlayer : EntityAIBase 
	{
		private Unit myUnit;
		private GameObject gameObject;

		//Constructor
		public TaskChasePlayer( GameObject myGameObject )
		{
			gameObject = myGameObject;
			myUnit = gameObject.GetComponent< Unit >();
			this.MutexBits = 2;
		}

		//Should we start executing?
		public override bool ShouldExecute()
		{
			return true;
		}

		//Actually start execution
		public override void StartExecuting()
		{
			gameObject.renderer.material.color = Color.yellow;
			myUnit.inputVelocity = VectorExtras.Direction( gameObject.transform.position, Main.player.transform.position ) * 20.0f;
		}
		//Should we stop executing?
		public override bool ContinueExecuting()
		{
			return true;
		}

		//This is basically our Update() function.
		public override void UpdateTask()
		{
			myUnit.inputVelocity = VectorExtras.Direction( gameObject.transform.position, Main.player.transform.position ) * 20.0f;
			if( Vector2.Distance( gameObject.transform.position, Main.player.transform.position ) <= 0.125f )
			{
				gameObject.renderer.material.color = Color.green;
				gameObject.rigidbody2D.mass = 5.0f;
			}
			else
			{
				gameObject.renderer.material.color = Color.yellow;
				gameObject.rigidbody2D.mass = 2.0f;
			}
		}

		//This is called when our task is stopped
		public override void ResetTask()
		{
			gameObject.rigidbody2D.mass = 2.0f;
			gameObject.renderer.material.color = Color.blue;
		}

		//Can this task be stopped externally?
		public override bool IsInterruptible()
		{
			return true;
		}
	}
}
