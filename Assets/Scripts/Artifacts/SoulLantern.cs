using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLantern : MonoBehaviour, IArtifact
{
    [SerializeField] GameObject soulLanternAttachment;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ITower>() != null)
        {
            //Create Lantern thing
            AttachArtifact(collision.gameObject);
        }
    }

    public void AttachArtifact(GameObject tower)
    {
        GameObject attachment = Instantiate(soulLanternAttachment, tower.transform.position, Quaternion.identity);
        attachment.transform.parent = tower.transform;
        attachment.GetComponent<SoulLanternAttachment>().Init(tower.transform.GetComponent<TowerStats>());
        tower.GetComponent<TowerStats>().AddArtifact(this);
        Destroy(GetComponent<Collider2D>());
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
