using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabid;
    public float damage;
    public int count;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index < count; index++) {
            Transform bullet = GameManager.instance.pool.Get(prefabid).transform;
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }

}
