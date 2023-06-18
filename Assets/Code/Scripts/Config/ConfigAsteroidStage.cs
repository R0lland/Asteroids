using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Asteroid Stage Config")]
public class ConfigAsteroidStage : ScriptableObject
{
    public float minSpeed;
    public float maxSpeed;
    public float size;
}
