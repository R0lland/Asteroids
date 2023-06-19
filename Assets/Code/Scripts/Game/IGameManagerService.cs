using ServiceLocatorAsteroid.Service;

public interface IGameManagerService : IGameService
{
    public void Initialize();

    public void Score(int scoreValue);
}
