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
    public bool left, down;

	// Use this for initialization
	void Start () {
        tileSystem = GameObject.FindGameObjectWithTag("TileSystem").GetComponent<TileSystem>();
        gameCon = gameObject.GetComponent<SB_GameController>();
        shortestDistance = 50;
        enemiesOnAlert();
        StartCoroutine(getClosestEnemy());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator getClosestEnemy()
    {
        yield return new WaitForSeconds(1f);
        calcClosestEnemy();
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
            gameCon.enemies[j].GetComponent<SB_EnemyController>().isAlarmed = false;
        }
    }

    //Turns on the alarm lights and misc graphics associated with alarm mode
    void alarmGraphicsOn()
    {

    }

    void activeAlarmMode()
    {

    }

    void calcClosestEnemy()
    {
        for (int i = 0; i < gameCon.enemies.Length - 1; i++)
        {
            if (left)
            {
                if(gameCon.enemies[i].GetComponent<SB_EnemyAlarmed>().currentLoc.column <= alarmSource.column)
                {
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

    void setEnemyRotations()
    {

    }
}
