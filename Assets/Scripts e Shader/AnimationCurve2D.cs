using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurve2D : MonoBehaviour
{
    public Transform targetA;
	public Transform targetB;
	
	public AnimationCurve lerpCurve;
	
	public Vector3 lerpOffset;
	
	public float lerpTime = 3f;
	
	public float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
		
		if(_timer > lerpTime){
			_timer = lerpTime;
		}
		
		float lerpRatio = _timer/lerpTime;
		
		Vector3 positionOffset = lerpCurve.Evaluate(lerpRatio)*lerpOffset;
		
		transform.position = Vector3.Lerp(targetA.position, targetB.position, lerpRatio);
    }
}
