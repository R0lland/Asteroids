using UnityEngine;

[CreateAssetMenu(menuName = "Config/Game Config")]
public class ConfigGame : ScriptableObject
{
    public int maxLives;
    public int timeToRespawn;
    public int timeToLose;
    public int milestoneToNextLife;
    public int additionalAsteroidsEachStage;
    public int maxAsteroids;
    public int initialAsteroidsAmount;
    public int scoreToWin;
}
