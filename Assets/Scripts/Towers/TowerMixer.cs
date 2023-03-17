using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMixer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem anim;
    [SerializeField] AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.magnitude >= .1f)
        {
            if(collision.transform.GetComponent<ITower>() != null)
            {
                if(collision.transform.GetComponent<ITower>().Level == GetComponent<ITower>().Level)
                {
                    if(collision.transform.GetComponent<ITower>().name == GetComponent<ITower>().name)
                    {
                        //Level up
                        List<IArtifact> temp = collision.gameObject.GetComponent<TowerStats>().Artifacts;
                        for (int i = 0; i < temp.Count; i++)
                        {
                            temp[i].AttachArtifact(gameObject);
                        }
                        Destroy(collision.gameObject);
                        GetComponent<ITower>().LevelUp();
                        Instantiate(anim, transform.position, Quaternion.identity);
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }
}
