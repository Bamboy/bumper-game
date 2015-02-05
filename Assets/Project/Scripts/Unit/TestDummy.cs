using UnityEngine;
using System.Collections;

namespace Excelsion.Units
{
	public class TestDummy : Unit 
	{

		public override void OnOutOfBounds()
		{
			Respawn();
		}
		public void Respawn()
		{
			transform.position = new Vector3(0,0,0);
		}
	}
}