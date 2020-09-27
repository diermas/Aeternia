using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterEncounter : MonoBehaviour
{
    public string NextScene;
    public string enemyName;
    public GameObject playerCharacter;
    // Start is called before the first frame update
    public float distance = 1;

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
            if (Input.GetKey(KeyCode.E))
            {
                if (NextScene != null)
                {
                    SceneManager.LoadScene(sceneName: NextScene);
                    GameController.loadEnemy = enemyName;
                }
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

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("dafe");
        if (col.gameObject == playerCharacter)
        {
            Debug.Log("dafe");
        }
    }
}
