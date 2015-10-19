using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sceneDirector : MonoBehaviour {

	public GameObject[] checkmarks;
	public int packedItemCount;
	public string name;
	public int itemsInBox = 0;
	private GameObject checklist;
	private GameObject blankPaper;
	private Transform checklistFinalPos;
	private Transform blankPaperFinalPos;
	private List<GameObject> objectsInBox = new List<GameObject>();
	private List<Transform> hoverPositions = new List<Transform>();
	private string scenePhase;
	private int boxItemNum = 0;
	private Vector3 moveRef = Vector3.zero;
	private int score;
	private GameObject scoreImage;
	public Material[] scores;
	private GameObject againButton;
	private raycast raycastScript;
//	private slerpMove slerpScript;

	// Use this for initialization
	void Start () {
		checklist = GameObject.Find("signage");
		blankPaper = GameObject.Find("summary page");
		checklistFinalPos = GameObject.Find("checklistFinalPosition").transform;
		blankPaperFinalPos = GameObject.Find("blankPaperFinalPosition").transform;
		hoverPositions.Add(GameObject.Find("holder/plane/a").transform);
		hoverPositions.Add(GameObject.Find("holder/plane/b").transform);
		hoverPositions.Add(GameObject.Find("holder/plane/c").transform);
		hoverPositions.Add(GameObject.Find("holder/plane/d").transform);
		hoverPositions.Add(GameObject.Find("holder/plane/e").transform);
		scoreImage = GameObject.Find("summary page/score");
		scoreImage.SetActive(false);
		againButton = GameObject.Find("summary page/again");
		againButton.SetActive(false);
		raycastScript = GameObject.FindWithTag("camera").GetComponent<raycast>();
	}
	
	// Update is called once per frame
	void Update () {
		if (scenePhase == "move checklist") {
			MoveChecklist();
		} else if (scenePhase == "move blank paper") {
			MoveBlankPaper();
		} else if (scenePhase == "move items") {
			MoveItemsInBox();
		} else if (scenePhase == "show score") {
			ShowScore();
		}
	}

	public void CheckItem(GameObject go){
		string name = go.name;
		if (name == "Keys") {
			checkmarks[0].SetActive(true);
			score++;
		} else if (name == "Glasses") {
			checkmarks[1].SetActive(true);
			score++;
		} else if (name == "Knife") {
			checkmarks[2].SetActive(true);
			score++;
		} else if (name == "Files") {
			checkmarks[3].SetActive(true);
			score++;
		} else if (name == "Flashlight") {
			checkmarks[4].SetActive(true);
			score++;
		}
		itemsInBox++;
		objectsInBox.Add(go);
		if (itemsInBox == 5) {
			scenePhase = "move checklist";
			raycastScript.canTargetObjects = false;
		}
	}

	public void MoveChecklist(){

		checklist.transform.position = Vector3.SmoothDamp(checklist.transform.position, checklistFinalPos.position, ref moveRef, 0.1f);
		checklist.transform.rotation = Quaternion.Slerp(checklist.transform.rotation, checklistFinalPos.rotation, 0.1f);
		if (checklist.transform.position == checklistFinalPos.position) {
			scenePhase = "move blank paper";
		}
	}

	public void MoveBlankPaper(){
		blankPaper.transform.position = Vector3.SmoothDamp(blankPaper.transform.position, blankPaperFinalPos.position, ref moveRef, 0.1f);
		blankPaper.transform.rotation = Quaternion.Slerp(blankPaper.transform.rotation, blankPaperFinalPos.rotation, 0.1f);
		if (blankPaper.transform.rotation == blankPaperFinalPos.rotation) {
			scenePhase = "move items";
		}
	}

	public void MoveItemsInBox(){
		objectsInBox[boxItemNum].transform.position = Vector3.SmoothDamp(objectsInBox[boxItemNum].transform.position, hoverPositions[boxItemNum].position, ref moveRef, 0.2f);
		//objectsInBox[boxItemNum].transform.LookAt(GameObject.FindWithTag("camera").transform);
		if (objectsInBox[boxItemNum].transform.position == hoverPositions[boxItemNum].position){
			//slerpScript = objectsInBox[boxItemNum].GetComponent<slerpMove>();
			boxItemNum++;
			Debug.Log("boxItemNum = " + boxItemNum);
			if (boxItemNum > 4){
				scenePhase = "show score";
			}
		}
	}

	public void ShowScore(){
		scoreImage.GetComponent<Renderer>().material = scores[score];
		scoreImage.SetActive(true);
		//yield return new WaitForSeconds(5);
		//againButton.SetActive(true);
		scenePhase = "show again button";
		StartCoroutine(ShowAgainButton());
	}

	IEnumerator ShowAgainButton(){
		yield return new WaitForSeconds(3);
		againButton.SetActive(true);
	}
}
