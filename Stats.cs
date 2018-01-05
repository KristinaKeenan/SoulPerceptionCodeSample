using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
	Napsack Napsack_Script;
	PlayerStart playerStart;

	GameObject napsack;
	public GameObject stats;
	public static bool statsOpen = false;
	public static bool statsCanOpen = true;

	public Text HP;
	public Text SP;

	void Start(){
		playerStart = GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ();
	}
	void Update () {
		//open stats menu
		if (!statsOpen && Input.GetKeyUp (KeyCode.X) && statsCanOpen) {
			stats.SetActive (true);
			//napsack.SetActive (false);
			statsOpen = true;
			//Napsack_Script.setNapsack (false);

			HP.text=playerStart.getPHP().ToString();
			SP.text=playerStart.getSHP().ToString();
		}
		//close stats menu
		else if (statsOpen && Input.GetKeyUp (KeyCode.X)) {
			stats.SetActive (false);
			statsOpen = false;
		}
	}

	public void setStats(bool x){
		statsOpen = x;
	}

	public bool getStats(){
		return statsOpen;
	}

	public void setStatsCanOpen(bool x){
		statsCanOpen = x;
	}
}
