using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f; // kogel snelheid
    public float destroydistance = 4.5f; // vanaf waar kogel vernietigt

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); // dit laat de kogel bewegen
        if (transform.position.y > destroydistance) // vanaf hier laat hij de kogel kapot maken
        {
            Destroy(gameObject); // hier word de kogel kapot gemaakt
        }
    }

}
