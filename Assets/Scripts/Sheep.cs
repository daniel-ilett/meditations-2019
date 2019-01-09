using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Sheep : MonoBehaviour
{
	private Coroutine moveRoutine;

	private const float moveSpeed = 2.5f;

	private new Rigidbody2D rigidbody;

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		moveRoutine = StartCoroutine(Move());
	}

	private IEnumerator Move()
	{
		// Wait for a random amount of time before moving.
		yield return new WaitForSeconds(Random.Range(2.0f, 7.0f));

		Vector2 walkVelocity = Random.insideUnitCircle * moveSpeed;

		float walkTime = Random.Range(0.5f, 2.0f);

		while(walkTime > 0.0f)
		{
			rigidbody.velocity = walkVelocity;
			walkTime -= Time.deltaTime;
			yield return null;
		}

		// Pick a random direction and a random time. Move in that direction.

		rigidbody.velocity = Vector3.zero;
		// FInally, reset the move timer.
		moveRoutine = StartCoroutine(Move());
	}

	public void EndLevel()
	{
		StopCoroutine(moveRoutine);
	}
}
