using UnityEngine;
using System.Collections;

public class raycast : MonoBehaviour {


	//private sceneDirector directorScript;
	public Transform spotlight;
	//public Transform particles;
	private float newHitTime;
	private Transform lastHit;
	private slerpMove objMoverScript;
	private Vector3 relativePos;
	private Quaternion rotationToTarget;
	//private Quaternion originalRotation;
	//private bool shouldMove;
	private RaycastHit hit;
	private bool signHasBeenSeen = false;
	private Light overheadLight;
	private Light spotlightLight;
	private float lightSwitchStartTime;
	private bool lightsAreSwitched;
	private float lightSwitchDuration = 0.5f;
	public bool canTargetObjects = false;

	 

	// Use this for initialization
	void Start () {
		//directorScript = GameObject.Find("Scene Director").GetComponent<sceneDirector>();
		//originalRotation = spotlight.rotation;
		overheadLight = GameObject.Find("Light - overhead").GetComponent<Light>();
		spotlightLight = GameObject.Find("Spotlight").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		//RaycastHit hit;

		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Ray rayOrigin = new Ray (transform.position, transform.forward);
		
		if (Physics.Raycast(transform.position, fwd, out hit, 400)) 
		{
			Debug.DrawRay(rayOrigin.direction, hit.point, Color.green, 1);
			
			if (hit.transform != lastHit){
				newHitTime = Time.time;
				lastHit = hit.transform;
			} 

			if (hit.transform.name == "signage" && signHasBeenSeen == false) {
				signHasBeenSeen = true;
				canTargetObjects = true;
				lightSwitchStartTime = Time.time;
				//overheadLight.intensity = 1;
				//spotlightLight.spotAngle = 15;
			}

			if (hit.transform.tag == "collectibleItem"  && canTargetObjects == true) {
				//shouldMove = true;
				if (Time.time - newHitTime > 2) {
					objMoverScript = hit.transform.GetComponent<slerpMove>();
					objMoverScript.MoveNow();
				} else if (Time.time - newHitTime > 1){
					
				} else if (Time.time - newHitTime > 0.5){
					//spotlight.LookAt(hit.transform);
					//MoveSpotlight();
					
				}
				relativePos = hit.transform.position - spotlight.position;
				rotationToTarget = Quaternion.LookRotation(relativePos);
				MoveSpotlight();


			} 

			if (signHasBeenSeen && !lightsAreSwitched) {
				SwitchLights();
			}
		}
	}

	public void MoveSpotlight(){
		relativePos = hit.transform.position - spotlight.position;
		rotationToTarget = Quaternion.LookRotation(relativePos);
		spotlight.rotation = Quaternion.RotateTowards(spotlight.rotation, rotationToTarget, 5);
		//if ()
	}


	public void SwitchLights(){
		float t = (Time.time - lightSwitchStartTime) / lightSwitchDuration;
		overheadLight.intensity = Mathf.SmoothStep(0.2f, 1.0f, t);
		spotlightLight.spotAngle = Mathf.SmoothStep(30.0f, 15.0f, t);
		if (Time.time > lightSwitchStartTime + lightSwitchDuration) {
			lightsAreSwitched = true;
		}
	}
}
