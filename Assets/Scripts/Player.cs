/*	Player.cs - takes input from keyboard and moves the player accordingly.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	private Vector2 moveVelocity;

	private const float moveSpeed = 2.5f;

	private Animator animator;
	private new Rigidbody2D rigidbody;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		moveVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// Cap the player's move speed.
		if(moveVelocity.magnitude > 1.0f)
		{
			moveVelocity.Normalize();
		}

		// Set the player orientation based on movement direction.
		if(Mathf.Abs(moveVelocity.y) > Mathf.Abs(moveVelocity.x))
		{
			animator.SetInteger("Direction", (moveVelocity.y > 0.0f) ? 2 : 1);
		}
		else if(Mathf.Abs(moveVelocity.x) > Mathf.Abs(moveVelocity.y))
		{
			animator.SetInteger("Direction", (moveVelocity.x < 0.0f) ? 0 : 3);
		}

		// Set the walk animation status accordingly.
		animator.SetBool("IsWalk", moveVelocity.magnitude > 0.5f);
	}

	private void FixedUpdate()
	{
		// Apply velocity in FixedUpdate() to avoid icky physics bugs.
		rigidbody.velocity = moveVelocity * moveSpeed;
	}
}
