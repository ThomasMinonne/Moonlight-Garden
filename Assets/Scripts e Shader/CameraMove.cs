using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraMove : MonoBehaviour
{
	[SerializeField] Transform[] target;
	
	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;
	[SerializeField] Ease easeType;
	[SerializeField] float duration;
	[SerializeField] FlowerManager fm;
	bool isRunning = false;
	bool canRotate = false;
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
					moveCamera(currentposition, duration, false); 
				}
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
			{
				if(currentposition > 0){
					currentposition--;
					moveCamera(currentposition, duration, false);
				}
			}
		}
		if(canRotate == true){
			Rotate();
		}		
    }
	
	public void moveCamera(int currentposition_temp, float duration_temp, bool anim){
		isRunning = true;
		transform.DOMove(target[currentposition_temp].position, duration_temp)
		.SetEase(easeType)
		.OnComplete(() => { 
			isRunning = false; 
			if(anim == true){
				Bloom b_temp;
				Volume lunaVolume_temp = GameObject.Find("Luna_volume").GetComponent<Volume>();
				lunaVolume_temp.profile.TryGet(out b_temp);
				GameObject luna = GameObject.FindGameObjectsWithTag("Moon")[0];
				Transform luna_child = luna.transform.GetChild(0);
				transform.DOMove(new Vector3 (0, transform.position.y + 1.25f, 0), duration)
				.OnComplete(() => {
					canRotate = true;
					luna.transform.DOScale(new Vector3(luna.transform.localScale.x + 0.2f,luna.transform.localScale.y + 0.2f, luna.transform.localScale.z + 0.2f), duration);
					﻿﻿﻿﻿﻿﻿﻿DOTween.To(()=> b_temp.intensity.value, x=> b_temp.intensity.value = x, 7, 5f)
					.OnComplete(() => {
						fm.Animate(luna.transform.position, 30);
						﻿﻿﻿﻿﻿﻿﻿DOTween.To(()=> b_temp.intensity.value, x=> b_temp.intensity.value = x, 1, 5f);
						luna.transform.DOScale(new Vector3(luna.transform.localScale.x - 0.2f,luna.transform.localScale.y - 0.2f, luna.transform.localScale.z - 0.2f), duration);
						transform.DOMove(new Vector3 (0, transform.position.y - 1.25f, 0), duration);
						canRotate = false;
					});
				});
			}
		});
	}
	
	public void animationGrow(){
		moveCamera(2, duration, true);
	}
	
	void Rotate(){
		GameObject luna = GameObject.FindGameObjectsWithTag("Moon")[0];
		Transform luna_child = luna.transform.GetChild(0);
		luna_child.transform.Rotate(new Vector3(0,0,0.6f));
	}
}
