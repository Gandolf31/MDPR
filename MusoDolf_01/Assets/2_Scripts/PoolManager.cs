using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolManager : MonoBehaviour
{
    // 오브젝트 풀링으로 관리할 프리팹들 담는 배열
    public GameObject[] prefabs;
    
    List<GameObject>[] pools;

    void Awake()
    {
        // 프리팹 종류 수만큼 리스트 크기 지정, 게임 오브젝트 리스트를 리스트에 담음
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index ++)
        {
            // 리스트에 담긴 리스트들 초기화
            pools[index] = new List<GameObject>();
        }
    }

    // 배열에 설정한 풀링 오브젝트 종류에 따른 설정 (index 0 = enemy, 1 = bullet0 . . .)
    public GameObject Get(int index)
    {
        GameObject select = null;
        // . . . 선택한 풀의 비활성화 되어 있는 게임오브젝트 접근
            // . . . 발견하면 select 변수에 할당

        foreach (GameObject item in pools[index])
        {
            // 재사용 로직, 비활성화된 오브젝트를 재활성
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 활성화된 오브젝트가 없을 경우 새로 생성해서 리스트에 저장 후 반환
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        // . . . 못찾았을 경우
            // . . .새롭게 생성하고 select 변수에 할당
        return select;
    }
}