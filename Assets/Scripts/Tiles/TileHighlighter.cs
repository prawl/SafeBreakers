using UnityEngine;
using System.Collections;

public class TileHighlighter : HighlighterController {

	public bool enabled;

	// Use this for initialization
	void Start () {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if(enabled){
			h.ConstantOnImmediate(Color.green);
		}
		else{
			h.Off ();
		}

	}
}
