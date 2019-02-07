using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed;
    private Vector2 force;
    public bool started = false;

    AudioSource fxSound;
    public AudioClip bounceMusic;


    void GoBall()
    {
        started = true;
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            force = new Vector2(20, Random.Range(12, -12));
            force = force.normalized; 
            rb2d.AddForce(force*60);
        }
        else
        {
            force = new Vector2(-20, Random.Range(12, -12));
            force = force.normalized;
            rb2d.AddForce(force*60);
        }
    }

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fxSound = GetComponent<AudioSource>();
        fxSound.playOnAwake = false;
        fxSound.clip = bounceMusic;
    }

    public void startGame()
    {
        ResetBall();
        Invoke("GoBall", 1f);
    }

    void ResetBall()
    {
        rb2d.velocity = new Vector2(0, 0);
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2.0f) + (col.collider.attachedRigidbody.velocity.y / 3.0f);
            vel = vel.normalized;
            rb2d.velocity = vel * speed;
            fxSound.Play();
        }
    }
        

}