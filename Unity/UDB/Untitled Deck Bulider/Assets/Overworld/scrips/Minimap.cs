using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject cam;
    private bool Minnimap;
    // Start is called before the first frame update
    void Start()
    {
        Minnimap = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Minnimap = !Minnimap;
            cam.gameObject.SetActive(Minnimap);
        }


    }
}
