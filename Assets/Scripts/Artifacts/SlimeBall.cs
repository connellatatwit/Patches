using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour, IArtifact
{
    [SerializeField] float slowAmount;
    [SerializeField] GameObject slimeAttachmentPrefab;

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
        GameObject attachment = Instantiate(slimeAttachmentPrefab, tower.transform.position, Quaternion.identity);
        attachment.transform.parent = tower.transform;

        tower.GetComponent<TowerStats>().IncreaseSlowAmount(slowAmount);
        tower.GetComponent<TowerStats>().IncreaseSlowTime(1f);

        Destroy(GetComponent<Collider2D>());
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
