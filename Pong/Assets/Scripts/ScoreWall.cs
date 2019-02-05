using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWall : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Ball")
        {
            string wallName = transform.name;
            GameManager.Score(wallName);
            col.gameObject.SendMessage("RestartGame", 1, SendMessageOptions.RequireReceiver);
        }
    }
}