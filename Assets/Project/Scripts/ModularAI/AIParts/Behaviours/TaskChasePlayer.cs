using UnityEngine;
using System.Collections;
using Excelsion.Units;

namespace Excelsion.ModularAI.Tasks
{
	public class TaskChasePlayer : EntityAIBase 
	{

		//Constructor
		public TaskChasePlayer( EntityLiving owner ) : base(owner)
		{
			this.MutexBits = 1;
		}

		//Should we start executing?
		public override bool ShouldExecute()
		{
			return true;
		}

		//Actually start execution
		public override void StartExecuting()
		{
			parent.gameObject.renderer.material.color = Color.yellow;
			parent.InputVelocity = VectorExtras.Direction( parent.transform.position, Main.player.transform.position );
		}
		//Should we stop executing?
		public override bool ContinueExecuting()
		{
			return true;
		}

		//This is basically our Update() function.
		public override void UpdateTask()
		{
			parent.InputVelocity = VectorExtras.Direction( parent.transform.position, Main.player.transform.position ) * 5.0f;
			if( Vector2.Distance( parent.gameObject.transform.position, Main.player.transform.position ) <= 0.425f )
			{
				parent.gameObject.renderer.material.color = Color.green;
				parent.rigidbody2D.mass = 4.0f;
			}
			else
			{
				parent.gameObject.renderer.material.color = Color.yellow;
				parent.rigidbody2D.mass = 2.0f;
			}
		}

		//This is called when our task is stopped
		public override void ResetTask()
		{
			parent.rigidbody2D.mass = 2.0f;
			parent.gameObject.renderer.material.color = Color.blue;
		}

		//Can this task be stopped externally?
		public override bool IsInterruptible()
		{
			return true;
		}
	}
}
