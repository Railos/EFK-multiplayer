using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Utility;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float damage;
    public float penetration;

    private void Start()
    {
        Invoke(nameof(DestroyBullet), 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("ZV");
        if (other.gameObject.GetComponent<Enemy>())
        {
            other.gameObject.GetComponent<Enemy>().DealDamage(other.gameObject.GetComponent<Enemy>(), damage);
        }
        DestroyBullet();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyBullet()
    {
        ServerManager.Despawn(gameObject);
    }
}
