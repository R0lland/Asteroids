using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ConfigBullet config;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * config.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
