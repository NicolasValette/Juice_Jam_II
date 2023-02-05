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
    private string _startSceneName;
    [SerializeField]
    private string _winSceneName;
    [SerializeField]
    private string _gameOverSceneName;
    [SerializeField]
    private int _firstThresholdFire = 5;
    [SerializeField]
    private int _secondThresholdFire = 5;
    [SerializeField]
    private AudioSource _powerUp;
    [SerializeField]
    private int _GoldSpawn;
    public int GoldSpawn { get { return _GoldSpawn; } }
    public int FirstfirstThresholdFire { get { return _firstThresholdFire; } }
    public int SecondThresholdFire { get { return _secondThresholdFire; } }

    private int _currentLevel = -1;
    public bool IsShopLevel = false;
    public bool IsStartScreen = true; // the game starts here
    public bool IsGameOver = false;

    public int ComboMultipl = 1;


    public bool DisplayTutorial { get; private set; } = true;
    public bool IsGameOn { get; private set; } = false;


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
        EventManager.StartListening(EventManager.Events.OnPlayerDeath, GameOver);
        EventManager.StartListening(EventManager.Events.OnStartSong, StartSong);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, IncreaseGold);
        EventManager.StopListening(EventManager.Events.EndSong, EndSong);
        EventManager.StopListening(EventManager.Events.OnWin, Win);
        EventManager.StopListening(EventManager.Events.OnPlayerDeath, GameOver);
        EventManager.StopListening(EventManager.Events.OnStartSong, StartSong);
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
            DisplayTutorial = false;
            RythmManager.Instance.Stop();
            Destroy(RythmManager.Instance.gameObject);
            IsShopLevel = false;
            _currentLevel++;
            if (_sceneName.Count <= _currentLevel)       // If there in not enough level, start a new cycle
            {
                _currentLevel = 0;
            }
            nextLevelName = _sceneName[_currentLevel];
            SceneManager.LoadScene(nextLevelName);
        }
    }
    public void IncreaseGold()
    {
        if (IsGameOn)
        {
            EventManager.TriggerEvent(EventManager.Events.OnGoldWin);
            goldAmount++;
        }
    }
    public void Win()
    {
        IsGameOver = true;
        RythmManager.Instance.Stop();
        SceneManager.LoadScene(_winSceneName);
        Destroy(gameObject);
    }
    private void LoadShop()
    {
        IsShopLevel = true;
        IsGameOn = false;
        SceneManager.LoadScene(_shopSceneName);
    }
    public void EndSong()
    {
        RythmManager.Instance.Stop();
        LoadShop();
    }
    public void ReturnMainScreen()
    {
        IsGameOver = false;
        IsStartScreen = true;
        SceneManager.LoadScene(_startSceneName);
    }
    public void GameOver()
    {
        IsGameOver = true;
        Invoke("LoadGameOver", 2f);
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene(_gameOverSceneName);
        Destroy(gameObject);
    }
    public void StartSong()
    {
        if (!IsShopLevel && !IsStartScreen && !IsGameOver)
        {
            IsGameOn = true;
        }
    }
    public void PowerUp()
    {
        _powerUp.Play();
    }
}
