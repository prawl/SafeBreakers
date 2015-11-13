/*
Script Name: SB_EnemyAlarmed.cs
Author: Bradley M. Butts
Last Modified: 11-04-2015
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
    private SB_AlarmMode alarmMode;
    public Path alarmPath;
    private int alarmPos;
    public int alarmedLength;
    public Vector3 alarmStart, alarmEnd;
    public Vector3[] pathToAlarm;
    public bool isClosest, alarmTo, alarmFrom, finishedTurn, checkingUp, checkingDown, checkingRight, checkingLeft, updatedPos, enemyReady;
    
    void Start()
    {
        enemyReady = false;
        alarmTo = true; alarmFrom = false; updatedPos = false;
        enemyCon = GetComponent<SB_EnemyController>();
        enemyAnimator = GetComponent<SB_EnemyAnimator>();
        alarmMode = enemyCon.gameCon.player.GetComponent<SB_AlarmMode>();
        alarmStart = transform.position;
        finishedTurn = false;
        if(enemyCon.gameCon.alarmMode)
        {
            enemyCon.seeker.StartPath(alarmStart, alarmEnd, calcPathToAlarm);
        }
        StartCoroutine(WaitForClosestEnemy());
    }

    IEnumerator WaitForClosestEnemy()
    {
        yield return new WaitForSeconds(1.5f);
    }

    void Update()
    {
        if (!alarmMode.enabled || alarmMode.ready)
        {
            if (enemyCon.isAlarmed && !enemyCon.gameCon.isLevelPaused && (enemyCon.gameCon.playerCount > enemyCon.gameCon.enemyCount))
            {
                if (isClosest)
                {
                    investigateSource();
                }
                else
                {
                    if (enemyCon.gameCon.player.GetComponent<SB_AlarmMode>().alarmModeLength > 0)
                    {
                        rotateAndSearch();
                    }
                    else
                    {
                        enemyCon.isAlarmed = false;
                    }
                }
            }
            if (enemyCon.gameCon.playerCount == enemyCon.gameCon.enemyCount && finishedTurn)
            {
                finishedTurn = false;
                checkingUp = false; checkingDown = false; checkingRight = false; checkingLeft = false;
            }
            if (!isClosest && !enemyCon.gameCon.alarmMode)
            {
                enemyCon.isAlarmed = false;
            }
        }
    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.000001;
    }

    void investigateSource()
    {
        enemyCon.moved = false;
        if (!finishedTurn)
        {
            if (alarmTo)
            {
                if (!updatedPos)
                {
                    alarmPos++;
                    updatedPos = true;
                }
                if (!V3Equal(pathToAlarm[alarmPos], transform.position) && (alarmPos < pathToAlarm.Length - 1))
                {
                    Vector3 dir = (pathToAlarm[alarmPos] - transform.position).normalized;
                    dir *= Time.fixedDeltaTime * enemyCon.speed;
                    enemyCon.controller.Move(dir);
                }
                if(V3Equal(pathToAlarm[alarmPos], transform.position))
                {
                    updatedPos = false;
                    enemyCon.moved = true;
                    finishedTurn = true;
                }
                if(alarmPos == pathToAlarm.Length - 1)
                {
                    alarmTo = false;
                    alarmPos--;
                }
            }
            if(!alarmTo && !alarmFrom)
            {
                if (enemyCon.gameCon.alarmMode)
                {
                    rotateAndSearch();
                }
                else
                {
                    alarmFrom = true;
                }
            }
            if (alarmFrom)
            {
                if (!updatedPos)
                {
                    alarmPos--;
                    updatedPos = true;
                }
                if (!V3Equal(pathToAlarm[alarmPos], transform.position) && (alarmPos > 0))
                {
                    Vector3 dir = (pathToAlarm[alarmPos] - transform.position).normalized;
                    dir *= Time.fixedDeltaTime * enemyCon.speed;
                    enemyCon.controller.Move(dir);
                }
                if (V3Equal(pathToAlarm[alarmPos], transform.position))
                {
                    updatedPos = false;
                    enemyCon.moved = true;
                    finishedTurn = true;
                }
                if (alarmPos == 0)
                {
                    enemyCon.isAlarmed = false;
                    GetComponent<SB_EnemyAlarmed>().enabled = false;
                }
            }
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
            enemyReady = true;
        }
    }
}
