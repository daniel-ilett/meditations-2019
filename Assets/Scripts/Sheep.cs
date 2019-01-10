/*	Sheep.cs - pick random directions to move in occasionally.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Sheep : MonoBehaviour
{
	[SerializeField]
	private Sprite upSprite;

	[SerializeField]
	private Sprite downSprite;

	[SerializeField]
	private SpriteRenderer graphic;

	private Coroutine moveRoutine;

	private const float moveSpeed = 2.5f;

	private Animator animator;
	private new Rigidbody2D rigidbody;

	private void Start()
	{
		// Cache components.
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		
		moveRoutine = StartCoroutine(Move());
	}

	// Continuous loop that controls sheep movement.
	private IEnumerator Move()
	{
		// Wait for a random amount of time before moving.
		yield return new WaitForSeconds(Random.Range(2.0f, 7.0f));

		Vector2 walkVelocity = Random.insideUnitCircle * moveSpeed;

		// Set the appropriate sprite for the sheep.
		animator.SetBool("IsUp", walkVelocity.y > 0.0f);
		animator.SetBool("IsWalk", true);

		float walkTime = Random.Range(0.5f, 2.0f);

		// Move in the chosen direction for a random amount of time.
		while(walkTime > 0.0f)
		{
			rigidbody.velocity = walkVelocity;
			walkTime -= Time.deltaTime;
			yield return null;
		}

		// Stop walking.
		animator.SetBool("IsWalk", false);
		rigidbody.velocity = Vector3.zero;

		// FInally, reset the move timer.
		moveRoutine = StartCoroutine(Move());
	}

	// The sheep enters the pen.
	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.tag == "Pen")
		{
			GameController.instance.CaptureSheep(this);
		}
	}

	// The sheep exits the pen.
	private void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (otherCollider.tag == "Pen")
		{
			GameController.instance.LoseSheep(this);
		}
	}
}
