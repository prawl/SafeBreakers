/*
Script Name: SB_AlarmMode.cs
Author: Bradley M. Butts
Last Modified: 10-20-2015
Description: Game script that controls all enemies and GUI aspects that occur during alarm mode. This script is attached
             to the same game object that the game controller is attached to. Script will automatically find the closest enemy 
             to the source of the alarm, activate all GUI, and calculate the length of the alarm.
*/
using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_AlarmMode : MonoBehaviour {

    private SB_GameController gameCon;
    public TileIndex alarmSource;
    private TileSystem tileSystem;
    public GameObject closestEnemy;
    public int shortestDistance, alarmModeLength;
    public bool left, down, alarmModeReady;

	// Use this for initialization
	void Start () {
        tileSystem = GameObject.FindGameObjectWithTag("TileSystem").GetComponent<TileSystem>();
        gameCon = gameObject.GetComponent<SB_GameController>();
        alarmModeReady = false;
        shortestDistance = 50;
        enemiesOnAlert();
        StartCoroutine(getClosestEnemy());
    }
	
	// Update is called once per frame
	void Update () {
        if(alarmModeLength == 0)
        {
            gameCon.alarmMode = false;
            enemiesToDefault();
            
        }
    }

    //Sets all enemies to be on alert
    void enemiesOnAlert()
    {
        for (int i = 0; i < gameCon.enemies.Length; i++)
        {
            gameCon.enemies[i].GetComponent<SB_EnemyController>().isAlarmed = true;
            gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().enabled = true;
        }
    }

    void enemiesToDefault()
    {
        for (int j = 0; j < gameCon.enemies.Length; j++)
        {
            if (gameCon.enemies[j].GetComponent<SB_EnemyController>().isAlarmed)
            {
                gameCon.enemies[j].GetComponent<SB_EnemyController>().isAlarmed = false;
                gameCon.enemies[j].GetComponent<SB_EnemyController>().lineOfSight--;
            }
        }
    }

    //Turns on the alarm lights and misc graphics associated with alarm mode
    void alarmGraphicsOn()
    {

    }

    void activeAlarmMode()
    {

    }

    IEnumerator getClosestEnemy()
    {
        yield return new WaitForSeconds(1f);
        calcClosestEnemy();
    }

    //Function goes through all enemies in the level and finds the closest enemy to the source of the alarm,
    //while only looking for enemies on the visible side of the alarm source to prevent looking for enemies
    //on the other side of the wall
    void calcClosestEnemy()
    {
        print("1");
        for (int i = 0; i < gameCon.enemies.Length - 1; i++)
        {
            print("2");
            if (left)
            {
                print("3");
                if(gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().currentLoc.column <= alarmSource.column)
                {
                    print("4: " + gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathArray.Length);
                    if (gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathArray.Length < shortestDistance)
                    {
                        closestEnemy = gameCon.enemies[i];
                        shortestDistance = gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathArray.Length;
                    }
                }
            }
            if (down)
            {
                if (gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().currentLoc.row >= alarmSource.row)
                {
                    if (gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathArray.Length < shortestDistance)
                    {
                        closestEnemy = gameCon.enemies[i];
                        shortestDistance = gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathArray.Length;
                    }
                }
            }
        }
        alarmModeLength = shortestDistance + 4;
        closestEnemy.GetComponent<SB_EnemyAlarmed>().isClosest = true;
    }
}
