using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
	private string tempDate;
	private DateTime gather;
	private long gatherTicks = 0;
	private long gatherTicksOnLoad;
	
    // Start is called before the first frame update
    void Start()
    {
		tickCooldown = secondsCooldown*10000000;
    }

    // Update is called once per frame
    void Update()
    {
		if(DateTime.Now.Ticks >= gatherTicks){
			chooseReady(true);
		}
    }
	
	public void SaveGameFuncFlower(){
		PlayerPrefs.SetString(name + " gather time", gather.ToString());
	}
	
	public void LoadGameFuncFlower(){
		tempDate = PlayerPrefs.GetString(name + " gather time", gather.ToString());
		gatherTicksOnLoad = Convert.ToDateTime(tempDate).Ticks + tickCooldown;
		if(DateTime.Now.Ticks >= gatherTicksOnLoad){
			chooseReady(true);
		}
	}
	
	public void chooseReady(bool check){
		ready = check;
		return;
	}
	
	public bool checkReady(bool check){
		if(ready == check){
			return true;
		}			
		else{return false;}
	}
	
	public void resetTimer(){
		gather = DateTime.Now;
		gatherTicks = DateTime.Now.Ticks + tickCooldown;
	}
}
