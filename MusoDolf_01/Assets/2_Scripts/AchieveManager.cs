using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{

    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achieve { UnlockPotato, UnlockBean };
    Achieve[] achieves;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        // �������� �����س��� ���� ���� Achieve �迭�� ����
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        wait = new WaitForSecondsRealtime(5);
        // ����� �����Ͱ� ������ ( ù ���� ) �ʱ�ȭ ����
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        // ŰMyData�� ���� 1�� ���� >> �����Ͱ� �����Ѵ�
        PlayerPrefs.SetInt("MyData", 1);
        foreach(Achieve achieve in achieves)
        {
            // ��� �������� Ŭ���� ���� 0���� ����
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }
    void UnlockCharacter()
    {
        // �������� �̸�(Ű)�� ���� ���� ����(��)�� bool������ ���� �� ĳ���� ����â�� SetActive�� ����
        for(int index=0; index < lockCharacter.Length; index++)
        {
            string achieveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }
    // �� �������� �� �޼� ���� üũ
    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;

        switch (achieve)
        {
            case Achieve.UnlockPotato:
                isAchieve = GameManager.instance.kill >= 10;
                break;
            case Achieve.UnlockBean:
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }
        // ���������� �޼��ϰ� && ���� �ش� �������� Ŭ��� ����Ǿ� ���� �ʴٸ�
        if(isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            // ���������� �޼����� ����
            PlayerPrefs.SetInt(achieve.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achieve;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }   

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Levelup);
        yield return wait;

        uiNotice.SetActive(false);
    }
}
