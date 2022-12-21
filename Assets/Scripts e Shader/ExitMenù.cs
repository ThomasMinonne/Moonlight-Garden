using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenù : MonoBehaviour
{
    public GameObject menù;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
			if(menù.activeSelf){
				menù.SetActive(false);
			} else menù.SetActive(true);
		}
    }
	
	public void quitgame(){
		Application.Quit();
	}
}
