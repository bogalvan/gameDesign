using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomMovement : MonoBehaviour
{

    private Rigidbody rb; //refrence to the rigidbody component of the Player 

    private float moveX;
    private float moveZ;
    private float deltaX;
    private float deltaZ;



    void Start()
    { //all code in start method gets called in first frame of the game 
        rb = GetComponent<Rigidbody>();
        moveX = Random.Range(-10f, 10f);
        moveZ = Random.Range(-10f, 10f);
        deltaX = Random.Range(-.1f, .1f);
        deltaZ = Random.Range(-.1f, .1f);
    }

    //update is called before rendering a frame
    //fixed update is where physics code should go 

    void FixedUpdate()
    {

        moveX += deltaX;
        moveZ += deltaZ;
        Vector3 move = new Vector3(moveX, 0.0f, moveZ);
        rb.MovePosition(move);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LRWalls"))
        {
            deltaX *= -1;
        }
        if (other.gameObject.CompareTag("TBWalls"))
        {
            deltaZ *= -1;
        }
    }


}
