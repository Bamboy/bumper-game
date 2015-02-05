using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	public abstract class EntityAIBase
	{
		/*
		 * Basically, this is the base class for any single AI behaviour. 
		 * We do a bitwise AND compare if this task can run alongside another behaviour (mutexBits)
		 * bool shouldExecute()
		 * bool continueExecuting()
		 * bool isInteruptable()
		 * 
		 * OVERRIDE FUNCTIONS
		 * void startExecuting()
		 * void resetTask()
		 * void updateTask()
		 * 
		 * int mutexBits( set; get; )
		 * Above is used for us knowing if this task can be run with another task - Do bitwise AND, if zero, we can run at the same time, otherwise seperately
		 */

		protected EntityLiving parent;
		//Constructor
		public EntityAIBase( EntityLiving owner ){ this.parent = owner; }

		/*Sets a bitmask telling which other tasks may not run concurrently. The test is a simple bitwise AND - if it
     	 *yields zero (As an int), the two tasks may run concurrently, if not - they must run exclusively from each other. */
		#region Examples
		/* -These would run together.
		 * (0 = 0000) & <ANYTHING> = (0 = 0000)
		 * (5 = 0101) & (2 = 0010) = (0 = 0000)
		 * (12 = 1100) & (3 = 0011) = (0 = 0000)
		 * 
		 * -These would not run together.
		 * (4 = 0100) & (7 = 0111) = (4 = 0100)
		 * (9 = 1001) & (1 = 0001) = (1 = 0001)
		 * (11 = 1011) & (14 = 1110) = (10 = 1010)
		 */
		#endregion
		private byte mutexBits; //Byte is an int range 0 - 255. (2^8)
		public byte MutexBits
		{
			get{ return mutexBits; }
			set{ mutexBits = value; }
		}

		//Should This behaviour begin executing?
		public abstract bool ShouldExecute();
		//Should this behaviour continue executing? (Are we done?)
		public virtual bool ContinueExecuting() { return ShouldExecute(); }

		//Determine if this AI Task is interruptible by a higher (= lower value) priority task.
		public virtual bool IsInterruptible() { return true; }

		//Do a one-time task or start a continuous task.
		public abstract void StartExecuting();
		//Update our task.
		public abstract void UpdateTask();
		//Clear (end) the task.
		public abstract void ResetTask();

		//This is called when the task is idle (not executing) Useful for cooldowns
		public virtual void UpdateIdle()
		{
			return;
		}
	}
}