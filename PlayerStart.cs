using UnityEngine;
using System.Collections;

public class PlayerStart : MonoBehaviour {

	public int pHP;
	public int sHP;
	public static int baseHP;
	public static int baseSP;

	public float healthbarWidth;
	public float spbarWidth;
	public static float healthbarWidthSTATIC;
	public static float spbarWidthSTATIC;



	void Awake(){

		DontDestroyOnLoad (GameObject.Find("PlayerNap"));
			
	
	}

	// Use this for initialization
	void Start () {

		baseHP = 2000;
		baseSP = 1000;

		pHP = 2000;
		sHP = 1000;

		healthbarWidthSTATIC = 194.00f;
		spbarWidthSTATIC = 194.00f;

		healthbarWidth = 194.00f;
		spbarWidth = 194.00f;
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}




	//current HP
	public int getPHP(){
		return pHP;
	}

	public void setPHP(int x){
		pHP = x;
	}

	//current SP
	public int getSHP(){
		return sHP;
	}

	public void setSHP(int x){
		sHP = x;
	}

	public int getbasePHP(){
		return baseHP;
	}

	public int getbaseSHP(){
		return baseSP;
	}


	//current HPbar
	public float getPHPBar(){
		return healthbarWidth;
	}

	public void setPHPBar(float x){
		healthbarWidth = x;
	}

	//current SPbar
	public float getSHPBar(){
		return spbarWidth;
	}

	public void setSHPBar(float x){
		spbarWidth = x;
	}

	//static HPbar
	public float getPHPBarSTATIC(){
		return healthbarWidthSTATIC;
	}

	//static SPbar
	public float getSHPBarSTATIC(){
		return spbarWidthSTATIC;
	}
}
