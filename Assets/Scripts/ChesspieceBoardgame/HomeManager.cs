using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private AudioManager audio;
    private void Awake()
    {
        audio = AudioManager.Instance;
    }

    private void Start()
    {
        audio.PlayMusic("BGM1");
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
