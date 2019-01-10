/*	Player.cs - takes input from keyboard and moves the player accordingly.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
	private Vector2 moveVelocity;

	private const float moveSpeed = 2.5f;

	private new Rigidbody2D rigidbody;

	private void Awake()
	{
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
	}

	private void FixedUpdate()
	{
		// Apply velocity in FixedUpdate() to avoid icky physics bugs.
		rigidbody.velocity = moveVelocity * moveSpeed;
	}
}
