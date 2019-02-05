using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameState { startMenu, gamePlay, restartMenu }
    public static GameState gameState = GameState.startMenu;
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    //UI
    public Text startText;
    public static Text p1Text;
    public static Text p2Text;

    private static GameObject button;
    private static GameObject game;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        button = GameObject.FindGameObjectWithTag("Button");
        game = GameObject.FindGameObjectWithTag("gameObjects");

        p1Text.text = "";
        p2Text.text = "";

    }

    public void Update()
    {
        if(gameState == GameState.startMenu)
        {
            game.SetActive(false);
        }
        //print(gameState);
        if(gameState == GameState.gamePlay)
        {
            button.SetActive(false);
            startText.text = "";
            game.SetActive(true);
        }
    }

    public static void buttonClicked()
    {
        print("changing game state");
        gameState = GameState.gamePlay;

    }

    public static void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            print("1");
            PlayerScore1++;
            p1Text.text = PlayerScore1.ToString();
        }
        else
        {
            print("2");
            PlayerScore2++;
            p2Text.text = PlayerScore2.ToString();
        }
    }


}


