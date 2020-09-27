using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardScript : MonoBehaviour
{
    public bool playable, held;
    public bool activeSupply = false;
    public Vector3 originalPos;
    public GameObject collidedGameobject;
    public bool targetableEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        collidedGameobject = col.gameObject;
        if (col.tag == "Enemy" && targetableEnemy)
        {
            col.GetComponent<Animator>().ResetTrigger("Idle");
            col.GetComponent<Animator>().SetTrigger("cardHoverEnter");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy" && targetableEnemy)
        {
            col.GetComponent<Animator>().SetTrigger("Idle");
            col.GetComponent<Animator>().ResetTrigger("cardHoverEnter");
        }
        //collidedGameobject = null;
    }
}
