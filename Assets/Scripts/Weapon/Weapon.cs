using FishNet.Object;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : NetworkBehaviour
{
    public AudioClip shootSound;
    public float shootSpeed;
    public float shootingRange;
    public float bulletsInTime;
    public bool isAutomatic;
    public Vector2 spread;
    private bool shootingInput;
    private bool canShoot = true;
    public GameObject GunHolder;
    public Camera cam;
    public Transform attackPoint;
    public GameObject bullet;

    private Controls controls;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            cam = Camera.main;
            GunHolder.transform.SetParent(cam.transform);
        }
    }

    private void Start()
    {
        controls = new Controls();
        controls.Enable();
    }

    private void Update()
    {
        Inputing();
        ChecksForShooting();
    }
    
    private void Inputing()
    {
        if (!base.IsOwner) return;
        if (!canShoot) return;
        if (isAutomatic) shootingInput = controls.Land.ShootWeapon.inProgress;
        else shootingInput = controls.Land.ShootWeapon.triggered;
    }

    private void ChecksForShooting()
    {
        //if (Player.Instance.GetComponent<PauseMenuManager>().isPaused) return;
        //if (Player.Instance.GetComponent<PlayerInventory>().inStash) return;
        if (!canShoot) return;
        if (!shootingInput) return;
        Shoot();
    }

    private void Shoot()
    {
        canShoot = false;
        gameObject.GetComponent<GunRecoil>().Fire();
        //camRecoil.GetComponent<CameraRecoil>().Fire();
        float x = Random.Range(-spread.x, spread.x);
        float y = Random.Range(-spread.y, spread.y);

        Vector3 bulletDirection = attackPoint.forward + new Vector3(x, y, 0);
        
        SpawnBullet(bullet, attackPoint.position, bulletDirection);
        
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        Invoke(nameof(ResetShot), shootSpeed);
    }

    private void ResetShot()
    {
        canShoot = true;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnBullet(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject spawned = Instantiate(prefab, position, Quaternion.identity);
        spawned.transform.forward = direction.normalized;
        ServerManager.Spawn(spawned);
    }

}