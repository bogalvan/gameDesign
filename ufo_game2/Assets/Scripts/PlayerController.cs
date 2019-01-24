using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float speed = 10;
    private int count;
    private int lives;
    private Rigidbody2D rb2d;
    private float waitTime = 5.0f;
    SpriteRenderer m_SpriteRenderer;
    Color speedColor = new Color(0, 255, 250);
    private bool gameOver = false;
 

    public Text countText;          
    public Text winText;
    public Text livesText;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        speed = 10;
        count = 0;
        lives = 3;

        winText.text = "";
        SetCountText();
        SetLivesText();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }

    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp") && gameOver == false)
        {
            other.gameObject.SetActive(false);

            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Speed") && gameOver == false)
        {
            other.gameObject.SetActive(false);

            speed = 50;
            m_SpriteRenderer.color = speedColor;
            yield return new WaitForSeconds(2.0f);
            speed = 10;
            m_SpriteRenderer.color = Color.white;
        }
        if (other.gameObject.CompareTag("Cow"))
        {
            speed = 0;
            m_SpriteRenderer.color = Color.red;
            lives--;
            SetLivesText();
            yield return new WaitForSeconds(1.50f);
            m_SpriteRenderer.color = Color.white;
            if(gameOver == false)
            {
                speed = 10;
            }
        }
    }

    void SetLivesText()
    {

        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            winText.text = "You Loose!";
            speed = 0;
            gameOver = true;
        }

    }


    void SetCountText()
    {

        countText.text = "Count: " + count.ToString();

        if (count >= 12)
            winText.text = "You win!";
    }

}
