using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI.Tasks
{
	public class TaskRunAroundLikeCrazy : EntityAIBase 
	{
		private int timer = -1;

		//Constructor. Put any variables you need in this. Call this when you're adding tasks.
		public TaskRunAroundLikeCrazy( EntityLiving owner ) : base(owner) //This calls the base constructor
		{
			this.MutexBits = 1;
		}

		//Should we start executing? (This task has higher priority and will stop the AI from chasing the player)
		public override bool ShouldExecute()
		{
			return (timer == 0); //Start if 'timer' is zero.
		}
		//Actually start execution.
		public override void StartExecuting()
		{
			timer = Random.Range( 80, 125 );
			parent.gameObject.renderer.material.color = Color.red;
			parent.InputVelocity = new Vector2(Random.value, Random.value);
		}
		//Should we stop executing? (Here we stop executing if our timer is zero)
		public override bool ContinueExecuting()
		{
			if( timer <= 0 )
				return false;
			else 
				return true;
		}
		//This is basically our Update() function.
		public override void UpdateTask()
		{
			if( timer-- % 1 == 0 ) //reduce timer by 1 and then test if it evenly divides by 5.
			{
				parent.InputVelocity = new Vector2(((Random.value - 0.5f) * 2.0f), ((Random.value - 0.5f) * 2.0f)) * 17.5f;
													//Above math makes the range -1 to +1, instead of 0 to +1. (too lazy to do random.range)
			}
		}
		//This is called when our task is stopped. Useful for clearing variables.
		//Note that unless the Entity that we're tied to is deleted, this task WILL remember values.
		public override void ResetTask()
		{
			timer = -1;
			parent.gameObject.renderer.material.color = Color.blue;
		}

		public override void UpdateIdle()
		{
			if( timer == -1 )
			{
				Main.instance.StartChildCoroutine( Cooldown() );
			}
		}
		public IEnumerator Cooldown()
		{
			timer = -2;
			yield return new WaitForSeconds( Random.Range(0.8f, 4.45f) );
			timer = 0;
		}

		//Can this task be stopped externally? IE, If we only can stop when we say so. (In this case, only we can stop it.)
		public override bool IsInterruptible()
		{
			return false;
		}
	}
}