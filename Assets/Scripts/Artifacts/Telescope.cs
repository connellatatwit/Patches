using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : MonoBehaviour, IArtifact
{
    [SerializeField] GameObject scopeAttachment;
    [SerializeField] float rangeIncrease;

    public string artName;

    public string ArtifactName => artName;

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
        GameObject attachment = Instantiate(scopeAttachment, tower.transform.position, Quaternion.identity);
        attachment.transform.parent = tower.transform;
        tower.GetComponent<TowerStats>().IncreaseRange(rangeIncrease);

        tower.GetComponent<TowerStats>().AddArtifact(this);
        Destroy(GetComponent<Collider2D>());
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
