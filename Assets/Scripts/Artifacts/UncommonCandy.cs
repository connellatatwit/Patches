using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncommonCandy : MonoBehaviour, IArtifact
{
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
        tower.GetComponent<ITower>().LevelUp();
        Destroy(gameObject);
    }
}
