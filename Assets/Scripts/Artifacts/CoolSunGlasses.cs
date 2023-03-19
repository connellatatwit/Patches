using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolSunGlasses : MonoBehaviour, IArtifact
{
    [SerializeField] GameObject sunGlassesAttachmentPrefab;
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
        GameObject attachment = Instantiate(sunGlassesAttachmentPrefab, tower.transform.position, Quaternion.identity);
        attachment.transform.parent = tower.transform;
        attachment.GetComponent<SunGlassesAttachment>().Init(tower.transform);
        attachment.transform.position += new Vector3(0, .5f);
        tower.GetComponent<TowerStats>().AddArtifact(this);
        Destroy(GetComponent<Collider2D>());
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
