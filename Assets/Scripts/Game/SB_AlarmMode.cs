/*
Script Name: SB_AlarmMode.cs
Author: Bradley M. Butts
Last Modified: 11-04-2015
Description: Game script that controls all enemies and GUI aspects that occur during alarm mode. This script is attached
             to the same game object that the game controller is attached to. Script will automatically find the closest enemy 
             to the source of the alarm, activate all GUI, and calculate the length of the alarm.
*/
using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_AlarmMode : MonoBehaviour {

    public TileIndex alarmSource;
    public int alarmModeLength;
    public bool left, down, ready;
    private SB_GameController gameCon;
    private GameObject closestEnemy;
    private int shortestPath = 100;


    void Start()
    {
        ready = false;
        gameCon = GetComponent<SB_GameController>();
        for(int i = 0; i < gameCon.enemies.Length - 1; i++)
        {
            gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().enabled = true;
            gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().alarmEnd = GetComponent<SB_PlayerController>().tileSystem.GetTile(alarmSource).gameObject.transform.position;
        }
        StartCoroutine(Wait());
        getClosestEnemy();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }

    void Update()
    {
        for(int i = 0; i < gameCon.enemies.Length - 1; i++)
        {
            if (!gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().enemyReady)
            {
                ready = false;
            }
            else
            {
                ready = true;
            }
        }
    }

    void getClosestEnemy()
    {
        for(int i = 0; i < gameCon.enemies.Length - 1; i++)
        {
            if (left)
            {
                if(gameCon.enemies[i].transform.position.x <= GetComponent<SB_PlayerController>().tileSystem.GetTile(alarmSource).gameObject.transform.position.x)
                {
                    if (gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathToAlarm.Length < shortestPath)
                    {
                        shortestPath = gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathToAlarm.Length;
                        closestEnemy = gameCon.enemies[i];
                    }
                }
            }
            if (down)
            {
                if (gameCon.enemies[i].transform.position.z <= GetComponent<SB_PlayerController>().tileSystem.GetTile(alarmSource).gameObject.transform.position.z)
                {
                    shortestPath = gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().pathToAlarm.Length;
                    closestEnemy = gameCon.enemies[i];
                }
            }
        }
        closestEnemy.GetComponent<SB_EnemyAlarmed>().isClosest = true;
        alarmModeLength = shortestPath + 4;
    }

   
}
