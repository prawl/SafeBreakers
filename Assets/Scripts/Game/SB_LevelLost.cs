using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SB_LevelLost : MonoBehaviour {
	
	private SB_GameController gameCon;
	public Image lostWindow;
	public Text lostCount;
	
	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<SB_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.isLevelPaused && gameCon.isLevelLost) {
			lostWindow.gameObject.SetActive(true);
			lostCount.text = "You lasted " + gameCon.playerCount + " turns!";
		}
		else{
			lostWindow.gameObject.SetActive(false);
		}
	}
	
	public void ResetLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}
}
