using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerConnector : MonoBehaviour
{
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] LineRenderer lr;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField] float fps = 30f;
    private float fpsCounter;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Init(Transform newtarget1, Transform newTarget2)
    {
        target2 = newTarget2;
        target1 = newtarget1;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            lr.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }


        if(target1 == null || target2 == null)
        {
            Destroy(gameObject);
        }

        lr.SetPosition(0, target1.position);
        lr.SetPosition(1, target2.position);
    }
}
