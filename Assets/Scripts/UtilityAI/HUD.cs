using System;
using System.Collections;
using System.Collections.Generic;
using AiMovement;
using UnityEngine;
using TMPro;


public class HUD : MonoBehaviour
{
	[SerializeField] private TMP_Text currentAction;
	[SerializeField] private TMP_Text hunger;
	[SerializeField] private TMP_Text energy;

	public void UpdateStatsText(int e, int h)
	{
		//currentAction.text = brain.bestAction;
		energy.text = e.ToString();
		hunger.text = h.ToString();
	}

	public void UpdateBestActionText(string bestAction)
	{
		currentAction.text = "Current Action = " + bestAction;
	}

	public void UpdateScoreText(Action action)
	{
		Debug.Log(action.name);
	}
}
