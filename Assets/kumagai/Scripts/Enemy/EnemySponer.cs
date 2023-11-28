using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponer : MonoBehaviour
{
    [SerializeField]GameObject[] Enemies;
    [SerializeField]GameObject SponePos;
    [SerializeField]private GameObject Enemy;
    public static  GameObject enemy;
    // Start is called before the first frame update
    void Awake()
    {
        int rand=Random.Range(0,Enemies.Length);
        enemy=Instantiate(Enemies[rand],SponePos.transform.position,Quaternion.identity,Enemy.transform);
        enemy.gameObject.name = enemy.gameObject.name.Replace("(Clone)", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
