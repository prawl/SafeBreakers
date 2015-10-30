/*
Script Name: SB_EnemyAlarmed.cs
Author: Bradley M. Butts
Last Modified: 10-22-2015
Description: This script is attached to each enemy and handles what they do in the case of alarm mode. 
             If this enemy is selected as the closest enemy, then they go to the source of the alarm.
             If not, then they stand their ground and turn each turn to search around then; the catch to
             this is that their line of sight is increased by 1 while they're searching. 
*/
using UnityEngine;
using System.Collections;
using Pathfinding;
using Rotorz.Tile;
using Rotorz.Tile.Internal;


public class SB_EnemyAlarmed : MonoBehaviour {

    private TileSystem tileSystem;
    public SB_EnemyController enemyCon;
    private GameObject player;
    public TileIndex startLoc, currentLoc, nextLoc, endLoc;
    public Path path;
    public Vector3[] pathArray;
    public bool isClosest, goClockwise, goCounterClockwise, to, from, investigate;
    private int progress;

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
        if (!isClosest)
        {
            enemyCon.lineOfSight = enemyCon.lineOfSight + 1;
        }
        GetMovementDirection(Random.Range(0, 1));
	}

    //This function checks if the random number is a 0 or 1. If it is a 0, then the enemy 
    //will turn counter-clockwise. Else it'll turn clockwise.
    void GetMovementDirection(float moveDir)
    {
        switch (Mathf.FloorToInt(moveDir))
        {
            case 1:
                goClockwise = true;
                break;
            default:
                goCounterClockwise = true;
                break;
        }
    }

	// Update is called once per frame
	void Update () {
        if (player.GetComponent<SB_GameController>().alarmMode)
        {
            if (isClosest)
            {

            }
            else
            {
                if (player.GetComponent<SB_GameController>().enemyCount < player.GetComponent<SB_GameController>().playerCount)
                {
                    rotateAndSearch();
                }
            }
        }
        else
        {
            if (isClosest)
            {
                returnToStart();
            }
        }
    }

    void alarmModeActions()
    {
        
    }

    void returnToStart()
    {
        if (currentLoc != startLoc)
        {

        }
        else
        {
            player.GetComponent<SB_AlarmMode>().enabled = false;
            for(int i = 0; i < player.GetComponent<SB_GameController>().enemies.Length; i++)
            {
                player.GetComponent<SB_GameController>().enemies[i].GetComponent<SB_EnemyAlarmed>().enabled = false;
            }
        }
    }

    //For enemies that are not the closest enemy to the source to the alarm, the follow functions
    //rotates them either counter-clockwise or clockwise so they can search around them for the player.
    void rotateAndSearch()
    {
        if (goCounterClockwise)
        {
            if (enemyCon.up)
            {
                turnLeft();
            }
            else if (enemyCon.down)
            {
                turnRight();
            }
            else if (enemyCon.right)
            {
                turnUp();
            }
            else if (enemyCon.left)
            {
                turnDown();
            }
        }
        else
        {
            if (enemyCon.up)
            {
                turnRight();
            }
            else if (enemyCon.down)
            {
                turnLeft();
            }
            else if (enemyCon.right)
            {
                turnDown();
            }
            else if (enemyCon.left)
            {
                turnUp();
            }
        }
        
    }

    //Function used for the closest enemy to the source of the alarm to move to the source.
    void goToAlarm(Vector3[] path)
    {
        if (!enemyCon.moved)
        {

        }    
    }

    //Calculates the enemy's path to the source of the alarm from the enemy's current location
    public void calcPathToAlarm(TileIndex currentPos, TileIndex alarmPos)
    {
        Vector3 startPos = player.GetComponent<SB_PlayerController>().tileSystem.GetTile(currentPos).gameObject.transform.position;
        Vector3 endPos = player.GetComponent<SB_PlayerController>().tileSystem.GetTile(alarmPos).gameObject.transform.position;
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(startPos, endPos, OnPathComplete);
    }

    //Generates the closest path to the target and then places the Vector3 steps into an array
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            pathArray = path.vectorPath.ToArray();
            enemyCon.ChangeYValue(pathArray);
        }
    }

    //The following functions turn the enemy in the direction specified. These are used exclusively for the 
    //the enemies that are not the closest enemy to the source of the alarm.
    void turnUp()
    {
        enemyCon.moved = false;
        enemyCon.up = true; enemyCon.down = false; enemyCon.right = false; enemyCon.left = false;
        enemyCon.faceUp = true; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = false;
        enemyCon.moved = true;
        player.GetComponent<SB_GameController>().enemyDone++;
    }

    void turnDown()
    {
        enemyCon.moved = false;
        enemyCon.up = false; enemyCon.down = true; enemyCon.right = false; enemyCon.left = false;
        enemyCon.faceUp = false; enemyCon.faceDown = true; enemyCon.faceRight = false; enemyCon.faceLeft = false;
        enemyCon.moved = true;
        player.GetComponent<SB_GameController>().enemyDone++;
    }

    void turnRight()
    {
        enemyCon.moved = false;
        enemyCon.up = false; enemyCon.down = false; enemyCon.right = true; enemyCon.left = false;
        enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = true; enemyCon.faceLeft = false;
        enemyCon.moved = true;
        player.GetComponent<SB_GameController>().enemyDone++;
    }

    void turnLeft()
    {
        enemyCon.moved = false;
        enemyCon.up = false; enemyCon.down = false; enemyCon.right = false; enemyCon.left = true;
        enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = true;
        enemyCon.moved = true;
        player.GetComponent<SB_GameController>().enemyDone++;
    }
}
