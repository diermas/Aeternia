using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    private Vector3 movment;

    private float movespeed = 0.05f;

    private int up, left, right, down;

    public GameObject character;
    public GameObject Inventory,health;
    public SpriteRenderer charimage;

    public Sprite forward1, forward2, forward3, left1, left2, left3, down1, down2, down3, right1, right2, right3;

    // Start is called before the first frame update
    void Start()
    {
        movment = character.transform.position;

        up = 0;
        down = 0;
        left = 0;
        right = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.active);
            health.SetActive(!health.active);
        }
        if (!Inventory.active)
        {

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                movment.y += movespeed;
                switchsprite(up, forward1, forward2, forward3);
                up++;
                if (up > 2)
                {
                    up = 0;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                movment.x -= movespeed;
                switchsprite(left, left1, left2, left3);
                left++;
                if (left > 2)
                {
                    left = 0;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                movment.y -= movespeed;
                switchsprite(down, down1, down2, down3);
                down++;
                if (down > 2)
                {
                    down = 0;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                movment.x += movespeed;
                switchsprite(right, right1, right2, right3);
                right++;
                if (right > 2)
                {
                    right = 0;
                }
            }

            character.transform.position = movment;
        }
    }

    void switchsprite(int sprites, Sprite cycle1, Sprite cycle2, Sprite cycle3)
    {
        switch(sprites)
        {
            case 0:
                charimage.sprite = cycle1;
                break;
            case 1:
                charimage.sprite = cycle2;
                break;
            case 2:
                charimage.sprite = cycle3;
                break;
        }


    }
}
