using UnityEngine;

public class Asteroid : MonoBehaviour, IHittable
{
    private float _speed = 1f;
    private int _currentSize = 4;

    public HitType hitType => HitType.Enemy;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    private void Explode()
    {
        if (_currentSize <= 0)
        {
            //TODO
        }
        else
        {
            _currentSize--;
            Split();
        }
    }

    private void Split()
    {
        
    }

    public void OnHitTaken()
    {
        Explode();
    }
}
