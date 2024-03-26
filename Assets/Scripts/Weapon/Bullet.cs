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
        GetComponent<Rigidbody>().AddForce(transform.forward * 300f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
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
