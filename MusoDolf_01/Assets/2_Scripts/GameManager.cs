using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;                     // ���� Liveüũ(�Ͻ�����, ������ ��)
    public float gameTime;                  // ���� �ð�
    public float maxGameTime = 2 * 10f;     // ���� �ð� ����
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // ���� ���� �������� �ʿ��� ����ġ ��
    [Header("# Game Object")]   
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;   // ������ UI(���� ������ ���� ��)
    public Result uiResult;
    public GameObject EnemyCleaner;

    // ���ӸŴ����� ��𼭵� ���� �����ϵ��� �ν��Ͻ�ȭ
    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        //test
        uiLevelUp.Select(0);    // ������ �� ���⸦ ��� �����ϱ� ���� - ù��° ĳ���� ����
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
        // ���� �Ͻ�����
        if (!isLive)
            return;

        // ���� Ÿ�̸�
        gameTime += Time.deltaTime;

        // ���� ���ѽð� �Ѿ�� ����
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

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) //�����Ǹ� �ְ� ����ġ���� ��� ���ˤ�
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // ���� Ÿ�� �Ͻ�����, �����

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