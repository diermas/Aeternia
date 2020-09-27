using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAndColection : MonoBehaviour
{
    public HealthAndGold PlayerGold;
    private Dictionary<int, cardClass> Colection = new Dictionary<int, cardClass>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCard(int id)
    {
        if((id<=mageCards.firstCard)&& (id >= mageCards.lastCard))
        {
            Colection.Add(id, CardsReadIn.CompleteDeckOfCards[id]);
        }
        else if ((id <= warriorCards.firstCard) && (id >= warriorCards.lastCard))
        {
            Colection.Add(id, CardsReadIn.CompleteDeckOfCards[id]);
        }
        else if ((id <= priestCards.firstCard) && (id >= priestCards.lastCard))
        {
            Colection.Add(id, CardsReadIn.CompleteDeckOfCards[id]);
        }
        else if ((id <= rogueCards.firstCard) && (id >= rogueCards.lastCard))
        {
            Colection.Add(id, CardsReadIn.CompleteDeckOfCards[id]);
        }
    }


}
