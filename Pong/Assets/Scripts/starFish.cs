using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starFish : MonoBehaviour
{

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * 50, 0, Time.deltaTime * 25);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D rb2d = col.gameObject.GetComponent<Rigidbody2D>();
            rb2d.AddForce(rb2d.velocity * 10);
            Destroy(gameObject);

            gameManager.createNewStarFish();

        }

    }
}
