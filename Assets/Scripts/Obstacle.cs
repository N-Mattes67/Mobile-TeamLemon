using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    public float speed = 5.0f;
    public float sideSpeed; //speed for side to side movement

    [HideInInspector]
    public int scoreModifier = 1;

    private Vector3 screenBounds;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!PlayerPrefs.HasKey("vibrate"))
            {
                Handheld.Vibrate();
            }
            else if (PlayerPrefs.GetInt("vibrate") == 1)
            {
                Handheld.Vibrate();
            }
            Destroy(col.gameObject);
            GameMan.Instance.State = GameMan.GameState.Ended;
        }
    }

    void Start()
    {
     
        GetComponent<Rigidbody2D>().velocity = new Vector2(-sideSpeed, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
    }

    void Update()
    {
        //this is still spaghet and needs optimizing (im sorry -toadje)
        if (transform.position.x == screenBounds.x || transform.position.x > screenBounds.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-sideSpeed, -speed);
        }
        if (transform.position.x == -screenBounds.x || transform.position.x < -screenBounds.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(sideSpeed, -speed);
        }

       if (transform.position.y < screenBounds.y)
        {
            Destroy(gameObject);
            GameMan.Instance.IncrementScore(scoreModifier);
        }
    }
}
