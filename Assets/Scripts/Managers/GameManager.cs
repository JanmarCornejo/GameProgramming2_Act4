using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject CompleteLevelUI;
    public GameObject FailLevelUI;

    public void GameComplete()
    {
        CompleteLevelUI.SetActive(true);
        Debug.Log("Game Complete");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            FailLevelUI.SetActive(true);
            gameHasEnded = true;
            Debug.Log("Game Over");           
        }
       
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
