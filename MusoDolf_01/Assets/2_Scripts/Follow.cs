using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 체력바 따라가기, 플레이어가 게임 화면에 보이는 좌표와 캔버스의 좌표를 조정하여 따라가도록
public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
