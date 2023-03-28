using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLanternTracker : MonoBehaviour
{
    [SerializeField] GameObject flameBulletPrefab;
    private float lifeTime = 1f;
    private bool naturalDeath;
    private TowerStats tS;
    public void InitTracker(TowerStats ts)
    {
        tS = ts;
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
            bullet.GetComponent<IBullet>().InitBullet(null, tS.Damage, tS.BulletSpeed, tS.SlowAmount, tS.SlowLength, tS.StunLength);
        }
    }
    void OnDisable()
    {
        // Instantiate objects here
    }
}