using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Creatures.Player
{
    public class Player : Creature
    {
		protected override void Start()
		{
			base.Start();
			Controller = new PlayerController();
		}
	}

}