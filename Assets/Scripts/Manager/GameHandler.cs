using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private int _winPrice;
    public int WinPrice { get { return _winPrice; } }
    public int goldAmount = 0;
    private GameData _gameData;
    public static GameHandler Instance;

    [SerializeField]
    private List<string> _sceneName;
    [SerializeField]
    private string _shopSceneName;
    [SerializeField]
    private string _winSceneName;
    private int _currentLevel = -1;
    public bool IsShopLevel = false;
    public bool IsStartScreen = true; // the game starts here


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, IncreaseGold);
        EventManager.StartListening(EventManager.Events.OnWin, Win);
        EventManager.StartListening(EventManager.Events.EndSong, EndSong);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, IncreaseGold);
        EventManager.StopListening(EventManager.Events.EndSong, EndSong);
        EventManager.StopListening(EventManager.Events.OnWin, Win);
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void LoadNextLevel()
    {

        string nextLevelName;
        if (IsStartScreen)
        {
            IsStartScreen = false;
            IsShopLevel = true;

        }
        else
        {
            RythmManager.Instance.Stop();
            Destroy(RythmManager.Instance.gameObject);
            IsShopLevel = false;
            _currentLevel++;
            if (_sceneName.Count < _currentLevel)       // If there in not enough level, start a new cycle
            {
                _currentLevel = 0;
            }
            nextLevelName = _sceneName[_currentLevel];
            SceneManager.LoadScene(nextLevelName);
        }



    }
    public void IncreaseGold()
    {
        if (!IsShopLevel && !IsStartScreen)
        {
            EventManager.TriggerEvent(EventManager.Events.OnGoldWin);
            goldAmount++;
        }
    }
    public void Win()
    {
        RythmManager.Instance.Stop();
        SceneManager.LoadScene(_winSceneName);
    }
    private void LoadShop ()
    {
        IsShopLevel = true;
        SceneManager.LoadScene(_shopSceneName);
    }
    public void EndSong()
    {
        RythmManager.Instance.Stop();
        LoadShop();
    }
}
