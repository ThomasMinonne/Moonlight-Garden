using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PolvereDiStelleManger : MonoBehaviour
{
	public int PolvereDiStelle;
	public TextMeshProUGUI Text;
	public int piantaCorrente = 0;
	public GameObject[] piante;
	private GameObject daDistruggere;
	private GameObject daAggiungere;
	[SerializeField] CameraMove cm;
	
	
    // Start is called before the first frame update
    void Start()
    {
        Text = FindObjectOfType<TextMeshProUGUI>();
		piantaCorrente = PlayerPrefs.GetInt("Pianta precedente");
		if(piantaCorrente == 0){
			daAggiungere = Instantiate(piante[0], new Vector3(0, -26, 0), Quaternion.identity);
			daAggiungere.name = "Pianta 0";
			daDistruggere = GameObject.Find("Pianta 0");	
		}
		else { 
			daAggiungere = Instantiate(piante[piantaCorrente], new Vector3(0, -26, 0), Quaternion.identity);
			daAggiungere.name = "Pianta " + piantaCorrente;
			daDistruggere = GameObject.Find("Pianta " + piantaCorrente); 
		}
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = PolvereDiStelle.ToString();
		if(Input.GetKeyDown("space")){
			if(PolvereDiStelle >= 3000 && piantaCorrente < piante.Length-1){
				growPlant();
				cm.animationGrow();
			}
		}
    }
	
	public void addPolvere(int n){
		PolvereDiStelle += n;
	}
	
	public void subPolvere(int n){
		PolvereDiStelle -= n;
	}
	
	public void growPlant(){
		Destroy(daDistruggere);
		//Controllo se la pianta non sia al massimo
		piantaCorrente += 1;
		PlayerPrefs.SetInt("Pianta precedente", piantaCorrente);
		daAggiungere = Instantiate(piante[piantaCorrente], new Vector3(0, -26, 0), Quaternion.identity);
		daAggiungere.name = "Pianta " + (piantaCorrente);
		daDistruggere = daAggiungere;
		PolvereDiStelle -= 3000;
		PlayerPrefs.SetInt("PolvereDiStelle", PolvereDiStelle);
		PlayerPrefs.Save();
	}
}
