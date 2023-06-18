using UnityEngine;

public class Asteroid : Enemy
{
    private float _speed = 0.3f;
    private int _currentSize = 4;

    private void Update()
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

    public override void OnHitTaken()
    {
        Explode();
    }
}
