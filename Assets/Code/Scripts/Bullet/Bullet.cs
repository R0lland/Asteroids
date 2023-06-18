using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ConfigBullet config;

    private HitType _target = HitType.None;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * config.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittable = GetComponent<IHittable>();
        Debug.LogError(hittable);
        if (hittable != null)
        {
            CheckForTargetHit(hittable);
        }
    }

    private void CheckForTargetHit(IHittable targetHit)
    {
        Debug.LogError(targetHit.hitType + " - " + _target);
        if (_target == targetHit.hitType)
        {
            Debug.LogError("HIT");
            targetHit.OnHitTaken();
        }
    }

    public void SetTarget(HitType hitType)
    {
        Debug.LogError(hitType);
        _target = hitType;
    }
}
