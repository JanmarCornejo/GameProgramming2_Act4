using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    GameManager gm;
    [SerializeField] AudioClip Click;

    public void MaineMenuOpt()
    {
        SoundManager.instance.PlaySound(Click);
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        gm.Restart();
    }
}
