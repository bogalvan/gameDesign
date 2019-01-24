using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class badPickup : MonoBehaviour
{

    private Rigidbody2D rb; //refrence to the rigidbody component of the Player 

    private float moveX;
    private float moveY;
    private float deltaX;
    private float deltaY;



    void Start()
    { //all code in start method gets called in first frame of the game 
        rb = GetComponent<Rigidbody2D>();
        moveX = Random.Range(-10f, 10f);
        moveY = Random.Range(-10f, 10f);
        deltaX = Random.Range(-.1f, .1f);
        deltaY = Random.Range(-.1f, .1f);
    }

    //update is called before rendering a frame
    //fixed update is where physics code should go 

    void FixedUpdate()
    {

        moveX += deltaX;
        moveY += deltaY;
        Vector3 move = new Vector3(moveX, moveY, 0.0f);
        rb.MovePosition(move);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LRWalls"))
        {
            deltaX *= -1;
        }
        if (other.gameObject.CompareTag("TBWalls"))
        {
            deltaY *= -1;
        }
    }


}
