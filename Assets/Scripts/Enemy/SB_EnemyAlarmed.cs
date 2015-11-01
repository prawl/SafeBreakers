/*
Script Name: SB_EnemyAlarmed.cs
Author: Bradley M. Butts
Last Modified: 10-30-2015
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

    private SB_EnemyController enemyCon;
    private SB_EnemyAnimator enemyAnimator;
    public Path alarmPath;
    private int alarmPos;
    public Vector3 alarmStart, alarmEnd;
    public Vector3[] pathToAlarm;
    public bool isClosest, checkedUp, checkedDown, checkedRight, checkedLeft, alarmTo, alarmFrom, finishedTurn, checkingUp, checkingDown, checkingRight, checkingLeft;
    
    void Start()
    {
        enemyCon = GetComponent<SB_EnemyController>();
        enemyAnimator = GetComponent<SB_EnemyAnimator>();
        alarmStart = transform.position;
        finishedTurn = false;  
    }

    void Update()
    {
        if (enemyCon.isAlarmed && !enemyCon.gameCon.isLevelPaused && (enemyCon.gameCon.playerCount > enemyCon.gameCon.enemyCount))
        {
            if (isClosest)
            {

            }
            else
            {
                rotateAndSearch();
            }
        }
        if(enemyCon.gameCon.playerCount == enemyCon.gameCon.enemyCount)
        {
            finishedTurn = false;
            checkingUp = false; checkingDown = false; checkingRight = false; checkingLeft = false;
        }
    }

    void rotateAndSearch()
    {
        if (!finishedTurn)
        {
            if (!checkedUp)
            {
                enemyCon.faceUp = true; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = false;
                checkingUp = true;
                enemyCon.LookForPlayer();
                checkedUp = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (!checkedLeft)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = true;
                checkingLeft = true;
                enemyCon.LookForPlayer();
                checkedLeft = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (!checkedDown)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = true; enemyCon.faceRight = false; enemyCon.faceLeft = false;
                checkingDown = true;
                enemyCon.LookForPlayer();
                checkedDown = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (!checkedRight)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = true; enemyCon.faceLeft = false;
                checkingRight = true;
                enemyCon.LookForPlayer();
                checkedRight = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else
            {
                enemyCon.isAlarmed = false;
                checkedUp = false; checkedDown = false; checkedRight = false; checkedLeft = false;
            }
        }
    }
    

	
}
