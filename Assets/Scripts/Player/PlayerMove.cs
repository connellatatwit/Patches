using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SpriteRenderer playerImage;
    private bool right = true;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y);

        transform.Translate(moveDir * speed * Time.deltaTime);
        if (right)
        {
            if(x < 0)
            {
                right = false;
                playerImage.flipX = true;
            }
        }
        else
        {
            if (x > 0)
            {
                right = true;
                playerImage.flipX = false;
            }
        }
    }
}
