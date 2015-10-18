using UnityEngine;
using System.Collections;

public class musicManager : MonoBehaviour {

	public AudioClip[] songs;
	public AudioSource audio;
	private int songNumber;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		songNumber = Random.Range(0,songs.Length);
		audio.clip = songs[songNumber];
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (audio.isPlaying == false) {
			int nextSong = Random.Range(0,songs.Length);
			if (nextSong == songNumber) {
				nextSong = Random.Range(0,songs.Length);
			}
			audio.clip = songs[songNumber];
			audio.Play();
		}
	}
}
