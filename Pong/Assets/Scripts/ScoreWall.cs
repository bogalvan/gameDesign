using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWall : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Ball")
        {
            string wallName = transform.name;
            gameManager.Score(wallName);

        }
    }
}