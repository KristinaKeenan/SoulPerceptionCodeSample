using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class Napsack : MonoBehaviour {
	public Stats Stats_Script;
	public Player player;
	public BattleSystem battlesystem;

	public static GameObject napsack;
	//    public GameObject stats;
	public static bool napsackOpen = false;
	public static bool napsackCanOpen = true;


	public Dictionary<string,int> Inventory;

	public GameObject[] Slots;

	public int slotCount;


	Image helpMe;




	Item[] itemList;


	public TextAsset napsackTXT;

	bool pauseBool = true;


	void Start(){


		Inventory = GameObject.Find ("NapsackStart").GetComponent<NapsackStart> ().Inventory;

		napsack = GameObject.Find ("NapsackImage");

		battlesystem = GameObject.Find ("Combat").GetComponent<BattleSystem> ();

		slotCount = 0;


		Slots = new GameObject[]{ GameObject.Find("Slot1"),GameObject.Find("Slot2"),GameObject.Find("Slot3"),GameObject.Find("Slot4"),GameObject.Find("Slot5"),GameObject.Find("Slot6"),GameObject.Find("Slot7"),GameObject.Find("Slot8"),GameObject.Find("Slot9"),GameObject.Find("Slot10"),GameObject.Find("Slot11"),GameObject.Find("Slot12")};


		itemList = new Item[12];



		fillSlots ();


	}


	public void testAdd(string itemName){

		if (itemName == "Half Finished Milk Shake") {

			testAdd (0, 5, "A half finished chocolate milk shake. It’s still somehow creamy and cold.", GameObject.Find ("Half Finished Milk Shake").GetComponent<Image> ().sprite, "Half Finished Milk Shake");
		} else if (itemName == "Rouge Scale") {
			testAdd (0, 0, "A shimmering red scale. It sells for a high price, but its origins are unknown.", GameObject.Find ("Rouge Scale").GetComponent<Image> ().sprite, "Rouge Scale");
		} else if (itemName == "Soul Artifact") {
			testAdd (20, 0, "A screaming soul rises from a crack in this sacred gem.", GameObject.Find ("Soul Artifact").GetComponent<Image> ().sprite, "Soul Artifact");
		} else if (itemName == "Wooden Amulet") {
			testAdd (0, 0, "A finished block of oak with a red ribbon. There’s some scribbling on it in another language.", GameObject.Find ("Wooden Amulet").GetComponent<Image> ().sprite, "Wooden Amulet");
		} else if (itemName == "Ragged Sock") {
			testAdd (0, -10, "An old, worn out sock. It smells like feet. Definitely do not eat it.", GameObject.Find ("Ragged Sock").GetComponent<Image> ().sprite, "Ragged Sock");
		} else if (itemName == "Recorder") {
			testAdd (0, 0, "A finely tuned recorder. It produces an ominous song.", GameObject.Find ("Recorder").GetComponent<Image> ().sprite, "Recorder");
		} else if (itemName == "Room Key") {
			testAdd (0, 0, "An ordinary key. Maybe it unlocks something somewhere?", GameObject.Find ("Room Key").GetComponent<Image> ().sprite, "Room Key");
		} else if (itemName == "Octopus Leg") {
			testAdd (0, 5, "A plated pile of wriggly legs. It’s nutritious, probably.", GameObject.Find ("Octopus Leg").GetComponent<Image> ().sprite, "Octopus Leg");
		} else if (itemName == "Mystery Potion") {
			testAdd (0, 5, "This elixir smells like cow dung. Maybe it tastes better?", GameObject.Find ("Mystery Potion").GetComponent<Image> ().sprite, "Mystery Potion");
		} else if (itemName == "HP Potion") {
			testAdd (0, 10, "A sweet smelling potion. It reminds you of dad’s cooking.", GameObject.Find ("HP Potion").GetComponent<Image> ().sprite, "HP Potion");
		} else if (itemName == "Dragon Scale") {
			testAdd (10, 0, "A shimmering green scale that once belonged to a dragon.", GameObject.Find ("Dragon Scale").GetComponent<Image> ().sprite, "Dragon Scale");
		} else if (itemName == "Better HP Potion") {
			testAdd (0, 20, "A savory smelling potion. It reminds you of mom’s cooking.", GameObject.Find ("Better HP Potion").GetComponent<Image> ().sprite, "Better HP Potion");

		} else {
		
		}


	}

	void testAdd(int spChange, int hpChange, string description,Sprite itemImage,string itemName){
		int val;
		if (Inventory.TryGetValue (itemName, out val)) { 
			if (Inventory[itemName] == 0) {
				itemList [slotCount] = Slots [slotCount].AddComponent<Item> () as Item;

				itemList [slotCount].spChange = spChange;

				//                print (itemList [slotCount].spChange);

				itemList [slotCount].hpChange = hpChange;

				itemList [slotCount].description = description;

				itemList [slotCount].itemImage = itemImage;

				itemList [slotCount].itemName = itemName;

				itemList [slotCount].slotNumber = slotCount;

				helpMe = Slots [slotCount].GetComponent<Image> ();

				helpMe.sprite = itemList [slotCount].itemImage;

				Color temp = helpMe.color;

				temp.a = 1;

				helpMe.color = temp;

				//itemList [slotCount].applyValues ();

				Item testingItem = itemList [slotCount];
				Slots [slotCount].GetComponent<Button> ().onClick.AddListener (delegate {
					testingItem.applyValues ();
				});


				slotCount++;




			}
			Inventory [itemName] = val + 1;

		}
	}


	public void testAddStart(string itemName, string[] tempString){

//		print ("itemName: " + itemName);
		if (itemName == "Half Finished Milk Shake") {

			testAddStart (0, 5, "A half finished chocolate milk shake. It’s still somehow creamy and cold.", GameObject.Find ("Half Finished Milk Shake").GetComponent<Image> ().sprite, "Half Finished Milk Shake",tempString);
		} else if (itemName == "Rouge Scale") {
			testAddStart (0, 0, "A shimmering red scale. It sells for a high price, but its origins are unknown.", GameObject.Find ("Rouge Scale").GetComponent<Image> ().sprite, "Rouge Scale",tempString);
		} else if (itemName == "Soul Artifact") {
			testAddStart (20, 0, "A screaming soul rises from a crack in this sacred gem.", GameObject.Find ("Soul Artifact").GetComponent<Image> ().sprite, "Soul Artifact",tempString);
		} else if (itemName == "Wooden Amulet") {
			testAddStart (0, 0, "A finished block of oak with a red ribbon. There’s some scribbling on it in another language.", GameObject.Find ("Wooden Amulet").GetComponent<Image> ().sprite, "Wooden Amulet",tempString);
		} else if (itemName == "Ragged Sock") {
			testAddStart (0, -10, "An old, worn out sock. It smells like feet. Definitely do not eat it.", GameObject.Find ("Ragged Sock").GetComponent<Image> ().sprite, "Ragged Sock",tempString);
		} else if (itemName == "Recorder") {
			testAddStart (0, 0, "A finely tuned recorder. It produces an ominous song.", GameObject.Find ("Recorder").GetComponent<Image> ().sprite, "Recorder",tempString);
		} else if (itemName == "Room Key") {
			testAddStart (0, 0, "An ordinary key. Maybe it unlocks something somewhere?", GameObject.Find ("Room Key").GetComponent<Image> ().sprite, "Room Key",tempString);
		} else if (itemName == "Octopus Leg") {
			testAddStart (0, 5, "A plated pile of wriggly legs. It’s nutritious, probably.", GameObject.Find ("Octopus Leg").GetComponent<Image> ().sprite, "Octopus Leg",tempString);
		} else if (itemName == "Mystery Potion") {
			testAddStart (0, 5, "This elixir smells like cow dung. Maybe it tastes better?", GameObject.Find ("Mystery Potion").GetComponent<Image> ().sprite, "Mystery Potion",tempString);
		} else if (itemName == "HP Potion") {
			testAddStart (0, 10, "A sweet smelling potion. It reminds you of dad’s cooking.", GameObject.Find ("HP Potion").GetComponent<Image> ().sprite, "HP Potion",tempString);
		} else if (itemName == "Dragon Scale") {
			testAddStart (10, 0, "A shimmering green scale that once belonged to a dragon.", GameObject.Find ("Dragon Scale").GetComponent<Image> ().sprite, "Dragon Scale",tempString);
		} else if (itemName == "Better HP Potion") {
			testAddStart (0, 20, "A savory smelling potion. It reminds you of mom’s cooking.", GameObject.Find ("Better HP Potion").GetComponent<Image> ().sprite, "Better HP Potion",tempString);

		} else {


		
		}
		

	}


	void testAddStart(int spChange, int hpChange, string description,Sprite itemImage,string itemName, string[] tempString){
		int multipleItems = 0;
		bool multipleItemsBOOL = false;
		for (int i = 0; i < 12; i++) {
		
			if (itemName == tempString [i]) {

				multipleItems++;
			
			}
		
		
		}
			
	


		if (multipleItems == 1) {

//			print (itemName);


			itemList [slotCount] = Slots [slotCount].AddComponent<Item> () as Item;

			itemList [slotCount].spChange = spChange;

			//                print (itemList [slotCount].spChange);

			itemList [slotCount].hpChange = hpChange;

			itemList [slotCount].description = description;

			itemList [slotCount].itemImage = itemImage;

			itemList [slotCount].itemName = itemName;

			itemList [slotCount].slotNumber = slotCount;

			helpMe = Slots [slotCount].GetComponent<Image> ();

			helpMe.sprite = itemList [slotCount].itemImage;

			Color temp = helpMe.color;

			temp.a = 1;

			helpMe.color = temp;

			//itemList [slotCount].applyValues ();

			Item testingItem = itemList [slotCount];
			Slots [slotCount].GetComponent<Button> ().onClick.AddListener (delegate {
				testingItem.applyValues ();
			});


			slotCount++;

		}




	}


	void Update () {


	}




	public void setNapsack(bool x){
		napsackOpen = x;
	}

	public bool getNapsack(){
		return napsackOpen;
	}

	public void setNapsackCanOpen(bool x){
		napsackCanOpen = x;
	}





	public void removeObject(string itemName, int slotNumber){

		print ("REMOVE OBJECT");

		int val;
//		if (pauseBool) {
			if (Inventory.TryGetValue (itemName, out val)) {
				print (itemName + " val: " + val);

				if (val > 0) {
				
					val--;
					Inventory [itemName] = val;
//					pauseBool = false;
					if (val == 0) {
						Image tempImage = Slots [slotNumber].GetComponent<Image> ();

						//remove
						tempImage.sprite = null;

						Color c = tempImage.color;
						c.a = 0;
						tempImage.color = c;

						Destroy (Slots [slotNumber].GetComponent<Item> ());
						removeSlots (itemName);

						Slots [slotCount].GetComponent<Button> ().onClick.RemoveAllListeners ();
						slotCount--;



		
					}
//					pauseBool = false;
//					StartCoroutine (clickPause (pauseBool));

//				}
			}
		}

	}


	IEnumerator clickPause(bool pauseBool){
		
			yield return new WaitForSeconds (0.5f);
			pauseBool = true;

	}

	void moveSlots(int slotNumber){

		for (int i = slotNumber; i < 12; i++) {

			Slots [slotNumber] = Slots [slotNumber + 1];



		}

	}


	void removeSlots(string itemName){
		StreamReader inp_stm = new StreamReader("assets/CombatAssets/InventoryScript/napsackTXT.text");
		string tempString = "";
		while(!inp_stm.EndOfStream)
		{
			
			string inp_ln = inp_stm.ReadLine( );

			if (inp_ln != itemName) {
			
				tempString += "\n" + inp_ln;

			} 


		


		}


		inp_stm.Close( ); 
		System.IO.File.WriteAllText("assets/CombatAssets/InventoryScript/napsackTXT.text",tempString);
	}

	public void fillSlots(){
		string[] tempString = new string[12];
		int i = 0;
		int tempInt = 0;


		for (int k = 0; k < 12; k++) {

			Destroy (Slots [k].GetComponent<Item> ());

			Slots [k].GetComponent<Button> ().onClick.RemoveAllListeners ();
		
		}


		StreamReader inp_stm = new StreamReader("assets/CombatAssets/InventoryScript/napsackTXT.text");
				while(!inp_stm.EndOfStream)
				{
					string inp_ln = inp_stm.ReadLine( );


//			if (inp_ln == "" && tempInt > 0) {
//				helpMe = Slots [slotCount].GetComponent<Image> ();
//
//				helpMe.sprite = itemList [slotCount].itemImage;
//
//				Color temp = helpMe.color;
//
//				temp.a = 0;
//
//				helpMe.color = temp;		
//
//				slotCount++;
//
//			}
						

		
			if(inp_ln != ""){
				
			tempString [i] = inp_ln;
		
			testAddStart (inp_ln,tempString);
			i++;
				tempInt++;



			}




				}
		
				inp_stm.Close( );  

		if (i < 11) {
		
			for (int j = i; j < 12; j++) {


				Image tempImage = Slots [j].GetComponent<Image> ();

				//remove
				tempImage.sprite = null;

				Color c = tempImage.color;
				c.a = 0;
				tempImage.color = c;

				Destroy (Slots [j].GetComponent<Item> ());

				Slots [j].GetComponent<Button> ().onClick.RemoveAllListeners ();

			
			}
		
		}
	
	}






}
