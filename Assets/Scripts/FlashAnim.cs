using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnim : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    private Material originalMat;
    [SerializeField] Material flashMat;

    [SerializeField] NonPlayerHealth health;
    [SerializeField] float flashTime = .125f;

    private Coroutine flashRoutine;

    private void Start()
    {
        originalMat = sr.material;
        health = GetComponent<NonPlayerHealth>();
        health.HurtEvents.Add(new UnityEngine.Events.UnityEvent());
        health.HurtEvents[health.HurtEvents.Count-1].AddListener(Flash);
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine());

    }

    public IEnumerator FlashRoutine()
    {
        Debug.Log("FLASHING");
        sr.material = flashMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = originalMat;
    }
}
