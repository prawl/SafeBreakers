using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SB_LevelPaused : MonoBehaviour {

	private SB_GameController gameCon;
	public Image pauseWindow;

	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<SB_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.isLevelPaused && !gameCon.isLevelLost) {
			pauseWindow.gameObject.SetActive(true);
		}
		else{
			pauseWindow.gameObject.SetActive(false);
		}
	}

	public void ResetLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}
}
