using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class Door_ControlPanel : MonoBehaviour {

	public TileSystem tileSystem;
	public TileData panelLocation;
	public TileIndex panelIndex;
	public GameObject tile;
	public TileCheck tileCheck;
	public int row;
	public int column;
	public bool activePanel;

	// Use this for initialization
	void Start () {
		activePanel = false;
		row = tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).row;
		column = tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).column;
	}

	void GetGameObject(){
		panelIndex = tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position);
		panelLocation = tileSystem.GetTile (panelIndex.row, panelIndex.column);
		tileCheck = panelLocation.gameObject.GetComponent<TileCheck> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetGameObject ();
		if (tileCheck.occupier == "Player") {
			activePanel = true;
		}
		else{
			activePanel = false;
		}
	}
}
