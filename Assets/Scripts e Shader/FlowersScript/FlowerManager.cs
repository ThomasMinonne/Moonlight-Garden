using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowerManager : MonoBehaviour
{
    [Header ("UI references")]
	PolvereDiStelleManger manager;
	[SerializeField] GameObject animatedSparklePrefab;
	[SerializeField] Transform target;
	
	[Space]
	[Header ("Sparle to pool")]
	[SerializeField] int sparkle_num;
	Queue<GameObject> sparkleQueue = new Queue<GameObject>();
	
	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;
	[SerializeField] Ease easeType;
	[SerializeField] float spread;
	Vector3 targetPosition;
	
	[Space]
	[Header ("Bucket")]
	[SerializeField] GameObject bucket;
	[SerializeField] Vector3 scaleTo;
	
	//[SerializeField] GameObject catalogo;
	
	void Awake (){
		targetPosition = target.position;

		//prepare pool
		PrepareSparkle();
	}
	
    // Update is called once per frame
    void Update(){
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
		
			//if (hit.collider != null && hit.collider.gameObject.tag != "Star" && hit.collider.gameObject.tag != "Catalogo"){
			if (hit.collider != null && hit.collider.gameObject.tag == "Fiore"){
				if(hit.collider.gameObject.GetComponent<Flowers10powder>().checkReady(true)){
					hit.collider.gameObject.GetComponent<Flowers10powder>().audioSource.Play();
					manager = GameObject.Find("Manager").GetComponent<PolvereDiStelleManger>();
					manager.addPolvere(10);
					hit.collider.gameObject.GetComponent<Flowers10powder>().chooseReady(false);
					Animate(hit.collider.gameObject.transform.position, 10);
					hit.collider.gameObject.GetComponent<Flowers10powder>().SaveGameFuncFlower();
					hit.collider.gameObject.GetComponent<Flowers10powder>().LoadGameFuncFlower();
				}
			} 
			
		}
    }
	
	void PrepareSparkle(){
		GameObject sparkle;
		for (int i = 0; i < sparkle_num; i++) {
			sparkle = Instantiate(animatedSparklePrefab);
			sparkle.transform.parent = transform;
			sparkle.SetActive(false);
			sparkleQueue.Enqueue(sparkle);
		}
	}
	
	public void Animate (Vector3 collectedSparklePosition, int amount){
		for (int i = 0; i < amount; i++) {
			//check if there's coins in the pool
			if (sparkleQueue.Count > 0) {
				//extract a coin from the pool
				GameObject sparkle = sparkleQueue.Dequeue ();
				sparkle.SetActive(true);

				//move coin to the collected coin pos
				sparkle.transform.position = collectedSparklePosition + new Vector3 (Random.Range (-spread, spread), Random.Range (3+(-spread), 3+spread), 0f);

				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				sparkle.transform.DOMove(targetPosition, duration)
				.SetEase(easeType)
				.OnComplete (() => {
					//executes whenever coin reach target position
					sparkle.SetActive(false);
					sparkleQueue.Enqueue(sparkle);
					bucket.transform.DOScale(scaleTo, 0.3f).SetEase(Ease.InOutBounce).OnComplete(() => { 
						bucket.transform.DOScale(new Vector3 (0.8f,0.8f,0.8f), 0.1f).SetEase(Ease.InOutBounce);
					});
				});
			}
		}
	}
}
