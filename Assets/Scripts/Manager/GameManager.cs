using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PooledObject DoubleJumpObstacle;
    [SerializeField] PooledObject GoldCoin;
    [SerializeField] PooledObject GoldCoinEffect;
    [SerializeField] PooledObject Growing;
    [SerializeField] PooledObject JumpObstacle;
    [SerializeField] PooledObject Magnet;
    [SerializeField] PooledObject MakeCoin;
    [SerializeField] PooledObject MakeJelly;
    [SerializeField] PooledObject PinkJelly;
    [SerializeField] PooledObject PinkJellyEffect;
    [SerializeField] PooledObject Potion;
    [SerializeField] PooledObject ResumeUI;
    [SerializeField] PooledObject RunningEffect;
    [SerializeField] PooledObject RunningFast;
    [SerializeField] PooledObject SilverCoin;
    [SerializeField] PooledObject SilverCoinEffect;
    [SerializeField] PooledObject SildeObstacle;
    [SerializeField] PooledObject YellowJelly;
    [SerializeField] PooledObject YellowJellyEffect;

    private void Start()
    {
        Manager.Pool.CreatePool(DoubleJumpObstacle, 30, 50);
        Manager.Pool.CreatePool(GoldCoin, 30, 50);
        Manager.Pool.CreatePool(GoldCoinEffect, 30, 50);
        Manager.Pool.CreatePool(Growing, 30, 50);
        Manager.Pool.CreatePool(JumpObstacle, 30, 50);
        Manager.Pool.CreatePool(Magnet, 30, 50);
        Manager.Pool.CreatePool(MakeCoin, 30, 50);
        Manager.Pool.CreatePool(MakeJelly, 30, 50);
        Manager.Pool.CreatePool(PinkJelly, 30, 50);
        Manager.Pool.CreatePool(PinkJellyEffect, 30, 50);
        Manager.Pool.CreatePool(Potion, 30, 50);
        Manager.Pool.CreatePool(ResumeUI, 30, 50);
        Manager.Pool.CreatePool(RunningEffect, 30, 50);
        Manager.Pool.CreatePool(RunningFast, 30, 50);
        Manager.Pool.CreatePool(SilverCoin, 30, 50);
        Manager.Pool.CreatePool(SilverCoinEffect, 30, 50);
        Manager.Pool.CreatePool(SildeObstacle, 30, 50);
        Manager.Pool.CreatePool(YellowJelly, 30, 50);
        Manager.Pool.CreatePool(YellowJellyEffect, 30, 50);
    }
}