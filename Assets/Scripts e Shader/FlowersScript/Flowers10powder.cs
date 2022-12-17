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
	private DateTime gather;
	private long gatherTicks;
	
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
		gather = DateTime.Parse(PlayerPrefs.GetString(name + " gather time"));
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

}
