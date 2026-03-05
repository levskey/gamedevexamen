using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f; // speler snelheid
    public float xLimit = 8f; // maximale range speler
    public GameObject bulletPrefab; // object van de kogel om elke keer in te spawnen
    public Transform firePoint; // het punt vanwaar de kogel vliegt
    public AudioClip shootSound; // schiet geluid wanneer speler schiet
    public AudioSource audioSource; // audiosource

    public float shootCooldown = 0.35f; //schot cooldown om moeilijker te maken en inhouden/spammen te voorkomen
    private float nextShootTime = 0f; // volgende schiet tijd niet echt gebruikt tho

    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        float move = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        if(pos.x == xLimit)
        {
            pos.x = -xLimit;
            pos.x += move * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
            transform.position = pos;
        }
        else if (pos.x == -xLimit)
        {
            pos.x = xLimit;
            pos.x += move * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
            transform.position = pos;
        }
        else
        {
            pos.x += move * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
            transform.position = pos;
        }


        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShootTime)
        {
            audioSource.PlayOneShot(shootSound);
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            nextShootTime = Time.time + shootCooldown;
        }
    }
}