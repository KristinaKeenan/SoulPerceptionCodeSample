using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public Stats Stats_Script;
	//Napsack Napsack_Script;

	public static bool hasKey = false;
	public static bool hasRecorder = false;

	public bool sleepingGirlActive = false;
	public bool elliotActive = false;
	public bool dreamKeeperActive = false;
	public bool maajiActive = false;

	public static bool sleepingGirlDefeated = false;
	public static bool elliotDefeated = false;
	public static bool dreamKeeperDefeated = false;
	public static bool maajiDefeated = false;

	public string savedLevel = " ";

	public bool loseCase = false;




	Player(){

	}



	public void setSavedLevel(string x){
		savedLevel = x;
	}
	public string getSavedLevel(){
		return savedLevel;
	}
		
	/*functions that get and set whether the player is holding key items*/
	public bool gethasKey(){
		return hasKey;
	}
	public void sethasKey(bool x){
		hasKey = x;
	}
	public bool gethasRecorder(){
		return hasRecorder;
	}
	public void sethasRecorder(bool x){
		hasRecorder = x;
	}
		

	public bool getSleepingGirlActive(){
		return sleepingGirlActive;
	}
	public void setSleepingGirlActive(bool x){
		sleepingGirlActive = x;
	}
	public bool getElliotActive(){
		return elliotActive;
	}
	public void setElliotActive(bool x){
		elliotActive = x;
	}
	public bool getMaajiActive(){
		return maajiActive;
	}
	public void setMaajiActive(bool x){
		maajiActive = x;
	}
	public bool getDreamKeeperActive(){
		return dreamKeeperActive;
	}
	public void setDreamKeeperActive(bool x){
		dreamKeeperActive = x;
	}


	public bool getSleepingGirlDefeated(){
		return sleepingGirlDefeated;
	}
	public void setSleepingGirlDefeated(bool x){
		sleepingGirlDefeated = x;
	}
	public bool getElliotDefeated(){
		return elliotDefeated;
	}
	public void setElliotDefeated(bool x){
		elliotDefeated = x;
	}
	public bool getMaajiDefeated(){
		return maajiDefeated;
	}
	public void setMaajiDefeated(bool x){
		maajiDefeated = x;
	}
	public bool getDreamKeeperDefeated(){
		return dreamKeeperDefeated;
	}
	public void setDreamKeeperDefeated(bool x){
		dreamKeeperDefeated = x;
	}


	public bool getLoseCase(){
		return loseCase;

	}
	public void setLoseCase(bool x){
		loseCase = x;
	}



}