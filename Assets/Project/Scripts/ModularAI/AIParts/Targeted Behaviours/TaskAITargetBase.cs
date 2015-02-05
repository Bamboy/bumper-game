﻿using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI.Tasks
{
	//'EntityAITarget' in MC sourcecode
	public abstract class TaskAITargetBase : EntityAIBase 
	{
		protected EntityCreature taskOwner;

		public TaskAITargetBase( EntityCreature owner )
		{
			taskOwner = owner;
		}

	}
}