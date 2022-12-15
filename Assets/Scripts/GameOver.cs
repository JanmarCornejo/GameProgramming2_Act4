using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameManager gm;
    [SerializeField] AudioClip MenuClick;

    public void MaineMenuOpt()
    {
        SoundManager.Instance.PlaySound(MenuClick);

        Debug.Log("Loading Menu");
        //SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        gm.Restart();
    }
}
