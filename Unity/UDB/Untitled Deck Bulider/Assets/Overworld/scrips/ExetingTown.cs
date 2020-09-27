using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExetingTown : MonoBehaviour
{
    public GameObject playerCharacter;
    public float distance = 1;
    private Vector3 location, Character;

    void Start()
    {
        location = this.gameObject.transform.position;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Overworld");
    }
    void Update()
    {
        if (IsCharcterInRange())
        {
            if (Input.GetKey(KeyCode.E))
            {
                    SceneManager.LoadScene("Overworld");
            }
        }
    }

    bool IsCharcterInRange()
    {
        Character = playerCharacter.transform.position;
        if ((location.x - Character.x < distance) && (location.x - Character.x > -distance))
        {
            if ((location.y - Character.y < distance) && (location.y - Character.y > -distance))
            {
                return true;
            }

        }
        return false;
    }
}
