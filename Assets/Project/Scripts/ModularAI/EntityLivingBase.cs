using UnityEngine;
using System.Collections;

namespace Excelsion.ModularAI
{
	public abstract class EntityLivingBase : Entity
	{
		protected bool isAlive;
		public override void Start()
		{
			base.Start();
			isAlive = true;
		}
		public override void Update()
		{
			base.Update();
		}

		public virtual void OnDeath()
		{
			isAlive = false;
		}

		public bool IsAlive()
		{
			return isAlive;
		}




	}
}