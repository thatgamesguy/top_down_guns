using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TDGP.Demo
{
/// <summary>
/// Shows text on screen for number of enemies killed in demo scene.
/// </summary>
	[RequireComponent (typeof(Text))]
	public class KillCount : MonoBehaviour
	{
		private Text text;
		private int currentKillCount = 0;
		private static readonly string TEXT_PREPEND = "Kills: ";
		// Use this for initialization
		void Start ()
		{
			text = GetComponent<Text> ();
			text.text = TEXT_PREPEND + currentKillCount.ToString ();
		}
	
		public void EnemyKilled ()
		{
			currentKillCount++;
			text.text = TEXT_PREPEND + currentKillCount.ToString ();
	
		}
	}
}
