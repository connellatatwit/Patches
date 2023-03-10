using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] public int value;
    private bool following = false;
    private Transform player;

    public void SetEXPFollow()
    {
        following = true;
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (following)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
