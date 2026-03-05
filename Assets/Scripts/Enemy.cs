using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4.5f; // zet snelheid
    public float destroyY = -3.75f; // zet vanaf waar hij destroyed

    private Rigidbody2D rb;
    private bool counted = false; // zet counted false om later true te zetten
    public AudioClip explosionSound; // explosie geluid
    public GameObject explosionFX; // explosie visuals
    public static float SpeedBonus = 0.3f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.gravityScale = 0f; 
    }

    public void ApplySpeed(float bonus)
    {
        float appliedSpeed = speed + bonus;
        rb.linearVelocity = Vector2.down * appliedSpeed;

        
    }

    void Update()
    {
        // als tegenstander langs mij gaat tel 1 hartje eraf en vernietig vliegtuig
        if (!counted && transform.position.y < destroyY)
        {
            counted = true;
            GameManager.instance.LoseLife();
            //Debug.Log("lost life");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameManager.instance.AddScore(1);

            if (explosionFX != null)
            {
                GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
                Destroy(fx, 1.5f); // vernietigt het effect na 1.5 sec
            }

            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position, 2.5f);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.instance.LoseLife();
            Destroy(gameObject);
        }
    }
}