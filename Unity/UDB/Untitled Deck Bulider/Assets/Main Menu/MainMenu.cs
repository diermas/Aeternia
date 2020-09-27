using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI, NewGameUI;
    

    PlayerCreation PlayerCreationRef;
    Dictionary<int,Quests> QuestLog;
    void Start()
    {
        PlayerCreationRef = FindObjectOfType<PlayerCreation>();
        QuestLog = new Dictionary<int, Quests>();
    }
    public void NewGame()
    {
        //MainMenuUI.SetActive(false);
       // NewGameUI.SetActive(true);
        PlayerCreationRef.CreatePlayer();
        SceneManager.LoadScene("Overworld");
    }

    public void LoadGame()
    {

    }

    public void QuitGame()
    {

    }

    public void NewGameEnter(string inputField)
    { 
        
    }

    public void ReturnToMenu()
    {
        MainMenuUI.SetActive(true);
        NewGameUI.SetActive(false);
    }

    public void SavePlayer()
    { 
        
    }
}
