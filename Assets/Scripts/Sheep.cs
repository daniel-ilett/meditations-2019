/*	Sheep.cs - pick random directions to move in occasionally.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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

		// Set the appropriate sprite for the sheep.
		graphic.sprite = (walkVelocity.y > 0.0f) ? upSprite : downSprite;

		float walkTime = Random.Range(0.5f, 2.0f);

		// Move in the chosen direction for a random amount of time.
		while(walkTime > 0.0f)
		{
			rigidbody.velocity = walkVelocity;
			walkTime -= Time.deltaTime;
			yield return null;
		}

		// FInally, reset the move timer.
		rigidbody.velocity = Vector3.zero;
		moveRoutine = StartCoroutine(Move());
	}

	public void EndLevel()
	{
		StopCoroutine(moveRoutine);
	}
}
