using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;                     // 게임 Live체크(일시정지, 레벨업 등)
    public float gameTime;                  // 게임 시간
    public float maxGameTime = 2 * 10f;     // 게임 시간 제한
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // 각각 다음 레벨까지 필요한 경험치 양
    [Header("# Game Object")]   
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;   // 레벨업 UI(무기 레벨업 선택 등)
    public Result uiResult;
    public GameObject EnemyCleaner;

    // 게임매니저를 어디서든 접근 가능하도록 인스턴스화
    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        //test
        uiLevelUp.Select(0);    // 시작할 때 무기를 들고 시작하기 위함 - 첫번째 캐릭터 선택
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {

        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();

        Stop();
    }


    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {

        isLive = false;
        EnemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        // 게임 일시정지
        if (!isLive)
            return;

        // 게임 타이머
        gameTime += Time.deltaTime;

        // 게임 제한시간 넘어가면 고정
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;
        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) //만렙되면 최고 경험치통을 계속 사요ㅛㅇ
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // 게임 타임 일시정지, 재시작

    public void Stop()
    {
        {
            isLive = false;
            Time.timeScale = 0;
        }
    }
    public void Resume()
    {
        {
            isLive = true;
            Time.timeScale = 1;
        }
    }
}