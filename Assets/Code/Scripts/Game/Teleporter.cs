using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private Transform _teleportTo;
    [SerializeField] private bool _teleportingOnX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_teleportingOnX)
        {
            collision.transform.position = new Vector3(_teleportTo.position.x + _offset, collision.transform.position.y, collision.transform.position.z);
        }
        else
        {
            collision.transform.position = new Vector3(collision.transform.position.x, _teleportTo.position.y + _offset, collision.transform.position.z);
        }
       
    }
}
