using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AutoSaveSystem : MonoBehaviour
{
	PolvereDiStelleManger manager;
	public GameObject[] moon;
	public GameObject[] constellation;
	public float Timer = 0;
	public bool SaveGame = false;
	public float TimeCheck = 120f;
	private long tickCooldown;
	public int secondsCooldown;
	private int currentPhase;
	private GameObject daAggiungere;
	private GameObject daDistruggere;
	private GameObject constdaAggiungere;
	private GameObject constDaDistruggere;
	private int currentConstellation;
	private DateTime moontime;
	
    // Start is called before the first frame update
    void Start()
    {
		tickCooldown = secondsCooldown*10000000;
        manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
		currentPhase = PlayerPrefs.GetInt("Last moon phase");
		currentConstellation = PlayerPrefs.GetInt("Old constellation");
		GameObject temp = Instantiate(moon[currentPhase], new Vector3(0, 0, 0), Quaternion.identity);
		temp.name = "Moon " + (currentPhase);
		Instantiate(constellation[currentConstellation], new Vector3(0, 0, 0), Quaternion.identity);
		LoadGameFunc();
		if(DateTime.Now.Ticks >= (moontime.Ticks + tickCooldown)){
			daDistruggere = GameObject.Find("Moon " + (currentPhase));
			constDaDistruggere = GameObject.FindGameObjectsWithTag("Constellation")[0];
			nextPhase();
		}
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + 1 * Time.deltaTime;
		if (Timer >= TimeCheck){
			SaveGame = true;
		}
		if (SaveGame == true){
			SaveGameFunc();
			LoadGameFunc();
			SaveGame = false;
			Timer = 0f;
		}
    }
	
	public void SaveGameFunc(){
		PlayerPrefs.SetInt("PolvereDiStelle", manager.PolvereDiStelle);
		PlayerPrefs.Save();
	}
	
	public void LoadGameFunc(){
		manager.PolvereDiStelle = PlayerPrefs.GetInt("PolvereDiStelle");
		moontime = DateTime.Parse(PlayerPrefs.GetString("Moon time phases time"));
	}
	
	public void nextPhase(){
		PlayerPrefs.SetString("Moon time phases time", DateTime.Now.ToString());
		moontime = DateTime.Parse(PlayerPrefs.GetString("Moon time phases time"));
		if(currentPhase < 7 ){
			currentPhase++;
			PlayerPrefs.SetInt("Last moon phase", currentPhase);
		}  else currentPhase = 0;
		if(currentConstellation < constellation.Length){
			currentConstellation++;
			PlayerPrefs.SetInt("Old constellation", currentConstellation);
		}  else currentConstellation = 0;
		Destroy(daDistruggere);
		Destroy(constDaDistruggere);
		daAggiungere = Instantiate(moon[currentPhase], new Vector3(0, 0, 0), Quaternion.identity);
		daAggiungere.name = "Moon " + (currentPhase);
		daDistruggere = daAggiungere;
		constdaAggiungere = Instantiate(constellation[currentConstellation], new Vector3(0, 0, 0), Quaternion.identity);
		constDaDistruggere = constdaAggiungere;
	}
	
}
