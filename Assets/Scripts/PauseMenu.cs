using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static event Action<SaveData> OnLoad;
    
    public static bool IsPaused = false;
    public GameObject pauseMenu;

    [SerializeField] private Button _saveBtn;
    [SerializeField] private Button _loadBtn;

    private void OnEnable()
    {
        _saveBtn.onClick.AddListener(Save);
        _loadBtn.onClick.AddListener(Load);
    }

    private void OnDisable()
    {
        _saveBtn.onClick.RemoveListener(Save);
        _loadBtn.onClick.RemoveListener(Load);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    private void Save()
    {
        var playerEntity = EntityManager.Instance.GetPlayerEntity();
        SaveData sd = new SaveData(playerEntity);
        SaveSystem.Instance.Save(sd);
    }

    private void Load()
    {
        //TODO load implemented
        var data = SaveSystem.Instance.Load();
        OnLoad?.Invoke(data);
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
