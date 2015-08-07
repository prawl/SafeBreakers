using UnityEngine;
using System.Collections;

public class TestHighlight : HighlighterController
{
	// 
	new void Update()
	{
		base.Update ();		
		h.ConstantOnImmediate(Color.green);
	}
}
