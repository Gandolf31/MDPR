using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;                     // ���� Liveüũ(�Ͻ�����, ������ ��)
    public float gameTime;                  // ���� �ð�
    public float maxGameTime = 2 * 10f;     // ���� �ð� ����
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; // ���� ���� �������� �ʿ��� ����ġ ��
    [Header("# Game Object")]   
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;   // ������ UI(���� ������ ���� ��)
    
    // ���ӸŴ����� ��𼭵� ���� �����ϵ��� �ν��Ͻ�ȭ
    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        //test
        uiLevelUp.Select(0);    // ������ �� ���⸦ ��� �����ϱ� ����
        isLive = true; // ���� ���� ��ư ������ ���� �����ϵ���
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
        }
    }

    public void GetExp()
    {
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