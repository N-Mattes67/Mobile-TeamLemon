using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [HideInInspector]
    public float speed = 5.0f;

    [HideInInspector]
    public int coinsModifier = 1;

    private Vector3 screenBounds;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameMan.Instance.IncrementCoins(coinsModifier);
        }
    }

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (transform.position.y < screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}