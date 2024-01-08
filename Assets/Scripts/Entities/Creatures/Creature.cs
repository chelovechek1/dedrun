using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Creatures
{
    public class Creature : Entity
    {
		public float maxSpeed = 10f; // units per sec
		public float maxAccelerationSpeedGrounded = 10f; // проценотов от оригинальной (ускорится с 0 до maxSpeed за 1/maxAccelerationSpeed секунд)
		public float maxAccelerationSpeedAir = 3f;

		public float jumpPower = 25f;

		protected float jumpCooldownTimer = 0f;
		protected static float jumpCooldown = 0.25f; // Кулдаун между прыжками
		public bool IsGrounded = false;
		//public bool IsGrounded => GroundedCollider.OverlapCollider(new ContactFilter2D() { useLayerMask=true, layerMask = LayerMask.GetMask("floor")}, new List<Collider2D>()) > 0;

		protected Rigidbody2D rb;

		public CreatureController creatureController { get => (CreatureController)Controller; set => Controller = value; }

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "floor") IsGrounded = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.tag == "floor") IsGrounded = false;
		}

		protected override void Start()
		{

			rb = GetComponent<Rigidbody2D>();
			base.Start();
		}

		protected virtual void ProcessMovementFixedUpdate()
		{
			var horizontalVelocity = rb.velocity.x / maxSpeed;
			var acc = (IsGrounded ? maxAccelerationSpeedGrounded : maxAccelerationSpeedAir) * fixedDeltaTime;
			horizontalVelocity += Mathf.Clamp(creatureController.HorizontalMoove - horizontalVelocity, -acc, acc);
			rb.velocity = new(horizontalVelocity * maxSpeed, rb.velocity.y);
		}

		protected virtual bool IsCanJump => IsGrounded && jumpCooldownTimer > jumpCooldown;

		protected virtual void ProcessJumpUpdate()
		{
			jumpCooldownTimer += deltaTime;
			if (creatureController.IsJumped && IsCanJump)
			{
				rb.velocity = new(rb.velocity.x, jumpPower);
				jumpCooldownTimer = 0f;
			}
			
		}

		protected override void Update()
		{
			ProcessJumpUpdate();
			base.Update();
		}

		protected override void FixedUpdate()
		{
			ProcessMovementFixedUpdate();
			base.FixedUpdate();
		}
	}
}