using UnityEngine;
using System.Collections;

public class slerpMove : MonoBehaviour {

	//public Transform target;
	public float journeyTime = 3.0f;
	private Vector3 start;
	private Vector3 end;
	public float startTime;
	public bool shouldMove = false;
	private sceneDirector directorScript;
	public bool debugRotation = false;

	public bool mustRotate = false;
	public Quaternion fixedRotation ;

	// Use this for initialization
	void Start () {
		start = transform.position;
		//end = target.transform.position;
		end = GameObject.Find("destination(box)").transform.position;
		directorScript = GameObject.Find("Scene Director").GetComponent<sceneDirector>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldMove) {
			Vector3 center = (start + end) * 0.5F;
			center -= new Vector3(0, 0.05f, 0);
			Vector3 riseRelCenter = start - center;
			Vector3 setRelCenter = end - center;
			float fracComplete = (Time.time - startTime) / journeyTime;
			transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
			transform.position += center;
			if (transform.position == end) {
				FinishedMoving();
			}
		}

		if (debugRotation) {
			Debug.Log("debugRot = " + transform.rotation);
		}
	}

	public void MoveNow(){
		// This function is called by the raycast script on player object.
		if (!shouldMove){
			startTime = Time.time;
			GetComponent<Collider>().enabled = false;
			shouldMove = true;
		}
	}

	public void FinishedMoving(){
		shouldMove = false;
		//GetComponent<Collider>().enabled = false;
		directorScript.CheckItem(gameObject);
		if (mustRotate) {
			transform.rotation = fixedRotation;
		}
	}
}
