using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour {

	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = this.GetComponent<LineRenderer>();
		//lineRenderer.useWorldSpace = true;
		lineRenderer.positionCount = 20;

		lineRenderer.SetPositions( 
			curve (20,
				new Vector3(0f,0f,0f),
				new Vector3(1f,4f,1f),
				new Vector3(3f,0f,3f),
				new Vector3(4f,-5f,0f)

			));
		Debug.Log ("Done");
	}

	//Fast forward difference 
	//http://www.drdobbs.com/forward-difference-calculation-of-bezier/184403417?pgno=5
	Vector3[] curve(int numPoints, Vector3 P0,Vector3 P1,Vector3 P2,Vector3 P3 ){
		Vector3[] result = new Vector3[numPoints];
		float ax, ay,az, bx, by,bz, cx, cy, cz, dx, dy, dz;
		int numSteps;
		float h;


		Vector3 firstFD;
		Vector3 secondFD;
		Vector3 thirdFD;



		Vector3 a = -P0 + 3 * P1 - 3 * P2 + P3;
		Vector3 b = 3 * P0 + -6 * P1 + 3 * P2;
		Vector3 c = -3 * P0 + 3 * P1;

		Vector3 d = P0;


		numSteps = numPoints - 1;        //    arbitrary choice
		h = 1.0f / (float) numSteps;

		Vector3 point = d;

		firstFD = a * Mathf.Pow(h,3) + b * Mathf.Pow(h,2f) + c * h;
		secondFD =  6 * a* Mathf.Pow(h,3) + 2 * b * Mathf.Pow(h,2);
		thirdFD =  6 * a * Mathf.Pow(h ,3);

		result[0] = point;
		 
		for(int i =0 ; i < numSteps; i++){
			point += firstFD;
			firstFD+=secondFD;
			secondFD += thirdFD;

			result[i + 1] = point;

		}
//		for(float i = 0f; i < 1; i+=0.05f){
//			result[count] = Mathf.Pow(1f-i,2f)*P0 + 2f*(1f-i)*i*P1 + Mathf.Pow(i,2f)*P2;
//			count++;
//
//
//		}
		return result;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				// Construct a ray from the current touch coordinates
				var ray = Camera.main.ScreenPointToRay (touch.position);
				if (Physics.Raycast (ray)) {
					// Create a particle if hit
					var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
					Instantiate (go, transform.position, transform.rotation);
				}
			}
		}
	}
}
