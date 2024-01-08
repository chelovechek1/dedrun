using InventorySystem;
using InventorySystem.Items.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Creatures.Player
{
    public class Player : Creature, IHasInventory
    {
		private PlayerInventory _inventory;
		public Inventory inventory
		{
			get => _inventory;
			set => _inventory = (PlayerInventory)this.SetInvertory(_inventory, value);
		}

		protected override void Start()
		{
			base.Start();
			Controller = new PlayerController();
		}
	}

	/// <summary>
	/// Инвентарь игрока
	/// </summary>
	public class PlayerInventory : GridInventory
	{
		public Weapon[] equpidWeapons = new Weapon[2];
		public override void Clear()
		{
			base.Clear();
			for (var i = 0; i < equpidWeapons.Length; i++)
				equpidWeapons[i] = null;
		}
	}
}