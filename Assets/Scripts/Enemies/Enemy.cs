using System.Collections;
using System.Collections.Generic;
using FishNet.Managing.Server;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public readonly SyncVar<float> Health = new SyncVar<float>(440f);
    
    [ServerRpc(RequireOwnership = false)]
    public void DealDamage(Enemy script, float damage)
    {
        script.Health.Value -= damage;
        if (script.Health.Value <= 0f) script.Kill();
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void Kill()
    {
        ServerManager.Despawn(gameObject);
    }
}
