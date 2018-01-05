using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public Napsack napsack;

	public float spChange, hpChange;

	public Sprite itemImage;

	public string description;

	public string itemName;

	BattleSystem battlePlayer;

	public GameObject HP, SP;

	float HPrectTEMP, SPrectTEMP;

	float HPrect, SPrect;

	public int slotNumber;

	bool used = false;

	public PlayerStart playerStart;

	static bool pauseBool = false;


	// Use this for initialization
	void Start () {

		battlePlayer = GameObject.Find ("Combat").GetComponent<BattleSystem> ();
		 playerStart = GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ();
		napsack = GameObject.Find ("Combat").GetComponent<Napsack> ();

		HP = GameObject.Find ("HP");
		SP = GameObject.Find ("SP");

		HPrectTEMP = battlePlayer.HPrectTEMP;
		SPrectTEMP = battlePlayer.SPrectTEMP;

		HPrect = battlePlayer.HPrect;
		SPrect = battlePlayer.SPrect;
		napsack = GameObject.Find ("Combat").GetComponent<Napsack> ();
		hpChange = hpChange / playerStart.getbasePHP ();
		spChange = spChange / playerStart.getbaseSHP ();
	}

	IEnumerator clickPause(){
		
			yield return new WaitForSeconds (0.25f);

		if (itemName != "Room Key" && itemName != "Recorder") {

			if (itemName == "Mystery Potion") {

				int randInt = Random.Range (0, 1);

				if (randInt == 0) {

					hpChange = hpChange;

				} else {

					hpChange = hpChange * -1;


				}



			}

			if (itemName == "Wooden Amulet") {


				GameObject.Find ("Combat").GetComponent<BattleSystem> ().luckNumCount = 3;

			}
			print ("hpchange: " + hpChange);
			print ("spchange: " + spChange);
			HPrectTEMP += (playerStart.getPHPBarSTATIC () * hpChange) * 2;

			SPrectTEMP += (playerStart.getSHPBarSTATIC () * spChange) * 2;

			print ("hprecttempitem: " + HPrectTEMP);
			print ("sprecttempitem: " + SPrectTEMP);


			playerStart.setPHPBar (HPrectTEMP);
			playerStart.setSHPBar (SPrectTEMP);

			if (playerStart.getPHPBar () >= playerStart.getPHPBarSTATIC ()) {

				playerStart.setPHPBar (playerStart.getPHPBarSTATIC ());
				HPrectTEMP = playerStart.getPHPBar ();
				HP.transform.localScale = new Vector3 (.25f, .45f, 1);


				playerStart.setPHP (playerStart.getbasePHP ());




			} else {

				HP.transform.localScale += new Vector3 (hpChange / 2, 0, 0);


				playerStart.setPHP ((int)((HPrectTEMP * playerStart.getbasePHP ()) / HPrect));


			}

			if (playerStart.getSHPBar () >= playerStart.getSHPBarSTATIC ()) {

				playerStart.setSHPBar (playerStart.getSHPBarSTATIC ());
				SPrectTEMP = playerStart.getSHPBar ();


				SP.transform.localScale = new Vector3 (.25f, .4f, 1);

				playerStart.setSHP (playerStart.getbaseSHP ());


			} else {

				SP.transform.localScale += new Vector3 (spChange / 2, 0, 0);
				playerStart.setSHP ((int)((SPrectTEMP * playerStart.getbaseSHP ()) / SPrect));


			}


			if (playerStart.getSHP () > 0) {

				battlePlayer.SPOut.enabled = false;
			}




			napsack.removeObject (itemName, slotNumber);



			print ("player HP: " + playerStart.getPHP ());
			print ("player SP: " + playerStart.getSHP ());


			print ("testing wait function");
			battlePlayer.logText.text += "\n >You used";
			battlePlayer.logText.text += "\n " + itemName;

			battlePlayer.logTextfiller = "\n >You used \n " + itemName;

			battlePlayer.logTextTracker = 2;


			battlePlayer.lineCount += 2;

//		napsack.Slots [slotNumber].GetComponent<Button> ().onClick.AddListener (delegate {
//			applyValues ();
//		});

		}
		pauseBool = false;

	}



	public void applyValues(){	
		if (Input.GetMouseButtonUp (0)) {
			if (!pauseBool) {


//				napsack.Slots [slotNumber].GetComponent<Button> ().onClick.RemoveAllListeners ();


				pauseBool = true;

				StartCoroutine (clickPause ());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
