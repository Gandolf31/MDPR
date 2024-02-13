using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Levelup);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)
    {
        items[index].onClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if(ran[0]!=ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
            {
                break;
            }
        }

        for (int index=0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

            if(ranItem.level == ranItem.data.damages.Length)
            {
                // 여기 애매한 부분... 업그레이드 다 된 무기가 선택되었을 때
                // 업그레이드가 다 안된 무기쪽으로 선택이 회전하는게 아니라 그 칸을 힐템 SetActive로 돌려버려서
                // 무기가 다 업그레이드 안되었음에도 3칸 다 선택지가 나오는게 아니라 1,2 칸짜리 선택지가 나옴
                // 수정하는걸 목표로
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
