using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BULLET_SPEED = 10f;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * BULLET_SPEED;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
