using UnityEngine;
using TMPro;
using UtilityAi;

	public class HUD : MonoBehaviour
	{
		[SerializeField] private GameObject hudCanvas;
		[SerializeField] private TMP_Text currentAction;
		[SerializeField] private TMP_Text hunger;
		[SerializeField] private TMP_Text energy;

		private void Update()
		{
			hudCanvas.transform.LookAt(Camera.main.transform);
		}

		public void UpdateStatsText(Stats stats)
		{
			//currentAction.text = brain.bestAction;
			energy.text = "Energy: " + stats.Energy.ToString();
			hunger.text = "Hunger: " + stats.Hunger.ToString();
		}

		public void UpdateBestActionText(Action bestAction)
		{
			currentAction.text = "Current Action = " + bestAction.name;
		}
	}
