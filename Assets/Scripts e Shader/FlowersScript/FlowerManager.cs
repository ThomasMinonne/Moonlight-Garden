using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    PolvereDiStelleManger manager;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
		
			if (hit.collider != null && hit.collider.gameObject.GetComponent<Flowers10powder>().checkReady(true)){
				Debug.Log(hit.collider.gameObject.name);
				manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
				manager.addPolvere(10);
				hit.collider.gameObject.GetComponent<Flowers10powder>().chooseReady(false);
				hit.collider.gameObject.GetComponent<Flowers10powder>().resetTimer();
			}
		}
    }
}
