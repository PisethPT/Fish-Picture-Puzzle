using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSound;

    private void Start()
    {
        backgroundSound.Play();
    }
    public void TapToPlay()
    {
        SceneManager.LoadScene("Pic Puzzle");
    }
}
