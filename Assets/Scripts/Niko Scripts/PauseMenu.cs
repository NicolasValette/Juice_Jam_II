using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject _pauseMenu;

    private bool _isGamePaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (_isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            
        }
    }

    private void Pause()
    {
        _pauseMenu.SetActive(true);
        RythmManager.Instance.Pause();
        Time.timeScale = 0f;
        _isGamePaused = true;
    }
    private void Resume()
    {
        _pauseMenu.SetActive(false);
        RythmManager.Instance.Resume();
        Time.timeScale = 1f;
        _isGamePaused = false;
    }
}
