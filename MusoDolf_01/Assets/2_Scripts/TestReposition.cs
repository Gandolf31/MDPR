using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch (transform.tag)
        {
            // 적이 시야범위 밖으로 나갔을 때 위치 다시 리스폰
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), 0);

                    transform.Translate(ran + dist * 2);
                }
                break;

        }
    }
}
