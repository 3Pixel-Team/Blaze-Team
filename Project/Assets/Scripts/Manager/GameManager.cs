using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance { private set; get; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("[GameManager] There is more than one GM Instance");
                return;
            }
            Instance = this;
        }

    #endregion


    public CharacterStats_SO playerStats;

    public enum GameState { GamePause, GameWon, GameLost};

    public event EventHandler OnPlayerDeathEvent;

    // subscribe to this whenever you want to do something when level starts
    public UnityAction OnLevelStartedEvent;
    // subscribe to this whenever you want to do something when level ends
    public UnityAction OnLevelLostEvent;
    // subscribe to this whenever you want to do something when the player wins
    public UnityAction OnLevelWonEvent;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (playerStats == null)
        {
            Debug.Log("[Game Manager] There is no player stats attached!");
        }

        playerStats.LoadStats();

        /*  Start on restart
     *  GoToRestartScene();
     */

        /* Start ingame
     *  GotoGameScene();
     */
        OnPlayerDeathEvent += OnPlayerDeath;
        OnLevelLostEvent += OnLevelLostManager;
        OnLevelWonEvent += OnLevelWonManager;
    }

    private void Update()
    {
        #region Scene Sound

        Scene currentScene = SceneManager.GetActiveScene();
        PlaySceneSound(currentScene.name);

        #endregion


    }

    #region Sound

    private void PlaySceneSound(string sceneName)
    {
        var sound = AudioManager.Instance.IsPlayingSound(sceneName + "Sound_1");
        if (!sound)
        {
            sound = AudioManager.Instance.IsPlayingSound(sceneName + "Sound_2");
            if (!sound)
            {
                var random = UnityEngine.Random.Range(0, 1);
                if (random < 0.5f)
                {
                    AudioManager.Instance.Play(sceneName + "Sound_1");
                }
                else
                {
                    AudioManager.Instance.Play(sceneName + "Sound_2");
                }
            }
        }
    }

    #endregion



    #region Scene Changes

    public void GoToRestartScene()
    {
        SceneChange("GameOver");
        // player loses everything he earned
        OnLevelLostEvent?.Invoke();
    }

    public void GoToGameScene()
    {
        SceneChange("Desert");
        OnLevelStartedEvent?.Invoke();
    }

    public void GoToMainMenu()
    {
        SceneChange("MainMenu");
        OnLevelLostEvent?.Invoke();
    }

    private void SceneChange(string sceneName)
    {
        //button animation and other things

        if (!_isChangingToLoadScene)
        {
            StartCoroutine(ChangeSceneOnLoad(sceneName));
        }
    }

    private bool _isChangingToLoadScene;

    IEnumerator ChangeSceneOnLoad(string sceneName)
    {
        Scene sceneToUnload = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByName("Scene") == sceneToUnload)
            yield break;

        if (sceneName == "GameOver")
        {
            SceneManager.LoadSceneAsync(sceneName);
            yield break;
        }
        _isChangingToLoadScene = true;

        SceneManager.LoadScene("LoadScene");

        _isChangingToLoadScene = false;

        yield return null; //wait one frame so the singleton can be loaded in the StatusBar script

        LoadSceneBar.Instance.LoadScene(sceneName);
    }

    #endregion

    #region End level changes

    public void OnLevelLostManager()
    {
        
    }

    public void OnLevelWonManager()
    {
        OnLevelWonEvent.Invoke();
        GoToMainMenu();
    }

    #endregion

    #region Player Death

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        SceneChange("GameOver");
    }

    public void DeathEventCall()
    {
        OnPlayerDeathEvent?.Invoke(this, EventArgs.Empty);
    }

    #endregion

}
