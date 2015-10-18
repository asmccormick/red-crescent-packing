using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour {

	
	void Start () 
	{
	
	}
	
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
