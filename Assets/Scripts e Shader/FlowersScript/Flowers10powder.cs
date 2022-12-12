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
	PolvereDiStelleManger manager;
	public bool ready1;
	public bool ready2;
	public int secondsCooldown;
	private long tickCooldown;
	private string tempDate;
	private DateTime gather;
	private long gatherTicks = 0;
	private long gatherTicksOnLoad;
	//private string name;
	
    // Start is called before the first frame update
    void Start()
    {
		tickCooldown = secondsCooldown*10000000;
    }

    // Update is called once per frame
    void Update()
    {
		setReady(name);
    }
	
	public void SaveGameFuncFlower(){
		PlayerPrefs.SetString(name + " gather time", gather.ToString());
	}
	
	public void LoadGameFuncFlower(){
		tempDate = PlayerPrefs.GetString(name + " gather time", gather.ToString());
		gatherTicksOnLoad = Convert.ToDateTime(tempDate).Ticks + tickCooldown;
		if(DateTime.Now.Ticks >= gatherTicksOnLoad){
			chooseReady(name, true);
		}
	}
	
	public void setReady(string name){
		if(DateTime.Now.Ticks >= gatherTicks){
			chooseReady(name, true);
		}	
		if (checkReady(name, true)){
			if (Input.GetMouseButtonDown(0)) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
				RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            
				if (hit.collider != null && hit.collider.gameObject.GetComponent<Flowers10powder>().checkReady(hit.collider.gameObject.name ,true)){
					//Debug.Log(hit.GameObject.name);
					manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
					manager.addPolvere(10);
					chooseReady(hit.collider.gameObject.name, false);
					gather = DateTime.Now;
					gatherTicks = DateTime.Now.Ticks + tickCooldown;
				}
			}
		}
	}
	
	public void chooseReady(string name, bool check){
		if(name == "Fiore 1"){
				ready1 = check;
				return;
		}
		if(name == "Fiore 2"){
			ready2 = check;
			return;
		}
	}
	
	public bool checkReady(string name, bool check){
		if(name == "Fiore 1"){
			if(ready1 == check){
				return true;
			}			
			else{return false;}
		}
		if(name == "Fiore 2"){
			if(ready2 == check){
				return true;
			}			
			else{return false;}
		}
		return true;
	}
}
