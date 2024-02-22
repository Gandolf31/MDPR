using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 캐릭터 종류(ID)에 따라서 플레이어의 특성을 조절함
    // ex) 플레이어 아이디 1번은 Speed 특성을 1.1(10%)더 높게 가져감
    public static float Speed
    {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f; }
    }

    public static float WeaponSpeed
    {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f; }
    }

    public static float WeaponRate
    {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f; }
    }

    public static float Damage
    {
        get { return GameManager.instance.playerId == 2 ? 1.2f : 1f; }
    }

    public static int Count
    {
        get { return GameManager.instance.playerId == 3 ? 1 : 0; }
    }
}
