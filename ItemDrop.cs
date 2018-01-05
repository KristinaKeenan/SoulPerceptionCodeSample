using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ItemDrop : MonoBehaviour {
	Image ItemSlot1, ItemSlot2, ItemSlot3;


	GameObject ItemDropImage;
	GameObject napsackImage;
	GameObject WinSound;



	Napsack napsackScript;
	Dictionary<string, int> itemDictionary;
	PlayerMovement player;
	Player playerScript;
	PlayerStart playerStart;
	Location location;
	Stats stats;

	string bossName;

	GameObject battleCanvas;

	GameObject Combat;

	GameObject xButton, zButton;

	bool randObj;


	public TextAsset napsackTXT;

	// Use this for initialization
	void Start () {

		playerScript = GameObject.Find ("Player").GetComponent<Player> ();
		playerStart = GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ();

		xButton = GameObject.Find ("x");

		xButton.SetActive (false);

		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		location = playerScript.GetComponent<Location> ();
		Combat = GameObject.Find ("Combat");

//
//		WinSound = GameObject.Find ("WinSound");
//
//		WinSound.SetActive (true);


		battleCanvas = GameObject.Find ("BattleScreen");


		ItemDropImage = GameObject.Find ("ItemDropImage");


		stats = GameObject.Find ("Stats_Icon").GetComponent<Stats> ();
	

		ItemSlot1 = GameObject.Find ("ItemSlot1").GetComponent<Image> ();

		napsackImage = GameObject.Find ("NapsackImage");
		napsackImage.SetActive (true);

		napsackScript = Combat.GetComponent<Napsack> ();

		itemDictionary = GameObject.Find("NapsackStart").GetComponent<NapsackStart>().Inventory;


		bossName = Combat.GetComponent<BattleSystem> ().bossName;

		randObj = false;


		if (bossName == "Dream Keeper") {
			
		
		} else if (bossName == "Sleeping Girl") {
			if (playerScript.getSleepingGirlDefeated() == true) {
				ItemSlot1.sprite = GameObject.Find ("Room Key").GetComponent<Image> ().sprite;

				napsackScript.testAdd ("Room Key");

				playerScript.sethasKey (true);

				addToTXT ("Room Key");


			
			}

		} else if (bossName == "Elliot") {
			if (playerScript.getMaajiDefeated () && playerScript.getElliotDefeated ()) {
			
				ItemSlot1.sprite = GameObject.Find ("Recorder").GetComponent<Image> ().sprite;

				napsackScript.testAdd ("Recorder");

				playerScript.sethasRecorder (true);

				addToTXT ("Recorder");

			
			} else {
				randObj = true;
			}

		} else if (bossName == "Maaji") {
			if (playerScript.getMaajiDefeated () && playerScript.getElliotDefeated ()) {

				ItemSlot1.sprite = GameObject.Find ("Recorder").GetComponent<Image> ().sprite;

				napsackScript.testAdd ("Recorder");

				playerScript.sethasRecorder (true);

				addToTXT ("Recorder");



			} else {
				randObj = true;
			}


		} else {
			randObj = true;
		} 

		if(randObj == true){


			string[] tempString = new string[12];

			int tempStringLocation = 0;


			foreach(KeyValuePair<string,int> keyValue in itemDictionary)
			{
				string key = keyValue.Key;
				int value = keyValue.Value;
				//value == 0 &&
				if ( key !="Recorder" && key != "Room Key") {
					tempString [tempStringLocation] = key;
					tempStringLocation++;
				}



			}

			int randNum = Random.Range (0, tempStringLocation);

			print (tempString[randNum]);
			ItemSlot1.sprite = GameObject.Find (tempString [randNum]).GetComponent<Image>().sprite;

			napsackScript.testAdd (tempString [randNum]);

			addToTXT (tempString [randNum]);
		
		}
		Destroy (battleCanvas.GetComponent<BattleSystem> ());


	
	}

	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.C)){
//
			player.setinCombat(false);

			xButton.SetActive (true);

//			WinSound.SetActive (false);

			Combat.SetActive (false);

			GameObject.Find ("Player").GetComponent<InitiateDialogue> ().setA (true);

			location.setLevel (playerScript.getSavedLevel());

			stats.setStatsCanOpen (true);

			print ("hp: "+playerStart.getPHPBar() + "sp: " + playerStart.getSHPBar ());

			Combat.GetComponent<BattleSystem> ().WinSound.Stop ();

			Destroy (Combat.GetComponent<ItemDrop> ());

	



		}



	
	}

	void addToTXT(string itemName){
	
		System.IO.File.AppendAllText ("assets/CombatAssets/InventoryScript/napsackTXT.text", "\n" + itemName);
	
	
	}
}
