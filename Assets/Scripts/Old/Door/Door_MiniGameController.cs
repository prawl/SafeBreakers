using UnityEngine;
using System.Collections;

public class Door_MiniGameController : Door_ControlPanel {

	public int[] keyPadNums;
	public int randomNum;
	public bool alarmTriggered;
	public Door_ButtonController[] buttons;
	public int arrayProgression;
	public int currentNum;

	// Use this for initialization
	void Start () {
		alarmTriggered = false;
		keyPadNums = new int[4];
		arrayProgression = 0;
		GetKeypadNum ();
		buttons = new Door_ButtonController[9];
		AssignButtonNum ();
	}
	
	// Update is called once per frame
	void Update () {
		if (arrayProgression != keyPadNums.Length) {
			ChangeColors ();
		}
		if (arrayProgression == keyPadNums.Length) {
			print ("Door Unlocked");
		}
	}

	void ChangeColors(){
		currentNum = keyPadNums [arrayProgression];
		for(int i = 0; i < buttons.Length; i++){
			if(buttons[i].buttonNum == currentNum){
				buttons[i].valid = true;
			}
			else{
				buttons[i].valid = false;
			}
		}
	}

	void AssignButtonNum(){
		for(int i = 0; i < buttons.Length; i++){
			Door_ButtonController temp = new Door_ButtonController();
			temp.SetButtonNum(i+1);
			buttons[i] = temp;
		}
	}

	void GetKeypadNum(){
		for(int i = 0; i < keyPadNums.Length; i++){
			randomNum = Random.Range (1,9);
			keyPadNums[i] = randomNum;
			for (int j = 0; j < i; j++){
				if(keyPadNums[j] == randomNum){
					j = i;
					i--;
				}			
			}
		}
	}
}
