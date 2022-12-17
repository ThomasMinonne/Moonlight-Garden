using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
	public int stars;
	private int link = 0;
	private bool complete = false;
	PolvereDiStelleManger manager;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
		
			if (hit.collider != null && complete == false){
				if(hit.collider.gameObject.tag == "Star"){
					link++;
					if(link == stars){
						complete = true;
						manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
						manager.addPolvere(10*stars);
						/*hit.collider.gameObject.GetComponent<Flowers10powder>().chooseReady(false);
						Animate(hit.collider.gameObject.transform.position, 10);
						hit.collider.gameObject.GetComponent<Flowers10powder>().SaveGameFuncFlower();
						hit.collider.gameObject.GetComponent<Flowers10powder>().LoadGameFuncFlower();*/
					}
				} else link = 0;
				/*hit.collider.gameObject.GetComponent<Flowers10powder>().chooseReady(false);
				Animate(hit.collider.gameObject.transform.position, 10);
				hit.collider.gameObject.GetComponent<Flowers10powder>().SaveGameFuncFlower();
				hit.collider.gameObject.GetComponent<Flowers10powder>().LoadGameFuncFlower();*/
			}
		}
    }
}
