using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SwitchSeen : MonoBehaviour
{
    public float distance = 1;

    public string NextSean;

    public TextMeshProUGUI Instruction;

    public GameObject playerCharacter;

    private Vector3 location, Character;
    // Start is called before the first frame update
    void Start()
    {
        location = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCharcterInRange())
        {
            Instruction.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if(NextSean != null)
                {
                    SceneManager.LoadScene(sceneName: NextSean);
                }
            }
        }
        else
        {
            Instruction.gameObject.SetActive(false);
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
