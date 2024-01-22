using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    // �÷��̾� ī�޶� �������� ���� ������ų �� ���ӿ�����Ʈ �迭
    public Transform[] spawnPoint;
    // ������ ���� �迭
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        // ���� ����Ʈ ��ġ�� ������ �ִ� �ڽ� �� ���ӿ�����Ʈ���� ������ �迭 �������� �ʱ�ȭ
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // ������/�Ͻ����� ������ ���� ���� �Ͻ����� �߿��� �۵� X - ��� ��ũ��Ʈ�� Update ���� �ڵ忡 �� �־���
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        // �ð��� �����鼭 �� ���� ���� ���� ( SpawnData�� ������ ) > ������ ������ ���͵� ���� ���� �������� �����ϱ�
        // 10��(10f) �� ���� ������ 1������ ������, �ִ� ���� �̻� �ȿö󰡵��� Mathf.Min���
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        // ���� Ÿ�̸�, ������ �´� ���� ���� ���� �����Ϳ� �ִ� ���� ��Ÿ�ӿ� ���� ����
        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // ������Ʈ Ǯ���� �����ؼ� ���� ��ȯ
        // PoolManager�� [0]�� �迭�� ���� ������ ����Ǿ� ���� - enemy�� �ش� ������ ����
        GameObject enemy = GameManager.instance.pool.Get(0);
        // ���� ����Ʈ ��Ƶ� �� ���� ������Ʈ�� �������� �����ͼ� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // ������ ���� ��Ƶ� ���� ������ ���� ���� ��ũ��Ʈ�� �� ����
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// �� ������ ���� ���� �����͸� ��� Ŭ����
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}