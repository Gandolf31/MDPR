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
        // 열거형에 저장해놨던 업적 종류 Achieve 배열에 저장
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
        wait = new WaitForSecondsRealtime(5);
        // 저장된 데이터가 없을때 ( 첫 시작 ) 초기화 설정
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        // 키MyData의 값을 1로 설정 >> 데이터가 존재한다
        PlayerPrefs.SetInt("MyData", 1);
        foreach(Achieve achieve in achieves)
        {
            // 모든 도전과제 클리어 상태 0으로 세팅
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }
    void UnlockCharacter()
    {
        // 도전과제 이름(키)에 따른 성공 여부(값)를 bool값으로 저장 후 캐릭터 선택창에 SetActive로 적용
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
    // 각 도전과제 별 달성 조건 체크
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
        // 도전과제를 달성하고 && 아직 해당 도전과제 클리어가 저장되어 있지 않다면
        if(isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            // 도전과제를 달성으로 저장
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
