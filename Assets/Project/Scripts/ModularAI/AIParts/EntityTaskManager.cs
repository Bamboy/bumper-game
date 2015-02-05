using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Excelsion.ModularAI.Tasks;

namespace Excelsion.ModularAI
{
	//This is the BRAIN of our AI. It decides what tasks get run, which dont, and when.
	public class EntityTaskManager
	{
		#region TaskEntry Class
		private class TaskEntry 
		{ 
			public EntityAIBase action; 
			public int priority;
			public TaskEntry( int priority, EntityAIBase behaviour )
			{
				this.priority = priority;
				this.action = behaviour;
			}
		}
		#endregion

		private List< TaskEntry > tasks = new List< TaskEntry >();
		private List< TaskEntry > executingTasks = new List< TaskEntry >();

		private int tickCount;
		private int tickRate = 3; //How often we 'Tick'. Every 'tick' will make us evaluate possible new tasks.

		//Constructor
		public EntityTaskManager() {}

		#region TaskSetup

		public void AddTask( int priority, EntityAIBase behaviour )
		{ tasks.Add(new TaskEntry( priority, behaviour )); }
		public void RemoveTask( EntityAIBase behaviour )
		{
			//List< TaskEntry > tasksCopy = new List< TaskEntry >( tasks );
			for(int i = 0; i < tasks.Count; i++)
			{
				EntityAIBase action = tasks[i].action;
				if( action == behaviour )
				{
					if( executingTasks.Contains( tasks[i] ) ) //Remove from our executing list too.
					{
						action.ResetTask();
						executingTasks.Remove( tasks[i] );
					}
					tasks.RemoveAt( i );
				}
			}
			//tasks = tasksCopy;
		}

		#endregion

		#region OnUpdateTasks
		//Everything is decided and run in this function!
		public void OnUpdateTasks()
		{
			List< TaskEntry > newTasks = new List< TaskEntry >();
			TaskEntry tempTask;

			if( tickCount++ % tickRate == 0 )
			{
				for(int t = 0; t < tasks.Count; t++) //t for task
				{
					tempTask = tasks[t];
					if(executingTasks.Contains( tempTask )) //Is this task already running?
					{
						if(CanUse( tempTask ) && CanContinue( tempTask ))
						{
							continue; //Exit this loop and go to the next one.
						}
						tempTask.action.ResetTask();
						executingTasks.Remove( tempTask );
					}

					if(CanUse( tempTask ) && tempTask.action.ShouldExecute())
					{
						newTasks.Add( tempTask ); //Add this task to our newTasks. We'll start it later.
						executingTasks.Add( tempTask );
					}
				}
			}
			else
			{
				for(int e = 0; e < executingTasks.Count; e++) //e for executing
				{
					tempTask = executingTasks[e];
					if( tempTask.action.ContinueExecuting() == false )
					{
						tempTask.action.ResetTask();
						executingTasks.RemoveAt( e );
					}
				}
			}

			//Tell new tasks to start executing.
			for(int n = 0; n < newTasks.Count; n++) //n for new
			{
				newTasks[n].action.StartExecuting();
			}

			//Execute main task(s)
			for(int u = 0; u < executingTasks.Count; u++) //u for update
			{
				executingTasks[u].action.UpdateTask();
			}

			//Execute idle tasks
			for(int i = 0; i < tasks.Count; i++) //i for idle
			{
				if( executingTasks.Contains( tasks[i] ) )
					continue;
				tasks[i].action.UpdateIdle();
			}
		}

		#endregion

		#region RunPermissions

		private bool CanContinue( TaskEntry task )
		{
			return task.action.ContinueExecuting();
		}

		private bool CanUse( TaskEntry task )
		{
			//Loop through all of our tasks and test them with the given task.
			for(int i = 0; i < tasks.Count; i++)
			{
				if( task != tasks[i] ) //We're checking OTHER tasks.
				{
					if( task.priority >= tasks[i].priority ) //If our task is greater or equal in priority
					{
						//Are we executing the task in question AND are our tasks not compatible?
						if(executingTasks.Contains( tasks[i] ) && AreTasksCompatible( task, tasks[i] ) == false)
						{
							return false;
						}
					}
					//Its not greater or equal in priority, return false if it's running and we can't stop it.
					else if(executingTasks.Contains( tasks[i] ) && tasks[i].action.IsInterruptible() == false)
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool AreTasksCompatible( TaskEntry task1, TaskEntry task2 )
		{   //The single '&' is a bitwise AND operation. (in this case, we're using ints)
			return (task1.action.MutexBits & task2.action.MutexBits) == 0;
		}

		#endregion

	}
}