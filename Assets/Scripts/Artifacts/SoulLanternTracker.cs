using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLanternTracker : MonoBehaviour
{
    [SerializeField] GameObject flameBulletPrefab;
    private int dmg;
    private float speed;
    private float lifeTime = 1f;
    private bool naturalDeath;
    public void InitTracker(int dmg, float speed)
    {
        this.speed = speed;
        this.dmg = dmg;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            naturalDeath = true;
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!naturalDeath)
        {
            if (!this.gameObject.scene.isLoaded) return;
            GameObject bullet = Instantiate(flameBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<SoulBullet>().InitBullet(null, dmg, speed/2);
        }
    }
    void OnDisable()
    {
        // Instantiate objects here
    }
}