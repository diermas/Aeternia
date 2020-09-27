using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class CardFunctions : MonoBehaviour
{
    GameController GameControllerRef;
    EnemyCreation EnemyCreationRef;
    Player targetedPlayer;
    drawScript DrawScriptRef;
    public AudioClip backstab;
    public AudioClip flame;
    public AudioClip strike;
    public AudioClip flurry;
    public AudioClip shieldsUp;
    public AudioClip pray;
    public AudioClip blessing;
    public AudioClip explosion;
    AudioSource audiosrc;

    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        GameControllerRef = GetComponent<GameController>();
        EnemyCreationRef = GetComponent<EnemyCreation>();
        DrawScriptRef = GetComponent<drawScript>();
    }

    #region Player Card Funcitons
    public void CardFunc(GameObject card, GameObject targetedGO, Player activePlayer, Enemy targetedEnemy)
    {
        int rage, shadows, divinity;
        rage = activePlayer.rage;
        shadows = activePlayer.shadows;
        divinity = activePlayer.divinity;
        bool divine = (divinity >= 10);
        bool shade = (shadows >= 1);

        switch (int.Parse(card.name))
        {
            //MAGE/////////
            //STARTER//
            //Arcane Bolt
            case 1:
                DealDamage(2, targetedEnemy, "Magical", activePlayer);
                break;
            //Bolt
            case 2:
                DealDamage(1, targetedEnemy, "Magical", activePlayer);
                break;
            //Meditate
            case 3:
                AddResource(activePlayer, 1);
                break;
            //ARCANE
            //Arcane Wave
            case 4:
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 3, "Magical", activePlayer);
                break;
            //Manaburn
            case 5:
                DealDamage(2 * (activePlayer.resourceValue), targetedEnemy, "Magical", activePlayer);
                activePlayer.resourceValue = 0;
                break;
            //Manafount
            case 6:
                AddResource(activePlayer, 3);
                break;
            //Manaflow
            case 7:
                AddResource(activePlayer, 2);
                break;
            //Magic Missile
            case 8:
                DealDamage(2, targetedEnemy, "Magical", activePlayer);
                break;
            //Manasurge
            case 9:
                AddResource(activePlayer, 5);
                break;
            //Seek Spells
            case 10:
                DrawCard(activePlayer, 2);
                break;
            //Spell Surge
            case 11:
                DrawCard(activePlayer, 3);
                break;
            //Arcane Blast
            case 12:
                DealDamage(7, targetedEnemy, "Magical", activePlayer);
                break;
            //Mana Trance
            case 13:
                activePlayer.resourceNewTurnAddition += 1;
                break;
            //PYROMANCY
            //Ignition
            case 14:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamage(3, targetedEnemy, "Fire", activePlayer);
                StartCoroutine(PlayerChoiceDestroyCard(activePlayer, 1, "grave and in hand"));
                DealDamage(2, targetedEnemy, "Fire", activePlayer);
                break;
            //Explosion
            case 15:
                audiosrc.clip = explosion;
                audiosrc.Play();
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 5, "Fire", activePlayer);
                break;
            //Burning Touch
            case 16:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamage(2, targetedEnemy, "Fire", activePlayer);
                targetedEnemy.AddBurn(1);
                break;
            //Emberdraw
            case 17:
                audiosrc.clip = flame;
                audiosrc.Play();
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    e.AddBurn(1);
                }
                DrawCard(activePlayer, 1);
                break;
            //Flame Siphon
            case 18:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamage(4, targetedEnemy, "Fire", activePlayer);
                AddResource(activePlayer, 2);
                break;
            //Exhaust
            case 19:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamage(3, targetedEnemy, "Fire", activePlayer);
                StartCoroutine(PlayerChoiceDestroyCard(activePlayer, 1, "grave and in hand"));
                targetedEnemy.AddBurn(2);
                break;
            //Flame Blast
            case 20:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamage(10, targetedEnemy, "Fire", activePlayer);
                break;
            //Heat Shock
            case 21:
                DrawCard(activePlayer, 2);
                StartCoroutine(PlayerChoiceDestroyCard(activePlayer, 1, "grave and in hand"));
                DrawCard(activePlayer, 2);
                break;
            //Flamewave
            case 22:
                audiosrc.clip = flame;
                audiosrc.Play();
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Fire", activePlayer);
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    e.AddBurn(1);
                }
                break;
            //Soulfire
            case 23:
                audiosrc.clip = flame;
                audiosrc.Play();
                AddResource(activePlayer, 2);
                StartCoroutine(PlayerChoiceDestroyCard(activePlayer, 1, "grave and in hand"));
                AddResource(activePlayer, 2);
                break;
            //CRYOMANCY
            //Shatter Ice
            case 24:
                ConsumeAllFrostDealDamage(activePlayer, targetedEnemy, 2, "Ice");
                break;
            //Powder Snow
            case 25:
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Ice", activePlayer);
                break;
            //Freeze
            case 26:
                targetedEnemy.stunned = true;
                //Animation
                targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Stun!";
                targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                break;
            //Frostbite
            case 27:
                DealDamage(1, targetedEnemy, "Ice", activePlayer);
                targetedEnemy.AddFrost(1);
                break;
            //Iceheart
            case 28:
                DealDamage(5, targetedEnemy, "Ice", activePlayer);
                break;
            //Cold Snap
            case 29:
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    e.AddFrost(1);
                }
                break;
            //Ice Shards
            case 30:
                DealDamageToRandomTargets(1, GameControllerRef.listOfEnemies, 2, activePlayer);
                break;
            //Ice Shell
            case 31:
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    e.frostTargetablePlayer.Add(activePlayer.className, 1);
                }
                break;
            //Quick Thaw
            case 32:
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    if (e.frost > 0)
                    {
                        DealDamage(5, targetedEnemy, "Ice", activePlayer);
                        e.frost = 0;
                    }
                }
                break;
            //Snowmelt
            case 33:
                ConsumeAllFrostGainMana(activePlayer, targetedEnemy, 1);
                break;


            //ROGUE/////////
            //STARTER//
            //Flurry
            case 200:
                audiosrc.clip = flurry;
                audiosrc.Play();
                DealDamageToRandomTargets(1, GameControllerRef.listOfEnemies, 2, activePlayer);
                break;
            //Strike
            case 201:
                audiosrc.clip = strike;
                audiosrc.Play();
                DealDamage(1, targetedEnemy, "Physical", activePlayer);
                break;
            //Recover
            case 202:
                AddResource(activePlayer, 1);
                break;
            //SCOUT
            //Critical Focus
            case 203:
                AddResource(activePlayer, 2);
                break;
            //Backstab
            case 204:
                audiosrc.clip = backstab;
                audiosrc.Play();
                DealDamage(3, targetedEnemy, "Physical", activePlayer);
                break;
            //Poison Blade
            case 205:
                PoisonDamageInfliciton(2, targetedEnemy); // (NT)
                break;
            //Expose Weakness
            case 206:
                DamageBuff(1, "Physical", activePlayer);
                break;
            //Sleight of Hand
            case 207:
                DrawCard(activePlayer, 2);
                break;
            //Swift Strikes
            case 208:
                audiosrc.clip = strike;
                audiosrc.Play();
                DealDamage(6, targetedEnemy, "Physical", activePlayer);
                break;
            //Absolute Focus
            case 209:
                AddResource(activePlayer, 3);
                break;
            //Quickstep
            case 210:
                DealDamage(2, targetedEnemy, "Physical", activePlayer);
                DrawCard(activePlayer, 1);
                break;
            //Smoke Bomb
            case 211:
                StunEnemy(targetedEnemy);
                break;
            //Fan of Knives
            case 212:
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Physical", activePlayer);
                break;  
            //POISON MASTER
            //Toxic Dart
            case 213:
                PoisonDamageInfliciton(2, targetedEnemy);
                break;
            //Miasma
            case 214:
                PoisonAllEnemies(2);
                break;
            //Poisonous Burn
            case 215:
                DealDamage(targetedEnemy.poisonDropOffDamage, targetedEnemy, "Physical", activePlayer);
                break;
            //Poison Draw
            case 216:
                PoisonDamageInfliciton(2, targetedEnemy);
                DrawCard(activePlayer, 1);
                break;
            //Poison Focus
            case 217:
                PoisonDamageInfliciton(3, targetedEnemy);
                AddResource(activePlayer, 3);
                break;
            //Toxic Sting
            case 218:
                PoisonDamageInfliciton(2, targetedEnemy);
                DrawCard(activePlayer, 2);
                break;
            //Well of Venom
            case 219:
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    PoisonEnemyEachRound(e, 1);
                }
                break;
            //Catalyst
            case 220:
                DealDamage(targetedEnemy.poisonDropOffDamage*2, targetedEnemy, "Physical", activePlayer);
                targetedEnemy.poisonDropOffDamage = 0;
                break;
            //Venom Tip
            case 221:
                DealDamage(1, targetedEnemy, "Physical", activePlayer);
                PoisonDamageInfliciton(1, targetedEnemy);
                break;
            //Venom Drench
            case 222:
                PoisonDamageInfliciton(targetedEnemy.poisonDropOffDamage*2, targetedEnemy);
                break;
            //SHADOWBLADE
            //Mark of Pain
            case 223:
                MarkEnemy(targetedEnemy, 3, "Physical");
                break;
            //Shadow Strike
            case 224:
                if (shade)
                {
                    DealDamage(9, targetedEnemy, "Physical", activePlayer);
                    activePlayer.AddShadows(-1);
                } else
                {
                    DealDamage(3, targetedEnemy, "Physical", activePlayer);
                }
                break;
            //Mark of Fire
            case 225:
                targetedEnemy.markOfFireM = true;
                targetedEnemy.markOfFireMDamage = 2;
                break;
            //Shadow Draw
            case 226:
                activePlayer.AddShadows(1);
                DrawCard(activePlayer, 1);
                break;
            //Deadly Flourish
            case 227:
                if (shade)
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 4, "Physical", activePlayer);
                    activePlayer.AddShadows(-1);
                } else
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 3, "Physical", activePlayer);
                }
                break;
            //Execution
            case 228:
                DealDamage(10, targetedEnemy, "Physical", activePlayer);
                break;
            //Preparation
            case 229:
                AddResource(activePlayer, 5);
                break;
            //Mark of Shadow
            case 230:

                break;
            //Shadeswarm
            case 231:
                activePlayer.AddShadows(3);
                break;
            //Shadow's End
            case 232:
                if (shade)
                {
                    DealDamage(7 + (4 * shadows), targetedEnemy, "Physical", activePlayer);
                    activePlayer.shadows = 0;
                } else
                {
                    DealDamage(7, targetedEnemy, "Physical", activePlayer);
                }
                break;




            //WARRIOR/////////
            //STARTER//
            //Provoke
            case 400:
                AddToTauntList(activePlayer, targetedEnemy);
                break;
            //Charge
            case 401:
                DealDamage(1, targetedEnemy, "Physical", activePlayer);
                break;
            //Rage
            case 402:
                AddResource(activePlayer, 1);
                break;
            //WARFARE
            //Shields Up
            case 403:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                AddDefence(activePlayer, 3);
                break;
            //Brute Force
            case 404:
                DealDamage(5, targetedEnemy, "Physical", activePlayer);
                break;
            //Reinforcements
            case 405:
                AddResource(activePlayer, 3);
                break;
            //War-Torn
            case 406:
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Physical", activePlayer);
                break;
            //Blood Brothers
            case 407:
                AddResource(activePlayer, 2);
                break;
            //Stalwart
            case 408:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                AddDefence(activePlayer, 5);
                break;
            //Morality
            case 409:

                break;
            //Challenging Shout
            case 410:
                TauntAllEnemies(activePlayer, GameControllerRef.listOfEnemies);
                break;
            //Rush Ahead    
            case 411:
                DrawCard(activePlayer, 2);
                break;
            //Face Me
            case 412:
                AddToTauntList(activePlayer, targetedEnemy);
                break;
            //BERSERKER
            //Axe Throw
            case 413:
                DealDamage(2, targetedEnemy, "Physical", activePlayer);
                AddToTauntList(activePlayer, targetedEnemy);
                break;
            //Enrage
            case 414:
                activePlayer.LoseHealth(3);
                activePlayer.AddRage(3);
                break;
            //Seethe
            case 415:
                DrawCard(activePlayer, 2);
                if (rage >= 3)
                {
                    AddResource(activePlayer, 3);
                    activePlayer.AddRage(-3);
                }
                break;
            //Heavy Swing
            case 416:
                if (rage >= 10)
                {
                    DealDamage(16, targetedEnemy, "Physical", activePlayer);
                    activePlayer.AddRage(-10);
                } else
                {
                    DealDamage(8, targetedEnemy, "Physical", activePlayer);
                }
                break;
            //Sweeping Blade
            case 417:
                DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 1, "Physical", activePlayer);
                TauntAllEnemies(activePlayer, GameControllerRef.listOfEnemies);
                break;
            //Slam
            case 418:
                if (rage >= 2)
                {
                    DealDamage(5, targetedEnemy, "Physical", activePlayer);
                    activePlayer.AddRage(-2);
                } else
                {
                    DealDamage(3, targetedEnemy, "Physical", activePlayer);
                }
                break;
            //Reckless Charge
            case 419:
                DealDamage(3, targetedEnemy, "Physical", activePlayer);
                activePlayer.AddRage(2);
                break;

            //Pummel
            case 420:
                if (rage >= 1)
                {
                    DealDamage(4 + (2 * rage), targetedEnemy, "Physical", activePlayer);
                    activePlayer.rage = 0;
                } else
                {
                    DealDamage(4, targetedEnemy, "Physical", activePlayer);
                }
                break;
            //Follow Through
            case 421:
                DealDamage(4, targetedEnemy, "Physical", activePlayer);
                if (rage >= 3)
                {
                    DrawCard(activePlayer, 1);
                    activePlayer.AddRage(-3);
                }
                break;
            //Edge of Madness
            case 422:

                break;
            //DEFENDER
            //Stand the Line
            case 423:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                TauntAllEnemies(activePlayer, GameControllerRef.listOfEnemies);
                AddDefence(activePlayer, 5);
                break;
            //Combat Lock
            case 424:

                break;
            //Triage
            case 425:
                HealDamage(activePlayer, 6);
                break;
            //Guard
            case 426:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                AddResource(activePlayer, 3);
                AddDefence(activePlayer, 3);
                break;
            //Shield Slam
            case 427:
                DealDamage(activePlayer.defence, targetedEnemy, "Physical", activePlayer);
                break;
            //Advance
            case 428:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                AddDefence(activePlayer, 3);
                DrawCard(activePlayer, 1);
                break;
            //Bandages
            case 429:
                HealDamage(activePlayer, 1);
                AddDefence(activePlayer, 2);
                break;
            //Endure
            case 430:

                break;
            //Shield Form
            case 431:

                break;
            //Last Stand
            case 432:
                audiosrc.clip = shieldsUp;
                audiosrc.Play();
                AddDefence(activePlayer, 15);
                break;




            //PRIEST/////////
            //STARTER//
            //Staff Bash
            case 600:
                DealDamage(1, targetedEnemy, "Physical", activePlayer);
                break;
            //Pray
            case 601:
                audiosrc.clip = pray;
                audiosrc.Play();
                AddResource(activePlayer, 1);
                break;
            //Healing Hands
            case 602:
                HealDamage(targetedGO.GetComponent<OnPlayerScript>().playerRef, 1);
                break;
            //DEVOUT
            //Sword of Light
            case 603:
                if (divine)
                {
                    DealDamage(2*2, targetedEnemy, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamage(2, targetedEnemy, "Holy", activePlayer);
                }
                break;
            //Rosary
            case 604:
                AddResource(activePlayer, 1);
                DrawCard(activePlayer, 1);
                break;
            //Blessing
            case 605:
                AddResource(activePlayer, 2);
                break;
            //God's Blessing
            case 606:
                audiosrc.clip = blessing;
                audiosrc.Play();
                HealDamage(targetedGO.GetComponent<OnPlayerScript>().playerRef, 5);
                break;
            //Banish
            case 607:
                StunEnemy(targetedEnemy);
                break;
            //Blind Faith
            case 608:
                AddResource(activePlayer, 3);
                break;
            //Mantra
            case 609:
                DrawCard(activePlayer, 2);
                break;
            //Lesser Heal
            case 610:
                HealDamage(targetedGO.GetComponent<OnPlayerScript>().playerRef, 2);
                break;
            //Raise
            case 611:

                break;
            //Godwave
            case 612:
                if (divine)
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2*2, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Holy", activePlayer);
                }
                break;
            //DIVINE WARFARE
            //Holy Storm
            case 613:
                if (divine)
                {
                    DealDamage(4*2, targetedEnemy, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamage(4, targetedEnemy, "Holy", activePlayer);
                }
                activePlayer.AddDivinity(1);
                break;
            //Holy Blast
            case 614:
                if (divine)
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2*2, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 2, "Holy", activePlayer);
                }
                activePlayer.AddDivinity(2);
                break;
            //Divine Right
            case 615:
                AddResource(activePlayer, 4);
                break;

            //Ascension
            case 616:

                break;
            //Veil of Light
            case 617:

                break;
            //Sacrifice
            case 618:

                break;
            //Smite
            case 619:
                if (divine)
                {
                    DealDamage(10*2, targetedEnemy, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamage(10, targetedEnemy, "Holy", activePlayer);
                }
                break;
            //Invocation
            case 620:
                activePlayer.AddDivinity(3);
                break;

            //Blessed Hand 
            case 621:
                DrawCard(activePlayer, 2);
                activePlayer.AddDivinity(1);
                break;
            //Judgement Wave
            case 622:
                if (divine)
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 5*2, "Holy", activePlayer);
                    activePlayer.AddDivinity(-10);
                } else
                {
                    DealDamageToAllEnemies(GameControllerRef.listOfEnemies, 5, "Holy", activePlayer);
                }
                break;
            //HOLY WARDEN
            //Resurrection
            case 623:

                break;
            //Heal Pulse
            case 624:
                HealAllAllies(3);
                break;
            //Miracle
            case 625:
                AddResource(activePlayer, 3);
                DrawCard(activePlayer, 1);
                break;
            //Charity
            case 626:

                break;
            //Cure Poison
            case 627:
                ReducePoison(targetedPlayer, 3);
                break;
            //God's Touch
            case 628:
                HealDamage(targetedPlayer, 10);
                break;
            //Cleansing Light
            case 629:

                break;
            //Scriptures
            case 630:
                DrawCard(activePlayer, 1);
                // TARGET DRAWS 1 CARD
                break;
            //Regeneration
            case 631:

                break;
            //Barrier of Light
            case 632:
                AddDefence(targetedPlayer, 6);
                break;
        }

        HealthRefresh(GameControllerRef.listOfEnemies, GameControllerRef.listOfPlayers);
    }

    private void AddResource(Player p, int resourceIncrease)
    {
        p.resourceValue += resourceIncrease;

        //Animation

        p.resourceUI.transform.GetChild(0).GetComponent<Text>().text = "+" + resourceIncrease.ToString();
        p.resourceUI.GetComponent<Animator>().SetTrigger("resourceChange");
    }

    private void DealDamage(int damageDealt, Enemy enemy, string damageType, Player activePlayer)
    {
        bool resisted = false;
        string resistedS = "";
        int actualDamageDealt = Convert.ToInt32(Math.Ceiling(damageDealt * enemy.resistanceDamageTypes[damageType]) );

        if (actualDamageDealt < damageDealt)
        {
            resistedS = "Resisted";
            resisted = true;
        }
        else if (actualDamageDealt > damageDealt)
        {
            resistedS = "Weakened";
            resisted = true;
        }
        #region Fire Attacks
        if (damageType == "Fire")
        {
            if (enemy.burn > 0)
            {
                actualDamageDealt = Convert.ToInt32(Math.Ceiling(actualDamageDealt + (actualDamageDealt * (enemy.burn / 4.0f))));
                enemy.AttackThisEnemy(actualDamageDealt);
            }
            else
            {
                enemy.AttackThisEnemy(actualDamageDealt);
            }
        }
        else
        {
            enemy.AttackThisEnemy(actualDamageDealt);
        }
        #endregion

        //Applies Damage Buffs
        if (activePlayer.BuffList.Count > 0)
        {
            foreach (Buffs b in activePlayer.BuffList)
            {
                foreach (KeyValuePair<string, int> dmbDictionary in b.damageBuffs)
                {
                    if (dmbDictionary.Key == damageType)
                    {
                        enemy.AttackThisEnemy(b.damageBuffs[damageType]);
                        actualDamageDealt += b.damageBuffs[damageType];
                    }
                }
            }
        }

        if (!resisted)
        {
            enemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + actualDamageDealt.ToString() + "\n" + damageType + " damage";
            enemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
        else
        {
            enemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + actualDamageDealt.ToString() + "\n" + damageType + " damage "+resistedS + "!";
            enemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
        
    }

    private void DealDamageToAllEnemies(List<Enemy> eList, int damageDealt, string damageType, Player p)
    {
        foreach (Enemy e in eList)
        {
            DealDamage(damageDealt, e, damageType, p);
        }
    }

    private void DealDamageToRandomTargets(int damageDealt, List<Enemy> listOE, int numberOfTargets, Player activePlayer)
    {
        if (listOE.Count <= numberOfTargets)
        {
            foreach (Enemy e in listOE)
            {
                DealDamage(damageDealt, e, "Physical", activePlayer);
            }
        }
        else
        {
            List<Enemy> tempRandList = new List<Enemy>();
            tempRandList.AddRange(listOE);
            int count = tempRandList.Count;
            int last = count - 1;

            for (int i = 0; i < last; i++)
            {
                int r = UnityEngine.Random.Range(i, count);
                Enemy tmp = tempRandList[i];
                tempRandList[i] = tempRandList[r];
                tempRandList[r] = tmp;
            }
            int j = 0;
            foreach (Enemy e in tempRandList)
            {
                if (j < numberOfTargets)
                {
                    DealDamage(damageDealt, e, "Physical", activePlayer);
                    j++;
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void HealDamage(Player p, int healValue)
    {
        int healthDisplay = 0;
        if (p.health + healValue >= p.maxHealth)
        {
            healthDisplay = p.maxHealth - p.health;
            p.health = p.maxHealth;
        }
        else
        {
            healthDisplay = healValue;
            p.health += healValue;
        }
        //Animation
        p.thisPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text = "+ " + healthDisplay + " HP";
        p.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage1");
    }

    private void HealAllAllies(int healValue)
    {
        foreach (Player p in GameControllerRef.listOfPlayers)
        {
            HealDamage(p, healValue);
        }
    }

    private void TauntAllEnemies(Player p, List<Enemy> eList)
    {
        foreach (Enemy e in eList)
        {
            e.tauntList.Add(p);
        }

        foreach (Enemy e in eList)
        {
            e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Taunted";
            e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
        
    }

    private void AddDefence(Player p, int defenceAdd)
    {
        p.defence += defenceAdd;
        p.thisPlayer.transform.GetChild(4).GetComponent<TextMeshPro>().text = "+ " + defenceAdd + " DF";
        p.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage2");
    }

    private void AddToTauntList(Player p, Enemy targetedEnemy)
    {
        targetedEnemy.tauntList.Add(p);

        //anim
        targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Taunted";
        targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
    }

    private void DiscardCards(Player p, int discardAmount)
    {
        DrawScriptRef.discardValue = discardAmount;
        DrawScriptRef.DiscardCardScreenDisplay(p);
    }

    private void DrawCard(Player p, int drawNum)
    {
        for (int i = 0; i < drawNum; i++)
        {
            if (p.playableDeck.Count == 0)
            {
                DrawScriptRef.randomiseCards(ref p);
            }
            p.currentCardsInHand.Add(p.playableDeck[0]);
            p.playableDeck.RemoveAt(0);
        }
        DrawScriptRef.CardLookup(p);
    }

    private void NotTargetableNextTurn(Player p)
    {
        if (p.nextTargetable <= GameControllerRef.roundValue)
        {
            p.targetable = false;
            p.nextTargetable = (GameControllerRef.roundValue + 1);
        }
    }

    private void PoisonDamageInfliciton(int numberOfPoison, Enemy targetedEnemy)
    {
        if (targetedEnemy.poisonDropOffDamage == 0)
        {
            targetedEnemy.thisEnemy.transform.GetChild(2).gameObject.SetActive(true);
        }
            //Clamps the posion stack to 10;
        if ((numberOfPoison + targetedEnemy.poisonDropOffDamage) > 10)
        {
            targetedEnemy.poisonDropOffDamage = 10;
        }
        else
        {
            targetedEnemy.poisonDropOffDamage += numberOfPoison;
        }
         
        //Animation
        targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Poisoned";
        targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
    }

    private void PoisonAllEnemies(int numberofPoison)
    {
        foreach (Enemy e in GameControllerRef.listOfEnemies)
        {
            PoisonDamageInfliciton(numberofPoison, e);
        }
    }

    private void DamageBuff(int damageBuffIncrease, string damageType, Player p)
    {
        Buffs tempBuffs = new Buffs();
        tempBuffs.damageBuffs.Add(damageType, damageBuffIncrease);
        p.BuffList.Add(tempBuffs);
    }

    private void EnemyDamageDropoff(int dropOffVal, Enemy targetedEnemy)
    {
        targetedEnemy.damageDropOff += dropOffVal;
    }

    private IEnumerator PlayerChoiceDestroyCard(Player p, int destoryAmount, string decksSelected)
    {
        yield return new WaitForSeconds(1.0f);

        DrawScriptRef.destroyValue = destoryAmount;
        DrawScriptRef.DestroyCardScreenDisplay(p, decksSelected);
    }
   
    private void ConsumeAllFrostDealDamage(Player p, Enemy targetedEnemy, int damageDealt, string damageType)
    {
        if (targetedEnemy.frost > 0)
        {
            int dam = damageDealt * targetedEnemy.frost;
            DealDamage(dam, targetedEnemy, damageType, p);
            targetedEnemy.UnFreeze();
            //Animation
            targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Frost Damage: -" + dam.ToString();
            targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
        else
        {
            //Animation
            targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "No Frost";
            targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
    }

    private void ConsumeAllFrostGainMana(Player p, Enemy targetedEnemy, int manaGain)
    {
        if (targetedEnemy.frost > 0)
        {
            int resourceIncrease = manaGain * targetedEnemy.frost;
            p.resourceValue += resourceIncrease;

            targetedEnemy.UnFreeze();

            p.resourceUI.transform.GetChild(0).GetComponent<Text>().text = "+" + resourceIncrease.ToString();
            p.resourceUI.GetComponent<Animator>().SetTrigger("resourceChange");
        }

        else
        {
            //Animation
            targetedEnemy.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "No Frost!";
            targetedEnemy.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
        }
    }

    private void StunEnemy(Enemy e)
    {
        e.stunned = true;
    }

    private void PoisonEnemyEachRound(Enemy e, int poisonGained)
    {
        e.poisonGain += poisonGained;
    }
   
    private void MarkEnemy(Enemy e, int markDamage, string markDamageType)
    {
        e.markOfPain = true;
        e.markOfPainDamage = markDamage;
        e.markOfPainDamageType = markDamageType;
    }

    private void ReducePoison(Player p, int poisonCleanse)
    {
        p.poisonCount -= 3;
        if (p.poisonCount < 0)
        {
            p.poisonCount = 0;
        }
    }
    private void InstantPlayerHealthUpdate(Player p)
    {
        if(p.health < 0)
        {
            p.healthUI.text = "0";
        }
        else
        {
            p.healthUI.text = p.health.ToString();
        }
    }

    #endregion

    #region Enemy Card Functions

    public void EnemyCardFunc(int cardNum, List<Player> allTargetablePlayers, Enemy currentEnemy, ref float timeNeeded)
    {
        switch (cardNum)
        {
            //SLIME////////
            //BIG SLIME//
            //Heavy Smash
            case 2001:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy); //gets and sets the Player being targeted
                EnemyDealDamage(targetedPlayer, 10, currentEnemy);
                targetedPlayer = null;
                break;

            //Heavy Bash
            case 2002:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy); //gets and sets the Player being targeted
                EnemyDealDamage(targetedPlayer, 4, currentEnemy);
                targetedPlayer = null;
                break;

            //Daze
            case 2003:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy); //gets and sets the Player being targeted
                targetedPlayer.stunValue += 1;
                targetedPlayer.thisPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text = "Stun!";
                targetedPlayer.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage1");
                Debug.Log(targetedPlayer.className);
                break;

            //Split
            case 2004:
                EnemyCreationRef.CreateEnemy(currentEnemy, "MediumSlime");
                break;

            //Smash
            case 2005:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy); //gets and sets the Player being targeted
                EnemyDealDamage(targetedPlayer, 7, currentEnemy);
                break;

            //Bash
            case 2006:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy); //gets and sets the Player being targeted
                EnemyDealDamage(targetedPlayer, 3, currentEnemy);
                break;

            //Split
            case 2007:
                EnemyCreationRef.CreateEnemy(currentEnemy, "SmallSlime");
                break;

            //Grow
            case 2008:
                EnemyHeal(4, currentEnemy);
                break;

            //Light Smash
            case 2009:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy);
                EnemyDealDamage(targetedPlayer, 4, currentEnemy);
                break;

            //Light Bash
            case 2010:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy);
                EnemyDealDamage(targetedPlayer, 2, currentEnemy);
                break;

            //Grow
            case 2011:
                EnemyHeal(3, currentEnemy);
                break;

            //RAT KING///

            //Summon Swarm
            case 2101:
                int animationState;
                int rand = UnityEngine.Random.Range(2, 4);
                int dif = 0;
                if (GameControllerRef.listOfEnemies.Count < RatClass.maxNumberOfRats)
                {
                    if (GameControllerRef.listOfEnemies.Count + rand <= 8)
                    {
                        for (int i = 0; i < rand; i++)
                        {
                            EnemyCreationRef.CreateEnemy(currentEnemy, "Rat");
                        }
                        animationState = 0;
                    }
                    else
                    {
                        dif = RatClass.maxNumberOfRats - GameControllerRef.listOfEnemies.Count;
                        for (int i = 0; i < dif; i++)
                        {
                            EnemyCreationRef.CreateEnemy(currentEnemy, "Rat");
                        }
                        animationState = 1;
                    }
                }
                else
                {
                    animationState = 2;
                }
                #region Animation States
                if (animationState == 0)
                {
                    currentEnemy.thisEnemy.transform.GetChild(1).GetComponent<TextMeshPro>().text = "+" + rand + " Rats";
                    currentEnemy.thisEnemy.GetComponent<Animator>().SetTrigger("healthDisplay");
                }
                else if (animationState == 1)
                {
                    currentEnemy.thisEnemy.transform.GetChild(1).GetComponent<TextMeshPro>().text = "+" + dif + " Rats";
                    currentEnemy.thisEnemy.GetComponent<Animator>().SetTrigger("healthDisplay");
                }
                else if (animationState == 2)
                {
                    currentEnemy.thisEnemy.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Max Rats!";
                    currentEnemy.thisEnemy.GetComponent<Animator>().SetTrigger("healthDisplay");
                }
                #endregion
                break;

            case 2102:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy);
                EnemyDealDamage(targetedPlayer, 5, currentEnemy);
                break;

            case 2103:
                EnemyGainDefence(10, currentEnemy);
                break;

            case 2104:
                EnemyHeal(5, currentEnemy);
                break;

            case 2105:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy);
                StartCoroutine(PoisonAndDamagePlayer(targetedPlayer, 2, 4, currentEnemy));
                timeNeeded = 2.0f;
                break;

            case 2106:
                StartCoroutine(DealDamageToAllPlayers(allTargetablePlayers, 3, currentEnemy));
                timeNeeded = GameControllerRef.listOfPlayers.Count;
                break;

            case 2107:
                foreach (Enemy e in GameControllerRef.listOfEnemies)
                {
                    if (e.EnemyID != currentEnemy.EnemyID)
                    {
                        e.AddDefence(3);
                    }
                }
                break;

            case 2108:
                targetedPlayer = PlayerTargetValue(allTargetablePlayers, currentEnemy);
                EnemyDealDamage(targetedPlayer, 2, currentEnemy);
                break;
        }

        HealthRefresh(GameControllerRef.listOfEnemies, allTargetablePlayers);
    }

    private Player PlayerTargetValue(List<Player> allTargetablePlayers, Enemy currentEnemy)
    {
        Player p;
        if (currentEnemy.tauntList.Count == 0)
        {
            p = RandomPlayerTarget(allTargetablePlayers);
        }
        else
        {
            p = currentEnemy.tauntList[0];
            currentEnemy.tauntList.RemoveAt(0);
        }

        return p;
    }

    private Player RandomPlayerTarget(List<Player> listOfPlayers)
    {
        List<Player> temp1 = new List<Player>();
        List<Player> temp2 = new List<Player>();
        // copy of deck into temp1 // 
        foreach (Player p in listOfPlayers)
        {
            if (p.targetable == true)
            {
                temp1.Add(p);
            }
        }

        if (temp1.Count > 0)
        {
            while (temp2.Count != listOfPlayers.Count)
            {
                int rnd = UnityEngine.Random.Range(0, temp1.Count);
                temp2.Add(temp1[rnd]);
                temp1.RemoveAt(rnd);
            }
            return temp2[0];
        }
        return null;
    }

    private void EnemyDealDamage(Player playerTarget, int damage, Enemy currentEnemy)
    {
        int overallDamage = 0;
        //Managing for Damage Drop Offs
        if (currentEnemy.damageDropOff == 0)
        {
            //Managing for Defence
            if (playerTarget.defence > 0)
            {
                int difference = playerTarget.defence - damage;
                if (difference == 0)
                {
                    playerTarget.defence = 0;
                }
                else if (difference > 0)
                {
                    playerTarget.defence = difference;
                }
                else if (difference < 0)
                {
                    playerTarget.defence = 0;
                    playerTarget.health += difference;
                }
                overallDamage = difference;
            }
            else
            {
                playerTarget.health -= damage;
                overallDamage = damage;
            }

        }
        else if (damage < currentEnemy.damageDropOff)
        {
            currentEnemy.damageDropOff -= damage;
        }
        else if (damage == currentEnemy.damageDropOff)
        {
            overallDamage = damage;
            currentEnemy.damageDropOff = 0;
        }
        else if (damage > currentEnemy.damageDropOff)
        {
            damage -= currentEnemy.damageDropOff;
            currentEnemy.damageDropOff = 0;

            //Managing for Defence
            if (playerTarget.defence > 0)
            {
                int difference = playerTarget.defence - damage;
                if (difference == 0)
                {
                    playerTarget.defence = 0;
                }
                else if (difference > 0)
                {
                    playerTarget.defence = difference;
                }
                else if (difference < 0)
                {
                    playerTarget.defence = 0;
                    playerTarget.health += difference;
                }
                overallDamage = difference;
            }
        }

        if (currentEnemy.frostTargetablePlayer.ContainsKey(playerTarget.className))
        {
            currentEnemy.AddFrost(currentEnemy.frostTargetablePlayer[playerTarget.className]);
        }

        //animate damage
        if (playerTarget.thisPlayer.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DamageTaken"))
        {
            playerTarget.thisPlayer.transform.GetChild(4).GetComponent<TextMeshPro>().text = "- " + overallDamage.ToString();
            playerTarget.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage2");
        }
        else
        {
            playerTarget.thisPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text = "- " + overallDamage.ToString();
            playerTarget.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage1");
        }

        InstantPlayerHealthUpdate(playerTarget);
    }

    IEnumerator DealDamageToAllPlayers(List<Player> listOfPlayers, int damageToDeal, Enemy currentEnemy)
    {
        for (int i = 0; i< listOfPlayers.Count; i++)
        {
            EnemyDealDamage(listOfPlayers[i], damageToDeal, currentEnemy);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void EnemyHeal(int healVal, Enemy currentEnemy)
    {
        int addedHealth = 0;
        if ((healVal + currentEnemy.health) >= currentEnemy.maxHealth)
        {
            addedHealth = currentEnemy.maxHealth - currentEnemy.health;
            currentEnemy.health = currentEnemy.maxHealth;
        }
        else
        {
            addedHealth = healVal;
            currentEnemy.health += healVal;
        }

        //animation
        currentEnemy.thisEnemy.transform.GetChild(1).GetComponent<TextMeshPro>().text = "+ " + addedHealth.ToString() + " HP";
        currentEnemy.thisEnemy.GetComponent<Animator>().SetTrigger("healthDisplay");

    }

    private void EnemyGainDefence(int defVal, Enemy CurrentEnemy)
    {
        if ((CurrentEnemy.defence + defVal) > CurrentEnemy.maxDefence)
        {
            CurrentEnemy.defence = CurrentEnemy.maxDefence;
        }
        else
        {
            CurrentEnemy.defence += defVal;
        }
    }

    private IEnumerator PoisonAndDamagePlayer(Player p, int poisonDamage, int physicalDamage, Enemy currentEnemy)
    {
        EnemyDealDamage(p, 4, currentEnemy);

        yield return new WaitForSeconds(1.0f);

        targetedPlayer.AddPoison(poisonDamage);
        targetedPlayer.thisPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text ="+" + poisonDamage + " Poison!";
        targetedPlayer.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage1");
    }
    #endregion

    #region For Player and Enemy
    public void HealthRefresh(List<Enemy> eList, List<Player> pList)
    {
        #region Check for End State
        List<Enemy> tempAliveEnemies = new List<Enemy>();
        foreach (Enemy e in eList)
        {
            if (e.health <= 0)
            {
                Destroy(e.thisEnemy);
            }
            else
            {
                tempAliveEnemies.Add(e);
                #region Spawn Icons
                //Poison Icon
                if (e.lastHealth > e.health)
                {
                    e.lastHealth = e.health;
                    StartCoroutine(e.MarkCheck(eList));
                }
                e.lastHealth = e.health;
                #endregion
            }
        }
        eList.Clear();
        if (tempAliveEnemies.Count > 0)
        {
            eList.AddRange(tempAliveEnemies);
            eList.OrderByDescending(x=>x.thisEnemy.transform.localPosition.x);
        }
        //If all Enemies are dead//
        else
        {
            GameControllerRef.MainUI.SetActive(false);
            GameControllerRef.winUI.SetActive(true);
        }
        #endregion

        #region Update HealthUI
        foreach (Enemy e in eList)
        {
            e.HealthBarRefresh();
            e.IconBarRefresh();
            e.DefenceRefresh();
            if (e.markOfFireMCheckInitiated)
            {
                HealthRefresh(eList, pList);
            }
        }
        #endregion

        #region For Players
        List<Player> tempAlivePlayers = new List<Player>();

        foreach (Player p in pList)
        {
            if (p.health <= 0)
            {
                p.health = 0;
                GameControllerRef.listOfDeadPlayers.Add(p);
                p.healthUI.text = "Out";
            }

            else
            {
                tempAlivePlayers.Add(p);

                p.healthUI.text = p.health.ToString();
                if (p.defence > 0)
                {
                    p.defenceUI.text = p.defence.ToString();
                }
                else if (p.defence == 0)
                {
                    p.defenceUI.text = "";
                }
            }
        }

        pList.Clear();
        #endregion

        #region For Enemies
        if (tempAlivePlayers.Count > 0)
        {
            pList.AddRange(tempAlivePlayers);
        }
        else
        {
            GameControllerRef.gameOverUI.SetActive(true);
            GameControllerRef.MainUI.SetActive(false);
        }

        tempAlivePlayers.Clear();

        GameControllerRef.UpdateEnemyTransforms();
        #endregion
    }

    #endregion

}


