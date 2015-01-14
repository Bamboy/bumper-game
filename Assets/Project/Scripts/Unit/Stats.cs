using UnityEngine;
using System.Collections;

namespace Excelsion.Unit
{
	public class Stats : System.Object
	{
		int _pow = 0;
		int _dmg = 0;
		int _res = 1;
		int _vit = 1;

		public Stats() { }
		public Stats( int power, int damage, int resistance, int vitality )
		{
			POW = Mathf.Max( 0, power );
			DMG = Mathf.Max( 0, damage );
			RES = Mathf.Max( 1, resistance );
			VIT = Mathf.Max( 1, vitality );
		}

		public int POW // How forceful attacks are.
		{
			get{ return _pow; }
			set{ _pow = Mathf.Max( 0, value ); }
		}
		public int DMG // How damaging attacks are.
		{
			get{ return _dmg; }
			set{ _dmg = Mathf.Max( 0, value ); }
		}
		public int RES // How quickly the unit is able to recover from being pushed.
		{
			get{ return _res; }
			set{ _res = Mathf.Max( 1, value ); }
		}
		public int VIT // How much health the unit has.
		{
			get{ return _vit; }
			set{ _vit = Mathf.Max( 1, value ); }
		}
	}
}
