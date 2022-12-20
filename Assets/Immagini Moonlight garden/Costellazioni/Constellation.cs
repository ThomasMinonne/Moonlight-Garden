using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Constellation : MonoBehaviour
{
	public int stars;
	private int link = 0;
	private bool complete = false;
	public GameObject[] lines;
	public float xOffset = 0;
	public float yOffset = 0;
	PolvereDiStelleManger manager;
	[SerializeField] Vector3 scaleTo;
	
    // Start is called before the first frame update
    void Start()
    {
		if(PlayerPrefs.GetString(name + " complete?") == "complete"){
			setLines();
			setChildInactive();
		}
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
					hit.collider.gameObject.transform.DOScale(scaleTo, 0.3f).SetEase(Ease.InOutBounce).OnComplete(() => { 
						hit.collider.gameObject.transform.DOScale(new Vector3 (1.3f,1.3f,1.3f), 0.1f).SetEase(Ease.InOutBounce);
						hit.collider.enabled = false;
					});
					link++;
					if(link == stars){
						complete = true;
						PlayerPrefs.SetString(name + " complete?", "complete");
						PlayerPrefs.Save();
						manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
						manager.addPolvere(10*stars);
						setLines();
						setChildInactive();
					}
				} else if (hit.collider.gameObject.tag != "Star") {
					link = 0;
					setChildback();
				}
			} else if (hit.collider == null){
				link = 0;
				setChildback();
			}
		}
    }
	
	public void chooseComplete(bool check){
		complete = check;
		return;
	}
	
	public void setLines(){
		for(int i = 0; i < lines.Length; i++){
			lines[i].SetActive(true);
		}
	}
	
	public void setChildback(){
		foreach (Transform child in transform){
			if(child.gameObject.tag == "Star"){
				if(child.localScale != new Vector3 (0.9f,0.9f,0.9f)) {
					child.DOScale(new Vector3 (0.9f,0.9f,0.9f), 0.1f).SetEase(Ease.InOutBounce).OnComplete(() => { 
						child.gameObject.GetComponent<Collider2D>().enabled = true;
					});
				}
			}
		}
	}
	
	public void setChildInactive(){
		foreach (Transform child in transform){
			if(child.gameObject.tag == "Star"){
				child.gameObject.GetComponent<Collider2D>().enabled = false;
			}
		}
	}
}
