using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.BindFactory<Bullet, BulletFactory>().AsSingle();
    }
}