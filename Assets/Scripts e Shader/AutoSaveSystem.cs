using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

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
	string format = "dd/MM/yyyy HH:mm:ss";
	
    // Start is called before the first frame update
    void Start()
    {
		tickCooldown = secondsCooldown*10000000;
        manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
		currentPhase = PlayerPrefs.GetInt("Last moon phase");
		currentConstellation = PlayerPrefs.GetInt("Old constellation");
		GameObject temp = Instantiate(moon[currentPhase], new Vector3(0, 9, 0), Quaternion.identity);
		temp.name = "Moon " + (currentPhase);
		GameObject tempConstellation = Instantiate(constellation[currentConstellation], null);
		tempConstellation.transform.position = new Vector3 ( tempConstellation.transform.position.x + tempConstellation.GetComponent<Constellation>().xOffset, tempConstellation.transform.position.y + tempConstellation.GetComponent<Constellation>().yOffset, 0);
		for(int i = 0; i < tempConstellation.GetComponent<Constellation>().lines.Length; i++){
			Vector3[] tempPos2 = new Vector3[tempConstellation.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().positionCount];
			tempConstellation.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().GetPositions(tempPos2);
			for(int j = 0; j < tempPos2.Length; j++){
				tempConstellation.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().SetPosition(j, new Vector3 (tempPos2[j].x + tempConstellation.GetComponent<Constellation>().xOffset, tempPos2[j].y + tempConstellation.GetComponent<Constellation>().yOffset, tempPos2[j].z));
			}
		}
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
		if(PlayerPrefs.GetString("Moon time phases time") == ""){
			moontime = DateTime.ParseExact("01/01/1991 00:00:00", format, CultureInfo.InvariantCulture);
		} else moontime = DateTime.ParseExact(PlayerPrefs.GetString("Moon time phases time"), format, CultureInfo.InvariantCulture);
	}
	
	public void nextPhase(){
		Debug.Log(currentPhase);
		PlayerPrefs.SetString("Moon time phases time", DateTime.Now.ToString());
		if(PlayerPrefs.GetString("Moon time phases time") == ""){
			moontime = DateTime.ParseExact("01/01/1991 00:00:00", format, CultureInfo.InvariantCulture);
		} else moontime = DateTime.ParseExact(PlayerPrefs.GetString("Moon time phases time"), format, CultureInfo.InvariantCulture);
		PlayerPrefs.SetInt("Old constellation", currentConstellation);
		if(currentPhase < 7 ){
			currentPhase++;
			PlayerPrefs.SetInt("Last moon phase", currentPhase);
		}  else {
			currentPhase = 0;
			PlayerPrefs.SetInt("Last moon phase", currentPhase);
		}
		if(currentConstellation < constellation.Length-1){
			currentConstellation++;
			PlayerPrefs.SetInt("Old constellation", currentConstellation);
		}  else {
			currentConstellation = 0;
			PlayerPrefs.SetInt("Old constellation", currentConstellation);
		}
		Destroy(daDistruggere);
		Destroy(constDaDistruggere);
		daAggiungere = Instantiate(moon[currentPhase], new Vector3(0, 9, 0), Quaternion.identity);
		daAggiungere.name = "Moon " + (currentPhase);
		daDistruggere = daAggiungere;
		constdaAggiungere = Instantiate(constellation[currentConstellation], null);
		constdaAggiungere.transform.position = new Vector3 ( constdaAggiungere.transform.position.x + constdaAggiungere.GetComponent<Constellation>().xOffset, constdaAggiungere.transform.position.y + constdaAggiungere.GetComponent<Constellation>().yOffset, 0);
		for(int i = 0; i < constdaAggiungere.GetComponent<Constellation>().lines.Length; i++){
			Vector3[] tempPos = new Vector3[constdaAggiungere.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().positionCount];
			constdaAggiungere.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().GetPositions(tempPos);
			for(int j = 0; j < tempPos.Length; j++){
				constdaAggiungere.GetComponent<Constellation>().lines[i].GetComponent<LineRenderer>().SetPosition(j, new Vector3 (tempPos[j].x + constdaAggiungere.GetComponent<Constellation>().xOffset, tempPos[j].y + constdaAggiungere.GetComponent<Constellation>().yOffset, tempPos[j].z));
			}
		}
		PlayerPrefs.SetString(constdaAggiungere.name + " complete?", "incomplete");
		constDaDistruggere = constdaAggiungere;
	}
	
}
