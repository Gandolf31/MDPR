using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ü�¹� ���󰡱�, �÷��̾ ���� ȭ�鿡 ���̴� ��ǥ�� ĵ������ ��ǥ�� �����Ͽ� ���󰡵���
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
