using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
/*TICKS
- Second : 10000000
- Minute : 600000000
- Hour : 36000000000
- Day : 864000000000
*/
public class Flowers10powder : MonoBehaviour
{
	public bool ready;
	public int secondsCooldown;
	private long tickCooldown;
	private DateTime gather;
	private long gatherTicks;
	string format = "dd/MM/yyyy HH:mm:ss";
	public AudioSource audioSource;
	
	[Space]
	[Header ("Flower settings")]
	public Material Lit;
	public Material Unlit;
	
    // Start is called before the first frame update
    void Start()
    {
		tickCooldown = secondsCooldown*10000000;
		LoadGameFuncFlower();
    }

    // Update is called once per frame
    void Update()
    {
		if(DateTime.Now.Ticks >= (gather.Ticks + tickCooldown)){
			chooseReady(true);
		}
    }
	
	public void SaveGameFuncFlower(){
		PlayerPrefs.SetString(name + " gather time", DateTime.Now.ToString());
		PlayerPrefs.Save();
	}
	
	public void LoadGameFuncFlower(){
		if(PlayerPrefs.GetString(name + " gather time") == ""){
			gather = DateTime.ParseExact("01/01/1991 00:00:00", format, CultureInfo.InvariantCulture);
		} else gather = DateTime.ParseExact(PlayerPrefs.GetString(name + " gather time"), format, CultureInfo.InvariantCulture);
	}
	
	public void chooseReady(bool check){
		ready = check;
		if(check == true){
			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = Unlit;
			transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = Unlit;
		} else {
			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = Lit;
			transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = Lit;
		}
		return;
	}
	
	public bool checkReady(bool check){
		if(ready == check){
			return true;
		}			
		else{return false;}
	}

}
