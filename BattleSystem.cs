using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleSystem : MonoBehaviour {

	PlayerMovement PlayerMove;
	Player Player;
	PlayerStart playerStart;
	Location location;
	Stats stats;

	GameObject Combat;
	GameObject battleScreen;
	GameObject ItemDropBool;
	GameObject xButton, zButton;

	AudioSource AttackSound;
	AudioSource BossSpecialSound;
	AudioSource PlayerSPSound;
	public AudioSource WinSound;

	RectTransform HPrectR;
	RectTransform SPrectR;
	RectTransform EPrectR;

	public float HPrectRhold;
	public float SPrectRhold;
	public float EPrectRhold;


	//placeholder character stats
	int luckNum = 80;
	public int luckNumCount = 0;

	int eHP,pHP, sHP;
	int eluckNum;

	public int lineCount = 0;


	//playerTurn (true is player's turn)
	public bool playerTurn;

	bool inBattle = true;

	public bool monsterGo = false;

	//Health bars
	public GameObject HP;
	public GameObject SP;
	public GameObject EP;
	public float HPrect;
	public float SPrect;
	public float EPrect;

	public float HPrectTEMP;
	public float SPrectTEMP;
	public float EPrectTEMP;

	float HPincN;
	float HPincS;
	float SPinc;
	float EPincN;
	float EPincS;

	//Menu Bar 
	GameObject SelectionArrow;
	int menuPlaceHolder;
	public RawImage SPOut;
	GameObject noEscape;
	bool canEscape;


	//Napsack
	public GameObject Napsack;
	public bool NapsackActive; 


	//Log
	public Text logText;
	public string logTextfiller;
	public int logTextTracker;

	public string bossName;
	public string bossNameTEXT;


	Image monsterSprite;
	Text MonsterName;


	//animators
	Animator enemyAnimator;


	//monster/boss triggers
	string triggerIdle;
	string triggerAttack;
	string triggerHit;

	string triggerAttack1;
	string triggerAttack2;


	public bool monsterAttackCurrent;


	bool bossActive;

	bool maajiActive,dreamKeeperActive,elliotActive,sleepingGirlActive;

	void Start () {

		Napsack combatNap = GameObject.Find ("Combat").GetComponent<Napsack> ();
		combatNap.slotCount = 0;
		combatNap.fillSlots ();


		PlayerMove = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		PlayerMove.setinCombat (true);

		Player = GameObject.Find ("Player").GetComponent<Player> ();
		playerStart = GameObject.Find ("PlayerNap").GetComponent<PlayerStart>();

		location = Player.GetComponent<Location> ();

		GameObject.Find ("Player").GetComponent<InitiateDialogue> ().setA (false);


		Combat = GameObject.Find ("Combat");

		stats = GameObject.Find ("Stats_Icon").GetComponent<Stats> ();
		stats.setStatsCanOpen (false);


		ItemDropBool = GameObject.Find ("ItemDropImage");
		ItemDropBool.SetActive(false);


		battleScreen = GameObject.Find ("BattleScreen");

		battleScreen.SetActive (true);



		pHP = GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getPHP();

		sHP = GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getSHP();


		//Audio Clips

		AttackSound = GameObject.Find ("Attack").GetComponent<AudioSource>();
		BossSpecialSound = GameObject.Find ("Boss Special").GetComponent<AudioSource>();
		PlayerSPSound = GameObject.Find ("Player Special").GetComponent<AudioSource>();
		WinSound = GameObject.Find ("WinSound").GetComponent<AudioSource>();




		//playerTurn
		playerTurn = true;

		//health bars
		HP = GameObject.Find ("HP");
		SP = GameObject.Find ("SP");
		EP = GameObject.Find ("EP");

		//keeps track of health bar size


		HP.transform.localScale = new Vector3 ((pHP*.25f)/GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbasePHP(), .4f, 0);
		SP.transform.localScale = new Vector3 ((sHP*.25f)/GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbaseSHP(), .4f, 0);
		EP.transform.localScale = new Vector3 (.25f, .4f, 0);

		bossActive = false;
		maajiActive = Player.getMaajiActive();
		elliotActive = Player.getElliotActive();
		sleepingGirlActive = Player.getSleepingGirlActive();
		dreamKeeperActive = Player.getDreamKeeperActive();



		enemyAnimator = GameObject.Find ("MonsterImage").GetComponent<Animator> ();

		noEscape = GameObject.Find ("NoEscape");


		MonsterName = GameObject.Find("MonsterName").GetComponent<Text>();


		chooseMonster ();

		monsterAttackCurrent = false;
	

		HPrectR = HP.GetComponent<RectTransform> ();
		HPrect = playerStart.getPHPBarSTATIC();
		HPrectTEMP = playerStart.getPHPBar();
//		print ("HPrecttempinitial: "+HPrectTEMP);
		HPincN = 5.0f / playerStart.getbasePHP();
		HPincS = 10.0f / playerStart.getbasePHP();
		System.Math.Round (HPincN, 2);
		System.Math.Round(HPincS,2);

		SPrectR = SP.GetComponent<RectTransform> ();
		SPrect = playerStart.getSHPBarSTATIC();
		SPrectTEMP = playerStart.getSHPBar();
//		print ("SPrecttempinitial: "+SPrectTEMP);

		SPinc = 20.0f / playerStart.getbaseSHP();
		System.Math.Round(SPinc,2);


		EPrectR = EP.GetComponent<RectTransform> ();
		EPrect = EPrectR.rect.width;
		EPrectTEMP = EPrect;
		EPincN = 5.0f / eHP;
		EPincS = 10.0f / eHP;
		System.Math.Round (EPincN, 2);
		System.Math.Round (EPincS, 2);



		//menu bar
		SelectionArrow = GameObject.Find ("SelectionArrow");
		RectTransform arrowpos = SelectionArrow.GetComponent<RectTransform> ();
		arrowpos.anchoredPosition = new Vector2(-15f, -39.4f);





		menuPlaceHolder = 0;
		SPOut = GameObject.Find ("SPOut").GetComponent<RawImage>();
		//SPOut.SetActive (false);
		SPOut.enabled = false;

		//napsack
		Napsack = GameObject.Find ("NapsackImage");
		Napsack.SetActive(false);
		NapsackActive = false;


		//log
		logText = GameObject.Find("LogText").GetComponent<Text>();
		logText.text = "Battle Start!";
		logTextfiller = "";
		logTextTracker = 0;



		xButton = GameObject.Find ("x");
		xButton.SetActive (false);

		//zButton = GameObject.Find("z");
		//zButton.SetActive(false);



	}


	IEnumerator startSleepingGirl(){
	

		yield return new WaitForSeconds(3);
		enemyAnimator.ResetTrigger ("SleepingGirl_Transformation");
	
		enemyAnimator.SetTrigger ("startSleepingGirl");

	}


	void chooseMonster(){

		Player.setSavedLevel(location.getLevel ());

	
	
		if(dreamKeeperActive && !Player.getDreamKeeperDefeated()){
			location.setLevel("dreamkeeper");
			bossName = "Dream Keeper";
			bossNameTEXT = bossName;
			eHP = 600;
			eluckNum = 80;
			triggerAttack1 = "DreamKeeper_Attack1";
			triggerAttack2 = "DreamKeeper_Attack2";
			triggerHit = "DreamKeeper_Hurt";
			triggerIdle = "DreamKeeper_Still";
			enemyAnimator.SetTrigger ("startDreamKeeper");
			bossActive = true;
			noEscape.SetActive (true);
			canEscape = false;



		}

		else if(maajiActive && !Player.getMaajiDefeated()){
			location.setLevel("maaji");
			bossName = "Maaji";
			bossNameTEXT = bossName;
			eHP = 500;
			eluckNum = 80;
			triggerAttack1 = "Maaji_Attack1";
			triggerAttack2 = "Maaji_Attack2";
			triggerHit = "Maaji_Hurt";
			triggerIdle = "Maaji_Still";
			enemyAnimator.SetTrigger ("startMaaji");
			bossActive = true;
			noEscape.SetActive(true);
			canEscape = false;




		
		}

		else if(sleepingGirlActive && !Player.getSleepingGirlDefeated()){

			location.setLevel("sleeping girl");
			bossName = "Sleeping Girl";
			bossNameTEXT = bossName;
			eHP = 400;
			eluckNum = 70;
			triggerAttack1 = "SleepingGirl_Attack1";
  		triggerAttack2 = "SleepingGirl_Attack2";
			triggerHit = "SleepingGirl_Hurt";
			triggerIdle = "SleepingGirl_Still";
			enemyAnimator.SetTrigger ("SleepingGirl_Transformation");
			StartCoroutine (startSleepingGirl ());
			bossActive = true;
			noEscape.SetActive(true);
			canEscape = false;

		}


		else if(elliotActive && !Player.getElliotDefeated()){
			location.setLevel("elliot");
			bossName = "Elliot";
			bossNameTEXT = bossName;
			eHP = 300;
			eluckNum = 60;
			triggerAttack1 = "Elliot_Attack1";
			triggerAttack2 = "Elliot_Attack2";
			triggerHit = "Elliot_Hurt";
			triggerIdle = "Elliot_Still";
			enemyAnimator.SetTrigger ("startElliot");
			bossActive = true;
			noEscape.SetActive(true);
			canEscape = false;



		}

		else{

			noEscape.SetActive(false);
			canEscape = true;
		location.setLevel("combat");

		int randNum = Random.Range (0, 4);

		if (randNum == 0) {
			bossName = "egg";
			bossNameTEXT = "Deviled Egg";
			eHP = 50;
			eluckNum = 30;
			triggerAttack = "egg_attack";
			triggerHit = "egg_hit";
			triggerIdle = "egg_idle";
			enemyAnimator.SetTrigger ("startEgg");


		} else if (randNum == 1) {
			bossName = "dog";
			bossNameTEXT = "Shadow Howler";
			eHP = 75;
			eluckNum = 40;
			triggerAttack = "dog_attack";
			triggerHit = "dog_hit";
			triggerIdle = "dog_idle";
			enemyAnimator.SetTrigger ("startDog");



		} else if (randNum == 2) {
			bossName = "fish";
			bossNameTEXT = "King Carp";
			eHP = 150;
			eluckNum = 50;
			triggerAttack = "fish_attack";
			triggerHit = "fish_hit";
			triggerIdle = "fish_idle";
			enemyAnimator.SetTrigger ("startFish");


		} else {
			bossName = "teru";
			bossNameTEXT = "Teru Teru";
			eHP = 200;
			eluckNum = 50;
			triggerAttack = "teru_attack";
			triggerHit = "teru_hit";
			triggerIdle = "teru_idle";
			enemyAnimator.SetTrigger ("startTeru");


		}




		enemyAnimator.SetTrigger (triggerIdle);
		}


		MonsterName.text = bossNameTEXT;

	}


	//Character attacks Monster
	void charAttack(){

		bool hit = false;
		//Miss/Standard/Critical
		int randomNum = Random.Range(0,luckNum);

		if (randomNum <= 10) {
		
			logText.text += "\n >Miss";
			logTextfiller = "\n >Miss";
			logTextTracker = 1;

			lineCount++;
			hit = false;


		} else if (randomNum > 10 && randomNum <= 60) {
			enemyAnimator.ResetTrigger(triggerIdle);

			enemyAnimator.SetTrigger (triggerHit);




			EP.transform.localScale += new Vector3 (-EPincN/2,0,0);


			EPrectTEMP -= (EPrect * EPincN)*2;


			logText.text += "\n >Hit";
			logTextfiller = "\n >Hit";
			logTextTracker = 1;


			lineCount++;
			hit = true;





		} else {
			
			enemyAnimator.ResetTrigger(triggerIdle);

			enemyAnimator.SetTrigger (triggerHit);

			EP.transform.localScale += new Vector3 (-EPincS/2,0,0);




			EPrectTEMP -= (EPrect * EPincS)*2;

			logText.text += "\n >Critical Hit";
			logTextfiller = "\n >Critical Hit";
			logTextTracker = 1;


			lineCount++;

			hit = true;





		}


		//checks to see if enemy is dead
		if (EPrectTEMP <= 0) {
			EPrectTEMP = 0;
			EP.transform.localScale = new Vector3 (0, 0, 0);
			logText.text += "\n >You've won!";
			logTextfiller = "\n >You've won!";
			logTextTracker = 1;


			lineCount++;
			ItemDropBool.SetActive (true);
			xButton.SetActive (true);
			//zButton.SetActive (true);

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHP (pHP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHP (sHP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHPBar (HPrectTEMP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHPBar (SPrectTEMP);




			if (bossName == "Maaji") {
				Destroy (GameObject.Find ("Maaji").GetComponent<SpriteRenderer> ());
				Player.setMaajiActive (false);
				Player.setMaajiDefeated (true);

			}
			if (bossName == "Dream Keeper") {
				Destroy (GameObject.Find ("Dreamkeeper").GetComponent<SpriteRenderer> ());
				Player.setDreamKeeperActive (false);
				Player.setDreamKeeperDefeated (true);
				PlayerMove.setinCombat (false);
				Combat.SetActive (false);


				GameObject.Find ("Player").GetComponent<InitiateDialogue> ().setA (true);
				inBattle = false;

				Destroy (Combat.GetComponent<BattleSystem> ());
				SceneManager.LoadScene ("FinalLevel");

			} else {


				if (bossName == "Elliot") {
					Destroy (GameObject.Find ("Elliot").GetComponent<SpriteRenderer> ());
					Player.setElliotActive (false);
					Player.setElliotDefeated (true);

				}
				if (bossName == "Sleeping Girl") {
					Destroy (GameObject.Find ("Sleeping Girl").GetComponent<SpriteRenderer> ());
					Player.setSleepingGirlActive (false);
					Player.setSleepingGirlDefeated (true);

				}

				noEscape.SetActive (true);
				StartCoroutine (playWinSound ());
				Combat.AddComponent<ItemDrop> ();
				Napsack.SetActive (true);

				inBattle = false;


			}

		



		} else {

			StartCoroutine (monsterHitDelay (hit,false));

		}


	}




	//Character uses soul perception
	void soulPerception(){

		bool hit = true;

		EP.transform.localScale += new Vector3 (-EPincS/2,0,0);

		EPrectTEMP -= (EPrect * EPincS)*2;



		SP.transform.localScale += new Vector3 (-SPinc/2,0,0);

		SPrectTEMP -= (SPrect * SPinc)*2;
//		print ("sprecttemp: "+SPrectTEMP);

		sHP = (int)((SPrectTEMP * GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbaseSHP())/SPrect);

		logText.text += "\n >You use";
		logText.text += "\n Soul Perception";
		logTextfiller = "\n >You use \n Soul Perception";
		logTextTracker = 2;

		lineCount += 2;

	



		//checks to see if enemy is dead
		if (EPrectTEMP <= 0) {
			EPrectTEMP = 0;
			EP.transform.localScale = new Vector3 (0, 0, 0);
			logText.text += "\n >You've won!";
			logTextfiller = "\n >You've won!";
			logTextTracker = 1;


			lineCount++;
			ItemDropBool.SetActive (true);
			xButton.SetActive (true);
			//zButton.SetActive (true);

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHP (pHP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHP (sHP);

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHPBar (HPrectTEMP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHPBar (SPrectTEMP);


			if (bossName == "Maaji") {
				Destroy (GameObject.Find ("Maaji").GetComponent<SpriteRenderer> ());
				Player.setMaajiActive (false);
				Player.setMaajiDefeated (true);

			}
			if (bossName == "Dream Keeper") {
				Destroy (GameObject.Find ("Dreamkeeper").GetComponent<SpriteRenderer> ());
				Player.setDreamKeeperActive (false);
				Player.setDreamKeeperDefeated (true);
				PlayerMove.setinCombat (false);
				Combat.SetActive (false);


				GameObject.Find ("Player").GetComponent<InitiateDialogue> ().setA (true);
				inBattle = false;

				Destroy (Combat.GetComponent<BattleSystem> ());
				SceneManager.LoadScene ("FinalLevel");
			} else {


				if (bossName == "Elliot") {
					Destroy (GameObject.Find ("Elliot").GetComponent<SpriteRenderer> ());
					Player.setElliotActive (false);
					Player.setElliotDefeated (true);

				}
				if (bossName == "Sleeping Girl") {
					Destroy (GameObject.Find ("Sleeping Girl").GetComponent<SpriteRenderer> ());
					Player.setSleepingGirlActive (false);
					Player.setSleepingGirlDefeated (true);

				}

				noEscape.SetActive (true);
				StartCoroutine (playWinSound ());

				Combat.AddComponent<ItemDrop> ();
				Napsack.SetActive (true);

				inBattle = false;


			}
		






		} else {

			//checks to see if you are out of soul perception
			if (SPrectTEMP <= 0) {
				SPrectTEMP = 0;
				sHP = 0;
				SP.transform.localScale = new Vector3 (0, 0, 0);
				logText.text += "\n >You have no more";
				logText.text += "\n Soul Perception";
				logTextfiller = "\n >You have no more \n Soul Perception";
				logTextTracker = 2;


				lineCount += 2;
				SPOut.enabled = true;



			}


			StartCoroutine (monsterHitDelay (hit,true));
		}


	}

	//Monster attacks character
	void BossAttack(){
		bool attack = false;
		int attackNum = 0;
		//Miss/Standard/Special
		int eRandomNum = Random.Range(0,eluckNum);

		if (eRandomNum <= 10) {

			logText.text += "\n >You dodged";
			logTextfiller = "\n >You dodged";
			logTextTracker = 1;


			lineCount++;
			attack = false;


		} else if (eRandomNum > 10 && eRandomNum < 50) {
			HP.transform.localScale += new Vector3 (-HPincN/2, 0, 0);


			HPrectTEMP -= (HPrect * HPincN)*2;

			pHP = (int)((HPrectTEMP * GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbasePHP())/HPrect);


			logText.text += "\n >You've been hit";
			logTextfiller = "\n >You've been hit";
			logTextTracker = 1;


			lineCount++;
			attack = true;
			attackNum = 1;



		} else {
			HP.transform.localScale += new Vector3 (-HPincS/2, 0, 0);

			HPrectTEMP -= (HPrect * HPincS)*2;

			logText.text += "\n >You've been";
			logText.text += "\n critically hit";
			logTextfiller = "\n >You've been \n critically hit";			
			logTextTracker = 2;



			pHP = (int)((HPrectTEMP * GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbasePHP())/HPrect);



			lineCount += 2;
			attack = true;
			attackNum = 2;


		}
		//checks to see if you are dead
		if (HPrectTEMP <= 0) {
			HPrectTEMP = 0;
			HP.transform.localScale = new Vector3 (0,0,0);
			logText.text += "\n >You've been defeated";
			logTextfiller = "\n >You've been defeated";
			logTextTracker = 1;


			lineCount++;

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHP (pHP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHP (sHP);

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHPBar (HPrectTEMP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHPBar (SPrectTEMP);

			Destroy (Combat.GetComponent<BattleSystem> ());
			Player.setLoseCase (true); 

			inBattle = false;


		}


		StartCoroutine (bossAttackDelay (attack,attackNum));


	}
	IEnumerator bossAttackDelay(bool attack, int attackNum){


		if (attack && attackNum == 1) {
			AttackSound.Play ();

			monsterAttackCurrent = false;


			enemyAnimator.ResetTrigger (triggerIdle);

			enemyAnimator.SetTrigger (triggerAttack1);

			yield return new WaitForSeconds(3);
//			AttackSound.SetActive (false);

			enemyAnimator.ResetTrigger (triggerAttack1);

			enemyAnimator.SetTrigger (triggerIdle);

			playerTurn = true;


		} else if (attack && attackNum == 2) {

			BossSpecialSound.Play();

			monsterAttackCurrent = false;


			enemyAnimator.ResetTrigger (triggerIdle);

			enemyAnimator.SetTrigger (triggerAttack2);

			yield return new WaitForSeconds(3);
//			BossSpecialSound.SetActive (false);


			enemyAnimator.ResetTrigger (triggerAttack2);

			enemyAnimator.SetTrigger (triggerIdle);

			playerTurn = true;


		} else {

			monsterAttackCurrent = false;

			yield return new WaitForSeconds(1);

			playerTurn = true;


		}






	} 




	//Monster attacks character
	void MonsterAttack(){
		bool attack = false;
		//Miss/Standard/Special
		int eRandomNum = Random.Range(0,eluckNum);
	
		if (eRandomNum <= 10) {

			logText.text += "\n >You dodged";
			logTextfiller = "\n >You dodged";
			logTextTracker = 1;


			lineCount++;
			attack = false;


		} else {
			HP.transform.localScale += new Vector3 (-HPincN / 2, 0, 0);


			HPrectTEMP -= (HPrect * HPincN) * 2;
			pHP = (int)((HPrectTEMP * GameObject.Find("PlayerNap").GetComponent<PlayerStart>().getbasePHP())/HPrect);

			logText.text += "\n >You've been hit";
			logTextfiller = "\n >You've been hit";
			logTextTracker = 1;


			lineCount++;
			attack = true;

		}
		//checks to see if you are dead
		if (HPrectTEMP <= 0) {
			HPrectTEMP = 0;
			HP.transform.localScale = new Vector3 (0, 0, 0);
			logText.text += "\n >You've been defeated!";
			logTextfiller = "\n >You've been defeated!";
			logTextTracker = 1;

			lineCount++;

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHP (pHP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHP (sHP);

			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHPBar (HPrectTEMP);
			GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHPBar (SPrectTEMP);


			Destroy (Combat.GetComponent<BattleSystem> ());
			Player.setLoseCase (true);

			inBattle = false;


		} else {
			

			StartCoroutine (monsterAttackDelay (attack));
		}

	
	}

	//monster hit delay
	IEnumerator monsterHitDelay(bool hit,bool SP){
	
		if (hit && !SP) {
			AttackSound.Play();

			playerTurn = false;

		
			enemyAnimator.ResetTrigger (triggerIdle);

			enemyAnimator.SetTrigger (triggerHit);

			yield return new WaitForSeconds (2);
//			AttackSound.SetActive (false);


			enemyAnimator.ResetTrigger (triggerHit);

			enemyAnimator.SetTrigger (triggerIdle);
			monsterAttackCurrent = true;



		} else if (hit && SP) {
		
			PlayerSPSound.Play ();

			playerTurn = false;


			enemyAnimator.ResetTrigger (triggerIdle);

			enemyAnimator.SetTrigger (triggerHit);

			yield return new WaitForSeconds (2);
//			PlayerSPSound.SetActive (false);


			enemyAnimator.ResetTrigger (triggerHit);

			enemyAnimator.SetTrigger (triggerIdle);
			monsterAttackCurrent = true;
		
		}else {
			playerTurn = false; 

			yield return new WaitForSeconds(1);
			monsterAttackCurrent = true;


		
		}

		monsterGo = false;
	
	}


	//monster attack delay
	IEnumerator monsterAttackDelay(bool attack){
	

		if (attack) {
			AttackSound.Play ();

			monsterAttackCurrent = false;


			enemyAnimator.ResetTrigger (triggerIdle);

			enemyAnimator.SetTrigger (triggerAttack);

			yield return new WaitForSeconds(2);

//			AttackSound.SetActive (false);

			enemyAnimator.ResetTrigger (triggerAttack);

			enemyAnimator.SetTrigger (triggerIdle);

			playerTurn = true;


		} else {

			monsterAttackCurrent = false;

			yield return new WaitForSeconds(2);
					
			playerTurn = true;

		
		}


	
	
	} 


	IEnumerator charAttackpause(){
		yield return new WaitForSeconds(3);
		monsterGo = true;

	}

	//Escape animation delay
	IEnumerator escapePause(){

		yield return new WaitForSeconds(1);

	}



	IEnumerator playWinSound(){

		WinSound.Play ();
	
		yield return new WaitForSeconds(1);
//		WinSound.SetActive (false);

	
	}


	void tempLuckNum(){

		if (luckNumCount > 0) {
		
			luckNum = 100;
			luckNumCount--;
		
		} else {
		
			luckNum = 80;
		}

	}



	// Update is called once per frame
	void Update () {

		if (SPrectTEMP <= 0) {
			SPOut.enabled = true;

		} else {
		
			SPOut.enabled = false;

		}




		if (inBattle) {
			if (playerTurn) {
//				print ("Napsack active: " +NapsackActive);

				if (!NapsackActive) {

					//menu movement
					if (Input.GetKeyUp ("right")) {
						if (menuPlaceHolder < 3) {

							menuPlaceHolder++;
		
		

							SelectionArrow.transform.localPosition += new Vector3 (33.0f, 0, 0);
						}
		
					}
					if (Input.GetKeyUp ("left")) {


						if (menuPlaceHolder > 0) {

							menuPlaceHolder--;
			

							SelectionArrow.transform.localPosition += new Vector3 (-33.0f, 0, 0);
						} 
					}
				}
				//menu selection
				if (Input.GetKeyDown (KeyCode.Space)) {
		
					if (menuPlaceHolder == 0) {
						if (EPrectTEMP > 0) {
							charAttack ();
						}
			
					} else if (menuPlaceHolder == 1) {
						if (GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().getSHP () > 0 && EPrect > 0) {
							SPOut.enabled = false;

							soulPerception ();
						}
						

			
					} else if (menuPlaceHolder == 2) {

						NapsackActive = !NapsackActive;
						Napsack.SetActive(NapsackActive);
//						inventoryNapsack.enabled = NapsackActive;
			
					} else {
			
			
						if(canEscape){

						int randNumE = Random.Range (0, 10);

						if (randNumE >= 8) {
							logText.text += "\n >You escaped!";
								logTextfiller = "\n >You escaped!";
								logTextTracker = 1;


							lineCount++;
						
							PlayerMove.setinCombat (false);
							Combat.SetActive (false);
							ItemDropBool.SetActive (true);
							Napsack.SetActive (true);


							xButton.SetActive (true);
							//zButton.SetActive(true);

								GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHP (pHP);
								GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHP (sHP);


								GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setPHPBar (HPrectTEMP);
								GameObject.Find ("PlayerNap").GetComponent<PlayerStart> ().setSHPBar (SPrectTEMP);
							GameObject.Find ("Player").GetComponent<InitiateDialogue> ().setA (true);

							location.setLevel (Player.getSavedLevel());

								stats.setStatsCanOpen (true);

								noEscape.SetActive(true);

							Destroy (Combat.GetComponent<BattleSystem> ());




						
						} else {
						
							logText.text += "\n  >You did not escape";
								logTextfiller = "\n >You did not escape";
								logTextTracker = 2;


							lineCount += 2;
							playerTurn = false;
							StartCoroutine (escapePause ());

							monsterAttackCurrent = true;
						
						}
						}
					}
		
		
				}

			} else {



				if (monsterAttackCurrent) {
						StartCoroutine (charAttackpause ());
					
					if (monsterGo) {
						if (!bossActive) {
							MonsterAttack ();
							monsterGo = false;

						} else {
							BossAttack ();
							monsterGo = false;

						}
					}
				}


		
			}


			tempLuckNum ();
		}



		if (lineCount >= 9) {
			logText.text = logTextfiller;
			lineCount = logTextTracker;
		}
	}
}
