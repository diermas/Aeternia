using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Enemy : MonoBehaviour
{
    #region Int's
    public int EnemyID;
    public int poisonDropOffDamage;
    public int damageDropOff;
    public int burn;
    public int frost;
    public int markOfPainDamage;
    public int markOfFireDamage;
    public int markOfFireMDamage;
    public int health;
    public int maxHealth;
    public int lastHealth;
    public int firstCardValue, lastCardValue, poisonGain = 0;
    public int defence = 0;
    public int maxDefence;
    #endregion
    public string className, resourceName;
    public List<int> currentCards, deck;
    public List<Player> tauntList;
    public GameObject thisEnemy, healthBarObject, iconBar, poisonIcon, markOfPainIcon, markOfFireIcon, stunnedIcon, frozenIcon, markOfFireMIcon, defenceValue;
    public bool frozen = false, stunned = false, markOfPain = false, markOfFire = false, markOfFireM = false, markOfFireMCheckInitiated = false; // states of each Icon
    public Dictionary<string, float> resistanceDamageTypes;
    public Dictionary<string, int> frostTargetablePlayer;
    public Dictionary<string, bool> iconPresent;
    public string child;
    public bool classContainsDefence; //Need to set where the Enemy type has a defence counter
    
    public string markOfPainDamageType, markAllEnemiesDamageType;
    public Vector3 cardPosition = new Vector3(), lastIconPositon = new Vector3();

    public Enemy()
    { 
        //SET GUI's
        poisonIcon = Resources.Load<GameObject>("UI/Poison");
        markOfPainIcon = Resources.Load<GameObject>("UI/Mark of Pain");
        markOfFireIcon = Resources.Load<GameObject>("UI/FireIcon");
        stunnedIcon = Resources.Load<GameObject>("UI/Stun Icon");
        stunnedIcon = Resources.Load<GameObject>("UI/Stun Icon");
        frozenIcon = Resources.Load<GameObject>("UI/Snowflake Icon");
        markOfFireMIcon = Resources.Load<GameObject>("UI/FireMIcon");

        maxDefence = 10;
        poisonDropOffDamage = 0;
        damageDropOff = 0;
        resistanceDamageTypes = new Dictionary<string, float>();
        currentCards = new List<int>();
        deck = new List<int>();
        tauntList = new List<Player>();
        frostTargetablePlayer = new Dictionary<string, int>();
        iconPresent = new Dictionary<string, bool>();
        iconPresent.Add("Frozen", false);
        iconPresent.Add("Stunned", false);
        iconPresent.Add("MarkOfPain", false);
        iconPresent.Add("MarkOfFire", false);
        iconPresent.Add("Poison", false);
        iconPresent.Add("MarkOfFireM", false);
    }

    // If adding a new Icon, you must create a state in which it's true, add it's name to the iconPresent Dictionary, add a prefab to the Enemy Script (for instansiating), add to 
    // CurrentIconCheck & ReturnPrefabIcon, ensure the prefab's tag is correct. 

    public void NewTurn()
    {
        #region Poison
        if (poisonGain > 0)
        {
            if ((poisonDropOffDamage + poisonGain) > 10)
            {
                poisonDropOffDamage = 10;
            }
            else
            {
                poisonDropOffDamage += poisonGain;
            }
            thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Poisoned";
            thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
            IconBarRefresh();
        }
        #endregion
    }
    public void ClearBuffs()
    {
        tauntList.Clear();
        damageDropOff = 0;
        if (burn > 0)
        {
            burn -= 1;
        }
    }
    public void HealthBarRefresh()
    {
        TextMeshPro T = new TextMeshPro();
        T = healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        T.text = health.ToString();
        float scale = (Mathf.Round(health) / Mathf.Round(maxHealth));
        healthBarObject.transform.GetChild(3).transform.localScale = new Vector3(scale, 1, 1);
    }
    public void AddDefence(int defenceAdded)
    {
        if (defenceAdded + defence >= maxDefence)
            defence = maxDefence;
        else
            defence += defenceAdded;
    }
    public void AddBurn(int burntAdded)
    {
        if (burntAdded + burn >= 4)
        {
            burn = 4;
        }
        else 
        {
            burn += 1;
        }
    }
    public void AddFrost(int frostAdded)
    {
        if (frost + frostAdded >= 5)
        {
            frost = 0;
            frozen = true;
            thisEnemy.transform.GetChild(3).gameObject.SetActive(true);

            //Animation
            thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Froze";
            thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
        else
        {
            frost += frostAdded;

            //Animation
            thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Frost: +" + frostAdded;
            thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
    }
    public void ClearDefence()
    {
        defence = 0;
    }
    public void UnFreeze()
    {
        frozen = false;
        frost = 0;
        thisEnemy.transform.GetChild(3).gameObject.SetActive(false);
    }
    public IEnumerator MarkCheck(List<Enemy> listOfEnemies)
    {
        //checks for Mark of Pain
        if (markOfPain)
        {
            yield return new WaitForSeconds(1.0f);
            int temp = Convert.ToInt32(Math.Ceiling(markOfPainDamage * resistanceDamageTypes["Physical"]));
            health -= temp;
            markOfPainDamage = 0;
            markOfPain = false;

            //Animation
            IconBarRefresh();
            HealthBarRefresh();
            thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + temp.ToString() + "\n" + "Physical Mark";
            thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }

        if (markOfFireM)
        {
            yield return new WaitForSeconds(1.0f);
            foreach (Enemy e in listOfEnemies)
            {
                int temp = Convert.ToInt32(Math.Ceiling(markOfFireMDamage * e.resistanceDamageTypes["Fire"]));
                e.health -= temp;

                //Animation
                e.IconBarRefresh();
                e.HealthBarRefresh();
                e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + temp.ToString() + "\n" + "Fire Mark";
                e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                yield return new WaitForSeconds(1.0f);
            }
            markOfFireMDamage = 0;
            markOfFireM = false;
            markOfFireMCheckInitiated = true;
            IconBarRefresh();

            
        }

        else
        {
            markOfFireMCheckInitiated = false;
        }
    }
    public void IconBarRefresh()
    {
        lastIconPositon = new Vector3(0, 0, 2000);
        bool iconRemoved = false;
        List<GameObject> tempRemove = new List<GameObject>();
        List<string> tempPresentChange = new List<string>();
        if (iconBar.transform.childCount > 0)
        {
            //check all Icons should still be in place.
            for (int i = 0; i < iconBar.transform.childCount; i++)
            {
                if (CurrentIconCheck(iconBar.transform.GetChild(i).tag))
                {
                    if (iconBar.transform.GetChild(i).transform.localPosition != lastIconPositon)
                    {
                        iconBar.transform.GetChild(i).transform.localPosition = lastIconPositon;
                    }
                    lastIconPositon += new Vector3(60, 0, 0);
                }
                else
                {
                    if (iconRemoved)
                    {
                        tempRemove.Add(iconBar.transform.GetChild(i).gameObject);
                    }
                    else
                    {
                        tempRemove.Add(iconBar.transform.GetChild(i).gameObject);
                        lastIconPositon = iconBar.transform.GetChild(i).transform.localPosition;
                        iconPresent[iconBar.transform.GetChild(i).tag] = false;
                        iconRemoved = true;
                    }
                }
            }

            //check if any need to be added afterwards
            foreach (KeyValuePair<string, bool> key in iconPresent)
            {
                if(!key.Value) //icon not present
                {
                    if (CurrentIconCheck(key.Key)) //icon needs to be added
                    {
                        GameObject icon = Instantiate(ReturnPrefabIcon(key.Key), iconBar.transform);
                        icon.transform.localPosition = lastIconPositon;
                        lastIconPositon += new Vector3(60, 0, 0);
                        tempPresentChange.Add(key.Key);
                    }
                }
            }

            // 
            if (tempPresentChange.Count > 0)
            {
                foreach (string iconName in tempPresentChange)
                {
                    iconPresent[iconName] = true;
                }
            }
        }
        else
        {
            foreach (KeyValuePair<string, bool> key in iconPresent)
            {
                if (CurrentIconCheck(key.Key)) //icon needs to be added
                {
                    GameObject icon = Instantiate(ReturnPrefabIcon(key.Key), iconBar.transform);
                    icon.transform.localPosition = lastIconPositon;
                    lastIconPositon += new Vector3(60, 0, 0);
                    tempPresentChange.Add(key.Key);
                }
            }

            if (tempPresentChange.Count > 0)
            {
                foreach (string iconName in tempPresentChange)
                {
                    iconPresent[iconName] = true;
                }
            }
        }

        foreach (GameObject icon in tempRemove)
        {
            Destroy(icon);
        }

        #region Special Case
        if (iconPresent["Poison"])
        { 
            foreach (Transform T in iconBar.transform)
            {
                if (T.tag == "Poison")
                {
                    T.GetChild(0).GetComponent<TextMeshPro>().text = "x " + poisonDropOffDamage;
                }
            }
        }
        #endregion
    }
    public void DefenceRefresh()
    {
        if (classContainsDefence)
        {
            if (defence > 0)
            {
                defenceValue.SetActive(true);
                defenceValue.GetComponent<TextMeshPro>().text = "+" + defence;
            }
            else
            {
                defenceValue.GetComponent<TextMeshPro>().text = "";
                defenceValue.SetActive(false);
            }
        }
    }
    public void AttackThisEnemy(int damageDealt)
    {
        if (defence == 0)
        {
            health -= damageDealt;
        }
        else
        {
            int difference = defence - damageDealt;
            if (difference >= 0)
            {
                defence -= damageDealt;
            }
            else
            {
                defence = 0;
                health -= difference;
            }
        }
    }
    public bool CurrentIconCheck(string iconTag)
    {
        if (iconTag == "Poison")
        {
            if (poisonDropOffDamage > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (iconTag == "Frozen")
        {
            if (frozen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (iconTag == "Stunned")
        {
            if (stunned)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (iconTag == "MarkOfPain")
        {
            if (markOfPain)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (iconTag == "MarkOfFire")
        {
            if (markOfFire)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (iconTag == "MarkOfFireM")
        {
            if (markOfFireM)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public GameObject ReturnPrefabIcon(string iconName)
    {
        if (iconName == "Poison")
        {
            return poisonIcon;
        }
        else if (iconName == "Frozen")
        {
            return frozenIcon;
        }
        else if (iconName == "Stunned")
        {
            return stunnedIcon;
        }
        else if (iconName == "MarkOfPain")
        {
            return markOfPainIcon;
        }
        else if (iconName == "MarkOfFire")
        {
            return markOfFireIcon;
        }
        else if (iconName == "MarkOfFireM")
        {
            return markOfFireMIcon;
        }
        else
        {
            return null;
        }
    }
    public void EnemyTransformGameScene(int numberOfEnemies, int currentPosition)
    {
        if (className == "Rat" || className == "GiantRat" || className == "RatKing")
        {
            RatClass.RatPositioning(numberOfEnemies, currentPosition, thisEnemy);
        }
    }
    public void CardTransformGameScene(int numberOfCards, int currentCardNum, GameObject cardGO)
    {
        if (className == "Rat" || className == "GiantRat" || className == "RatKing")
        {
            RatClass.CardPositioning(numberOfCards, currentCardNum, cardGO);
        }
    }

}
