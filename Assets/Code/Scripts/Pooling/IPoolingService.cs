using ServiceLocatorAsteroid.Service;

public interface IPoolingService : IGameService
{
    public void AddToPool(IPooling i);

    public void RemoveFromPool(IPooling i);
}
