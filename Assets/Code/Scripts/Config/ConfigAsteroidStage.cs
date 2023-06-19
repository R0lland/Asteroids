using UnityEngine;

[CreateAssetMenu(menuName = "Config/Asteroid Stage Config")]
public class ConfigAsteroidStage : ScriptableObject
{
    public int id;
    public float minSpeed;
    public float maxSpeed;
    public float size;
    public int additionalAsteroid;
}
