using GlobalSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Creatures.Player
{
	public class PlayerController : CreatureController
	{
		public override float HorizontalMoove => UserControllerGS.HorizontalAxis;
		public override float VerticalMoove => UserControllerGS.VerticalAxis;

		public override bool IsJumped => UserControllerGS.IsJumpButtonPressed;
		public bool IsJumping => UserControllerGS.IsJumpButtonHolded;
	}
}

