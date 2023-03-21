using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyKnockBack : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float strength;
    [SerializeField] private float delay;
    public UnityEvent OnBegin, OnDone;


    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 dir = (transform.position - sender.transform.position).normalized;
        rb.AddForce(dir * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();

    }
}
