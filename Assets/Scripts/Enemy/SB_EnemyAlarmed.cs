using UnityEngine;
using System.Collections;
using Pathfinding;
using Rotorz.Tile;
using Rotorz.Tile.Internal;


public class SB_EnemyAlarmed : MonoBehaviour {

    private TileSystem tileSystem;
    public SB_EnemyController enemyCon;
    private GameObject player;
    public TileIndex startLoc, currentLoc, endLoc;
    public Path path;
    public Vector3[] pathArray;
    public bool isClosest, goClockwise, goCounterClockwise;

	// Use this for initialization
	void Start () {
        tileSystem = GameObject.FindGameObjectWithTag("TileSystem").GetComponent<TileSystem>();
        startLoc = tileSystem.ClosestTileIndexFromWorld(transform.position);
        currentLoc = startLoc;
        player = GameObject.FindGameObjectWithTag("Player"); 
        isClosest = false;
        enemyCon = gameObject.GetComponent<SB_EnemyController>();
        endLoc = player.GetComponent<SB_AlarmMode>().alarmSource;
        calcPathToAlarm(currentLoc, endLoc);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void returnToStart()
    {

    }

    void rotateAndSearch()
    {

    }

    public void calcPathToAlarm(TileIndex currentPos, TileIndex alarmPos)
    {
        Vector3 startPos = player.GetComponent<SB_PlayerController>().tileSystem.GetTile(currentPos).gameObject.transform.position;
        Vector3 endPos = player.GetComponent<SB_PlayerController>().tileSystem.GetTile(alarmPos).gameObject.transform.position;
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(startPos, endPos, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            pathArray = path.vectorPath.ToArray();
            enemyCon.ChangeYValue(pathArray);
        }
    }
}
