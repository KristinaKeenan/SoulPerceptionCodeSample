using UnityEngine;
using System.Collections;

public class CombatInfo : MonoBehaviour {
	public Location location;
	public PlayerMovement movement;
	public Stats Stats_Script;
	public Napsack Napsack_Script;
	
	static bool[] BossActive; //checks to see if a fight with these bosses is active
	static bool[] BossDefeated; //checks to see if these bosses are defeated

	public static bool battleStart = false;
	public static bool monstersCanSpawn = false;

	private int randNum;
	private int playerNum = 0;

	void Start () {
		//1.maaji, 2.elliot, 3.sleeping girl, 4.dreamkeeper
		BossActive = new bool[]{ false, false, false, false};
		BossDefeated = new bool[]{ false, false, false, false};
	}

	void Update () {
		randNum = (Random.Range(-100,100));
		//Debug.Log (randNum);

		//makes sure player is outside house before monsters can spawn
		if (location.getLevel () == "garden") {
			monstersCanSpawn = true;
		}

		/*activates combat*/
		//activates battle screen if standard monster fight is active
		if (monstersCanSpawn && playerNum == randNum) {
			battleStart = true;
		}
		//activates battle screen if a boss fight is active
		if (BossActive[0] || BossActive[1] || BossActive[2] || BossActive[3]) {
			battleStart = true;
		}

		//displays and undisplays combat screen
		if (battleStart) {
			//Debug.Log ("this displays combat screen");
			movement.setKeyAble (false);
			Stats_Script.setStatsCanOpen (false);
			Napsack_Script.setNapsackCanOpen (false);
		}
		else if (!battleStart) {
			//Debug.Log ("this undisplays combat screen");
			movement.setKeyAble (true);
			Stats_Script.setStatsCanOpen (true);
			//Napsack_Script.setNapsackCanOpen (true);
		}
	}


	/*functions that set a boss as active and defeated*/
	public void setBossActive(int x, bool y){
		BossActive [x] = y;
	}
	public void setBossDefeated(int x, bool y){
		BossDefeated [x] = y;
	}

	//sets battle on/off
	public void setBattleStart(bool x){
		battleStart = x;
	}
}
