using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    // 플레이어 카메라 기준으로 몹을 스폰시킬 빈 게임오브젝트 배열
    public Transform[] spawnPoint;
    // 몬스터의 정보 배열
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        // 스폰 포인트 위치를 가지고 있는 자식 빈 게임오브젝트들을 설정한 배열 변수에다 초기화
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // 레벨업/일시정지 등으로 인한 게임 일시정지 중에는 작동 X - 모든 스크립트의 Update 관련 코드에 다 넣었음
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        // 시간이 지나면서 더 강한 몬스터 스폰 ( SpawnData에 들어가있음 ) > 기존에 나오던 몬스터도 같이 섞여 나오도록 수정하기
        // 10초(10f) 당 스폰 레벨이 1레벨씩 오르고, 최대 레벨 이상 안올라가도록 Mathf.Min사용
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length - 1);

        // 스폰 타이머, 레벨에 맞는 몬스터 스폰 스폰 데이터에 있는 스폰 쿨타임에 맞춰 스폰
        if(timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // 오브젝트 풀링에 접근해서 몬스터 소환
        // PoolManager의 [0]번 배열에 몬스터 프리팹 저장되어 있음 - enemy에 해당 프리팹 저장
        GameObject enemy = GameManager.instance.pool.Get(0);
        // 스폰 포인트 잡아둔 빈 게임 오브젝트를 랜덤으로 가져와서 스폰
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // 레벨에 따라 담아둔 스폰 데이터 값을 몬스터 스크립트에 값 전달
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 그 레벨에 나올 몬스터 데이터를 담는 클래스
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}