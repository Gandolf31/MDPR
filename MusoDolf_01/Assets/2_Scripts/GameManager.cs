using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    // 게임 Live체크(일시정지, 레벨업 등)
    public bool isLive;                     
    public float gameTime;                  
    public float maxGameTime = 2 * 10f;    
    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]   
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject EnemyCleaner;
    public Transform uiJoy;

    // 게임매니저를 어디서든 접근 가능하도록 인스턴스화
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);

        // 캐릭터id에 해당하는 캐릭터의 무기로 게임 스타트 
        uiLevelUp.Select(playerId % 2);   
        Resume();

        AudioManager.instance.PlayBgm(true);    
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
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

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }


    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {

        isLive = false;
        // 맵 전체를 10000~~ 데미지의 콜라이더로 덮어서 게임 종료시 적 정리
        EnemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }


    public void GameQuit()
    {
        Application.Quit();
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

        // 만렙되면 최고 경험치통을 계속 사용
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) 
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
            uiJoy.localScale = Vector3.zero;
        }
    }
    public void Resume()
    {
        {
            isLive = true;
            Time.timeScale = 1;
            uiJoy.localScale = Vector3.one;
        }
    }
}