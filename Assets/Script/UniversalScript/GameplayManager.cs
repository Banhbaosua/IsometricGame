using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public enum GameplayState
{
    Gameplay,
    Pause,
    Win,
}

public class GameplayManager : MonoBehaviour
{
    [SerializeField] ObjectiveManager objectiveManager;
    [SerializeField] LoadingAsync loadingAsync;
    [SerializeField] CharacterInitiate characterSpawn;
    [SerializeField] GameObject winBoard;
    [SerializeField] GameObject pauseBoard;
    [SerializeField] CurrentMapLevelInfo currentMapLevelInfo;

    private PlayerController playerController;
    StateMachine<GameplayState,GamesStateDriver> stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine<GameplayState, GamesStateDriver>(this);
        stateMachine.ChangeState(GameplayState.Gameplay);
        //AudioManager.Instance.PlayMusic("Gameplay");
    }

    private void Start()
    {
        playerController = characterSpawn.Player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        stateMachine.Driver.Update.Invoke();
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
        winBoard.SetActive(true);
        currentMapLevelInfo.MapTierData.Complete();
        currentMapLevelInfo.MapTierData.Save(currentMapLevelInfo.SelectedScene);
    }

    void Gameplay_Enter()
    {
        Debug.Log("Start game");
    }

    void Gameplay_Update()
    {
        if(objectiveManager.IsCompleted)
        {
            stateMachine.ChangeState(GameplayState.Win);
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            stateMachine.ChangeState(GameplayState.Pause);
        }
    }

    void Win_Enter()
    {
        StartCoroutine(Win());
    }

    void Pause_Enter()
    {
        Debug.Log("Pause game");
        playerController.enabled = false;
        pauseBoard.SetActive(true);
        Time.timeScale = 0;
    }

    void Pause_Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            stateMachine.ChangeState(GameplayState.Gameplay);
        }
    }

    void Pause_Exit()
    {
        playerController.enabled = true;
        pauseBoard.SetActive(false);
        Time.timeScale = 1;
    }

    //button method
    public void BackToMenu()
    {
        Time.timeScale = 1;
        loadingAsync.LoadScene("MainMenu");
    }

    //button method
    public void Resume()
    {
        Time.timeScale = 1;
        pauseBoard.SetActive(false);
        stateMachine.ChangeState(GameplayState.Gameplay);
    }

    public class GamesStateDriver
    {
        public StateEvent Update;
    }
}
