using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
	[SerializeField] Transform[] target;
	
	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;
	[SerializeField] Ease easeType;
	public int duration;
	bool isRunning = false;
	Vector3 targetPosition;
	int currentposition = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(!isRunning){
			if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
			{
				if(currentposition < 2){
					isRunning = true;
					currentposition++;
					transform.DOMove(target[currentposition].position, duration)
					.SetEase(easeType)
					.OnComplete(() => { 
						isRunning = false; 
					}); 
				}
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
			{
				if(currentposition > 0){
					isRunning = true;
					currentposition--;
					transform.DOMove(target[currentposition].position, duration)
					.SetEase(easeType)
					.OnComplete(() => { 
						isRunning = false; 
					}); 
				}
			}
		}	
    }	
}
