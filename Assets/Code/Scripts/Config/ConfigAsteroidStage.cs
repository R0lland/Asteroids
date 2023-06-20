using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Config/Asteroid Stage Config")]
public class ConfigAsteroidStage : ScriptableObject
{
    public int id;
    public float minSpeed;
    public float maxSpeed;
    [Range(0, 1)] public float size;
    public int scoreValue;
    public int additionalAsteroid;
}
