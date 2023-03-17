using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLantern : MonoBehaviour
{
    [SerializeField] GameObject soulLanternAttachment;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ITower>() != null)
        {
            //Create Lantern thing
            GameObject attachment = Instantiate(soulLanternAttachment, collision.transform.position, Quaternion.identity);
            attachment.transform.parent = collision.transform;
            attachment.GetComponent<SoulLanternAttachment>().Init(collision.transform.GetComponent<TowerStats>());
            Destroy(gameObject);
        }
    }
}
