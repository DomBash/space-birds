using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float thrust = 15.0f;

    public SystemController system;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
    }

    void Update()
    {
        /*if (Input.GetButtonDown("Fire1") && system.isInGame)
        {
            Tap();
        }*/
    }

    public void Tap()
    {
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        //rb2D.AddForce(transform.up * 1000);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Meteor")
            FindObjectOfType<SystemController>().GameOver();

        if (other.tag == "Coin")
        {
            FindObjectOfType<SystemController>().AddCoin();
            Destroy(other.gameObject);
        }
    }

    public void Play()
    {
        rb2D.gravityScale = 0;
        rb2D.velocity = Vector2.zero;
        transform.position = new Vector2(-4f, 0f);
    }

    public void TapToPlay()
    {
        rb2D.gravityScale = 3;
        
    }
}
