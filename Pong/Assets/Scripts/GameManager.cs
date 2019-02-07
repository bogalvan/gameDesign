using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState { startMenu, gamePlay, gameOver }
    public static GameState gameState = GameState.startMenu;
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public  int winAmount;
    private bool startGame = false;
    
    //UI
    public Text startText;
    public Text p1Text;
    public Text p2Text;
    public Text winnerText;
    public Ball ball;

    private static GameObject button;
    private static GameObject game;

    public starFish starfishPrefab;
    starFish starfish = null;


    // Use this for initialization
    void Start()
    {
        button = GameObject.FindGameObjectWithTag("Button");
        game = GameObject.FindGameObjectWithTag("gameObjects");
        createNewStarFish();
        createNewStarFish();
        createNewStarFish();

        p1Text.text = "";
        p2Text.text = "";


    }

    public void Update()
    {
        if(gameState == GameState.startMenu)
        {
            button.SetActive(true);
            game.SetActive(false);
        }
        if(gameState == GameState.gamePlay)
        {  
            
            button.SetActive(false);
            game.SetActive(true);
            startText.text = "";
            winnerText.text = "";
            if (startGame == false)
            {
                ball.startGame();
                startGame = true;
            }
            game.SetActive(true);
        }
        if (gameState == GameState.gameOver)
        {
            ball.started = false;
            if (PlayerScore1 >= winAmount)
            {
                winnerText.text = "Player 1 wins!";
            }
            else
            {
                winnerText.text = "Player 2 wins!";
            }
            button.SetActive(true);
            game.SetActive(false);
            p1Text.text = "";
            p2Text.text = "";
            startText.text = "Restart?"; 
            startGame = false;
        }



    }

    public void createNewStarFish()
    {
        starfish = Instantiate(starfishPrefab);
        starfish.transform.position = new Vector3(Random.Range(-5.5f, 5.5f), Random.Range(-3.5f, 3.5f), 0);
    }

    public static void buttonClicked()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;
        gameState = GameState.gamePlay;
    }

    public void Score(string wallID)
    {

        if (ball.started == true)
        {
            if (wallID == "RightWall")
            {
                PlayerScore1++;
                p1Text.text = PlayerScore1.ToString();
            }
            else
            {
                PlayerScore2++;
                p2Text.text = PlayerScore2.ToString();
            }
            if (PlayerScore1 < winAmount && PlayerScore2 < winAmount)
            {
                ball.SendMessage("RestartGame", 1, SendMessageOptions.RequireReceiver);
            }
            else
            {
                gameState = GameState.gameOver;
            }
        }
    }


}


