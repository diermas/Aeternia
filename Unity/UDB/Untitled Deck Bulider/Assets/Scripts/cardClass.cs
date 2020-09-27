using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardClass : ScriptableObject
{
    public bool takesTarget, isBuff;
    public string title, subClass, cardEffectText, targetType;
    public int cardCost, physicalDamage, defenceGain, heal, draw, discard, resourceCount, cardDraw, cardNumber, cardCount;

    //for enemy
    public int rangeMin, rangeMax, numberOfTargets;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
