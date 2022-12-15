using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSaveSystem : MonoBehaviour
{
	PolvereDiStelleManger manager;
	public GameObject[] fiori;
	public float Timer = 0;
	public bool SaveGame = false;
	public float TimeCheck = 120f;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
		LoadGameFunc();
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
		for(int i = 0; i < fiori.Length; i++){
			fiori[i].GetComponent<Flowers10powder>().SaveGameFuncFlower();
		}
		PlayerPrefs.SetInt("PolvereDiStelle", manager.PolvereDiStelle);
		PlayerPrefs.Save();
	}
	
	public void LoadGameFunc(){
		for(int i = 0; i < fiori.Length; i++){
			fiori[i].GetComponent<Flowers10powder>().LoadGameFuncFlower();
		}
		manager.PolvereDiStelle = PlayerPrefs.GetInt("PolvereDiStelle");
	}
	
}
