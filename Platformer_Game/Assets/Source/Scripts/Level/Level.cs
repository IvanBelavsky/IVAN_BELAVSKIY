using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level
{
    public event Action Defeat;

    public void Start()
    {
        Debug.Log("Start level");
    }

    public void Restart()
    {
        // очистка уровня
        SceneManager.LoadScene("SampleScene");
        Start();
    }

    public void OnDefeat()
    {
        // логика остановки игры 
        Defeat?.Invoke();
    }
}