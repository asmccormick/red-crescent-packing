using UnityEngine;
using System.Collections;

public class jazzSign : MonoBehaviour {

	public GameObject glow;
	public AudioSource audio;
	public float changeTime;
	private float myVolume;
	private Renderer signRenderer;
	public Light signLight;

	// Use this for initialization
	void Start () {
		signRenderer = glow.GetComponent<Renderer>();
		audio = glow.GetComponent<AudioSource>();
		signLight = GameObject.Find("Jazz Sign/Jazz Glow/Light - Jazz club").GetComponent<Light>();
		myVolume = audio.volume;					// because the volume set in the inspector could be any number.
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > changeTime) {
			if (signRenderer.enabled == true){
				audio.volume = 0;
				signRenderer.enabled = false;
				signLight.enabled = false;;
			} else {
				audio.volume = myVolume;
				signRenderer.enabled = true;
				signLight.enabled = true;
			}
			changeTime = Time.time + Random.Range(0.5f,5.0f);
		}
	}
}
