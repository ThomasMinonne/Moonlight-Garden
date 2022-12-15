using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PolvereDiStelleManger : MonoBehaviour
{
	public int PolvereDiStelle;
	public TextMeshProUGUI Text;
	public int piantaCorrente;
	public GameObject[] piante;
	private GameObject daDistruggere;
	private GameObject daAggiungere;
	//public List<GameObject> piante = new List<GameObject>();
	
    // Start is called before the first frame update
    void Start()
    {
        Text = FindObjectOfType<TextMeshProUGUI>();
		daDistruggere = GameObject.Find("Pianta " + (piantaCorrente+1));
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = PolvereDiStelle.ToString();
		if(Input.GetKeyDown("space")){
			growPlant();
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
		daAggiungere = Instantiate(piante[piantaCorrente+1], new Vector3(0, -26, 0), Quaternion.identity);
		piantaCorrente += 1;
		daAggiungere.name = "Pianta " + (piantaCorrente+1);
		daDistruggere = daAggiungere;
	}
	
}
