using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class NapsackStart : MonoBehaviour {

	public TextAsset inventory_text;



	public Dictionary<string, int> Inventory = new Dictionary<string, int>();

	public int dictionaryCount;


	void Awake(){
	
		DontDestroyOnLoad (GameObject.Find ("NapsackStart"));


	
	}




	// Use this for initialization
	void Start () {



		dictionaryCount = 0;
		readTextFile ("assets/CombatAssets/InventoryScript/inventory_text.text");


		foreach(KeyValuePair<string,int> keyValue in Inventory)
		{
			string key = keyValue.Key;
			int value = keyValue.Value;

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	//reads text from the inventory text file


	void readTextFile(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);

		while(!inp_stm.EndOfStream)
		{
			string inp_ln = inp_stm.ReadLine( );


			Inventory.Add (inp_ln, 0);
			dictionaryCount++;



		}

		inp_stm.Close( );  

	}



}


