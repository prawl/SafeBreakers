/*
Script Name: SB_EnemyAlarmed.cs
Author: Bradley M. Butts
Last Modified: 11-03-2015
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
    public int alarmedLength;
    public Vector3 alarmStart, alarmEnd;
    public Vector3[] pathToAlarm;
    public bool isClosest, alarmTo, alarmFrom, finishedTurn, checkingUp, checkingDown, checkingRight, checkingLeft;
    
    void Start()
    {
        enemyCon = GetComponent<SB_EnemyController>();
        enemyAnimator = GetComponent<SB_EnemyAnimator>();
        alarmStart = transform.position;
        finishedTurn = false;
        if(enemyCon.gameCon.alarmMode)
        {
            enemyCon.seeker.StartPath(alarmStart, alarmEnd, calcPathToAlarm);
        }
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
                if(alarmedLength != 0)
                {
                    rotateAndSearch();
                }
                else
                {
                    enemyCon.isAlarmed = false;
                }
            }
        }
        if(enemyCon.gameCon.playerCount == enemyCon.gameCon.enemyCount && finishedTurn)
        {
            finishedTurn = false;
            checkingUp = false; checkingDown = false; checkingRight = false; checkingLeft = false;
            alarmedLength--;
        }
    }

    void rotateAndSearch()
    {
        if (!finishedTurn)
        {
            if (enemyCon.faceUp)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = true;
                checkingLeft = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (enemyCon.faceDown)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = false; enemyCon.faceRight = true; enemyCon.faceLeft = false;
                checkingRight = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (enemyCon.faceRight)
            {
                enemyCon.faceUp = true; enemyCon.faceDown = false; enemyCon.faceRight = false; enemyCon.faceLeft = false;
                checkingUp = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
            else if (enemyCon.faceLeft)
            {
                enemyCon.faceUp = false; enemyCon.faceDown = true; enemyCon.faceRight = false; enemyCon.faceLeft = false;
                checkingDown = true;
                enemyCon.gameCon.enemyDone++;
                finishedTurn = true;
            }
        }
    }
    

    public void calcPathToAlarm(Path p)
    {
        if (!p.error)
        {
            alarmPath = p;
            pathToAlarm = alarmPath.vectorPath.ToArray();
            enemyCon.ChangeYValue(pathToAlarm);
        }
    }
}
