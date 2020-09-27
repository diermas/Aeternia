using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackable : MonoBehaviour
{
    public GameObject playerCharacter;
    public static float[] playerCharacterPostion = new float[3];

    void Update()
    {
        playerCharacterPostion[0] = playerCharacter.transform.localPosition.x;
        playerCharacterPostion[1] = playerCharacter.transform.localPosition.y;
        playerCharacterPostion[2] = playerCharacter.transform.localPosition.z;
    }
}
