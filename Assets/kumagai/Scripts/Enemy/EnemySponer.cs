using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponer : MonoBehaviour
{
    [SerializeField]GameObject[] Enemies;
    [SerializeField]GameObject SponePos;
    [SerializeField]private GameObject Enemy;
    public static GameObject[] sponeEnemy=new GameObject[1];
    public static  GameObject enemy;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyManager.enemyNumber=Random.Range(1,Enemies.Length)+1;
        enemy=Instantiate(Enemies[EnemyManager.enemyNumber-1],SponePos.transform.position,Quaternion.identity,Enemy.transform);
        enemy.gameObject.name = enemy.gameObject.name.Replace("(Clone)", "");
        sponeEnemy[0]=enemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
