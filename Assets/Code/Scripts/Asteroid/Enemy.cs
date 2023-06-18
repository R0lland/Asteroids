using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    public HitType hitType => HitType.Enemy;

    public virtual void OnHitTaken()
    {
        
    }
}
