using UnityEngine;
using System.Collections;

public class focus_marker : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("average_man_01/Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
	}
}
