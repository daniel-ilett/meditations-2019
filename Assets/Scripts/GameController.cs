/*	Keeps track of how many sheep are in the pen and ends the game when all
 *	sheep are captured.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private List<Sheep> sheepList;

	private List<Sheep> capturedSheep = new List<Sheep>();

	// I know it's dirty, but this game is tiny, which makes this fine.
	private static GameController instance;

	private void Awake()
	{
		instance = this;
	}

	// A sheep is pushed into the pen or walks into it.
	public void CaptureSheep(Sheep sheep)
	{
		capturedSheep.Add(sheep);

		if(capturedSheep.Count == sheepList.Count)
		{
			WinGame();
		}
	}

	// A sheep walks out of the pen.
	public void LoseSheep(Sheep sheep)
	{
		capturedSheep.Remove(sheep);
	}

	// Close the window.
	private void WinGame()
	{
		Application.Quit();
	}
}
