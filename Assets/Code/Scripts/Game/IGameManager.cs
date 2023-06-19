using ServiceLocatorAsteroid.Service;

public interface IGameManager : IGameService
{
    public void Initialize();

    public void Score(int scoreValue);
}
