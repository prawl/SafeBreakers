using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_DoorMinigame : MonoBehaviour {

    public Image miniGameWindow;
    private GameObject player;
    public bool gameActive, isGameWon, isGameLost;
    private int numOne, numTwo, numThree, progress;
    public int timerLength;
    public Button button1, button2, button3, button4, button5, button6, button7, button8, button9, progressLight1, progressLight2, progressLight3, timerLight1, timerLight2, timerLight3, timerLight4, timerLight5;
    private float timer;
    public TileIndex gameTile;

	// Use this for initialization
	void Start () {
        timer = timerLength;
        progress = 0;
        gameActive = false;
        getPassword();
        player = GameObject.FindGameObjectWithTag("Player");
        isGameLost = false;
        isGameWon = false;
        miniGameWindow.color = Color.gray;
	}
	
	// Update is called once per frame
	void Update () {
        if(gameTile == player.GetComponent<SB_PlayerController>().currentTile && (!isGameLost && !isGameWon))
        {
            player.GetComponent<SB_GameController>().canInteract = true;
        }
        else
        {
            player.GetComponent<SB_GameController>().canInteract = false;
        }
        if (player.GetComponent<SB_GameController>().minigameOn)
        {
            gameActive = true;
        }
        if (gameActive)
        {
            miniGameWindow.gameObject.SetActive(true);
            if(!isGameLost && !isGameWon)
            {
                timer = timer - Time.deltaTime;
                lightTimer();
                doorMinigame();
                highlightProgress();
                highlightButtons();
            }
        }
        if (!gameActive)
        {
            miniGameWindow.gameObject.SetActive(false);
        }
        if (isGameWon)
        {
            StartCoroutine(gameWon());
        }
        if (isGameLost)
        {
            StartCoroutine(gameLost());
        }
	}

    void highlightButtons()
    {
        if(progress == 0)
        {
            if(numOne == 1)
            {
                button1.image.color = Color.blue;
            }
            else if (numOne == 2)
            {
                button2.image.color = Color.blue;
            }
            else if (numOne == 3)
            {
                button3.image.color = Color.blue;
            }
            else if (numOne == 4)
            {
                button4.image.color = Color.blue;
            }
            else if (numOne == 5)
            {
                button5.image.color = Color.blue;
            }
            else if (numOne == 6)
            {
                button6.image.color = Color.blue;
            }
            else if (numOne == 7)
            {
                button7.image.color = Color.blue;
            }
            else if (numOne == 8)
            {
                button8.image.color = Color.blue;
            }
            else if (numOne == 9)
            {
                button9.image.color = Color.blue;
            }
        }

        if(progress == 1)
        {
            if (numTwo == 1)
            {
                button1.image.color = Color.blue;
            }
            else if (numTwo == 2)
            {
                button2.image.color = Color.blue;
            }
            else if (numTwo == 3)
            {
                button3.image.color = Color.blue;
            }
            else if (numTwo == 4)
            {
                button4.image.color = Color.blue;
            }
            else if (numTwo == 5)
            {
                button5.image.color = Color.blue;
            }
            else if (numTwo == 6)
            {
                button6.image.color = Color.blue;
            }
            else if (numTwo == 7)
            {
                button7.image.color = Color.blue;
            }
            else if (numTwo == 8)
            {
                button8.image.color = Color.blue;
            }
            else if (numTwo == 9)
            {
                button9.image.color = Color.blue;
            }
        }

        if(progress == 2)
        {
            if (numThree == 1)
            {
                button1.image.color = Color.blue;
            }
            else if (numThree == 2)
            {
                button2.image.color = Color.blue;
            }
            else if (numThree == 3)
            {
                button3.image.color = Color.blue;
            }
            else if (numThree == 4)
            {
                button4.image.color = Color.blue;
            }
            else if (numThree == 5)
            {
                button5.image.color = Color.blue;
            }
            else if (numThree == 6)
            {
                button6.image.color = Color.blue;
            }
            else if (numThree == 7)
            {
                button7.image.color = Color.blue;
            }
            else if (numThree == 8)
            {
                button8.image.color = Color.blue;
            }
            else if (numThree == 9)
            {
                button9.image.color = Color.blue;
            }
        }
    }

    void lightTimer()
    {
        if(timer >= (timerLength * .8f))
        {
            timerLight1.image.color = Color.green;
        }
        if (timer < (timerLength * .8f))
        {
            timerLight1.image.color = Color.red;
        }
        if(timer >= (timerLength * .6f))
        {
            timerLight2.image.color = Color.green;
        }
        if (timer < (timerLength * .6f))
        {
            timerLight2.image.color = Color.red;
        }
        if(timer >= (timerLength * .4f))
        {
            timerLight3.image.color = Color.green;
        }
        if (timer < (timerLength * .4f))
        {
            timerLight3.image.color = Color.red;
        }
        if (timer >= (timerLength * .2f))
        {
            timerLight4.image.color = Color.green;
        }
        if (timer < (timerLength * .2f))
        {
            timerLight4.image.color = Color.red;
        }
        if(timer > 0)
        {
            timerLight5.image.color = Color.green;
        }
        if (timer < 0)
        {
            timerLight5.image.color = Color.red;
            isGameLost = true;
        }
    }

    void doorMinigame()
    {
        if(progress == 3)
        {
            isGameWon = true;
        }
    }

    void getPassword()
    {
        numOne = Random.Range(1, 9);
        numTwo = Random.Range(1, 9);
        if(numTwo == numOne)
        {
            numTwo = Random.Range(1, 9);
        }
        numThree = Random.Range(1, 9);
        if(numThree == numOne || numThree == numTwo)
        {
            numThree = Random.Range(1, 9);
        }
    }

    void highlightProgress()
    {
        if(progress >= 1)
        {
            progressLight1.image.color = Color.green;
        }
        if(progress >= 2)
        {
            progressLight2.image.color = Color.green;
        }
        if(progress == 3)
        {
            progressLight3.image.color = Color.green;
        }
    }

    IEnumerator gameWon()
    {
        miniGameWindow.color = Color.green;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.green;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.green;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        openDoor();
        gameActive = false;
        player.GetComponent<SB_GameController>().minigameOn = false;
    }

    IEnumerator gameLost()
    {
        miniGameWindow.color = Color.red;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.red;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.red;
        yield return new WaitForSeconds(1f);
        miniGameWindow.color = Color.gray;
        yield return new WaitForSeconds(1f);
        gameActive = false;
        player.GetComponent<SB_GameController>().minigameOn = false;
    }

    public void openDoor()
    {
        Vector3 openLoc = gameObject.transform.position;
        openLoc.y = 1.5f;
        if(!player.GetComponent<SB_PlayerController>().V3Equal(gameObject.transform.position, openLoc))
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = openLoc;
        }
    }

    public void inputOne()
    {
        if(progress == 0)
        {
            if(numOne == 1)
            {
                progress++;
                button1.image.color = Color.green;
            }
            else
            {
                button1.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if(progress == 1)
        {
            if (numTwo == 1)
            {
                progress++;
                button1.image.color = Color.green;
            }
            else
            {
                button1.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if(progress == 2)
        {
            if (numThree == 1)
            {
                progress++;
                button1.image.color = Color.green;
            }
            else
            {
                button1.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputTwo()
    {
        if (progress == 0)
        {
            if (numOne == 2)
            {
                progress++;
                button2.image.color = Color.green;
            }
            else
            {
                button2.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 2)
            {
                progress++;
                button2.image.color = Color.green;
            }
            else
            {
                button2.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 2)
            {
                progress++;
                button2.image.color = Color.green;
            }
            else
            {
                button2.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputThree()
    {
        if (progress == 0)
        {
            if (numOne == 3)
            {
                progress++;
                button3.image.color = Color.green;
            }
            else
            {
                button3.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 3)
            {
                progress++;
                button3.image.color = Color.green;
            }
            else
            {
                button3.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 3)
            {
                progress++;
                button3.image.color = Color.green;
            }
            else
            {
                button3.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputFour()
    {
        if (progress == 0)
        {
            if (numOne == 4)
            {
                progress++;
                button4.image.color = Color.green;
            }
            else
            {
                button4.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 4)
            {
                progress++;
                button4.image.color = Color.green;
            }
            else
            {
                button4.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 4)
            {
                progress++;
                button4.image.color = Color.green;
            }
            else
            {
                button4.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputFive()
    {
        if (progress == 0)
        {
            if (numOne == 5)
            {
                progress++;
                button5.image.color = Color.green;
            }
            else
            {
                button5.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 5)
            {
                progress++;
                button5.image.color = Color.green;
            }
            else
            {
                button5.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 5)
            {
                progress++;
                button5.image.color = Color.green;
            }
            else
            {
                button5.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputSix()
    {
        if (progress == 0)
        {
            if (numOne == 6)
            {
                progress++;
                button6.image.color = Color.green;
            }
            else
            {
                button6.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 6)
            {
                progress++;
                button6.image.color = Color.green;
            }
            else
            {
                button6.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 6)
            {
                progress++;
                button6.image.color = Color.green;
            }
            else
            {
                button6.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputSeven()
    {
        if (progress == 0)
        {
            if (numOne == 7)
            {
                progress++;
                button7.image.color = Color.green;
            }
            else
            {
                button7.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 7)
            {
                progress++;
                button7.image.color = Color.green;
            }
            else
            {
                button7.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 7)
            {
                progress++;
                button7.image.color = Color.green;
            }
            else
            {
                button7.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputEight()
    {
        if (progress == 0)
        {
            if (numOne == 8)
            {
                progress++;
                button8.image.color = Color.green;
            }
            else
            {
                button8.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 8)
            {
                progress++;
                button8.image.color = Color.green;
            }
            else
            {
                button8.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 8)
            {
                progress++;
                button8.image.color = Color.green;
            }
            else
            {
                button8.image.color = Color.red;
                isGameLost = true;
            }
        }
    }

    public void inputNine()
    {
        if (progress == 0)
        {
            if (numOne == 9)
            {
                progress++;
                button9.image.color = Color.green;
            }
            else
            {
                button9.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 1)
        {
            if (numTwo == 9)
            {
                progress++;
                button9.image.color = Color.green;
            }
            else
            {
                button9.image.color = Color.red;
                isGameLost = true;
            }
        }
        else if (progress == 2)
        {
            if (numThree == 9)
            {
                progress++;
                button9.image.color = Color.green;
            }
            else
            {
                button9.image.color = Color.red;
                isGameLost = true;
            }
        }
    }
}
