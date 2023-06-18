public enum HitType
{
    None,
    Player,
    Enemy
}

public interface IHittable
{
    public HitType hitType { get;}
    public void OnHitTaken();
}
