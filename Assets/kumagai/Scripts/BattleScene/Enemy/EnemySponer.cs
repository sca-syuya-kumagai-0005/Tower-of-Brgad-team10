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
        //EnemyManager.enemyNumber=Random.Range(0,0)+1;
       
        
        if(FloarManager.nowFloar==9)
        {
            EnemyManager.enemyNumber=7;
        }
        else if(FloarManager.nowFloar==19)
        {
            EnemyManager.enemyNumber=8;
        }
        else if(FloarManager.nowFloar==29)
        {
            EnemyManager.enemyNumber=9;
        }
        else if (FloarManager.nowFloar % 10 == 0&&FloarManager.nowFloar>=31)
        {
            EnemyManager.enemyNumber = Random.Range(7, Enemies.Length) + 1;
        }
        else
        {
            EnemyManager.enemyNumber = Random.Range(0, Enemies.Length - 3) + 1;
        }
        enemy =Instantiate(Enemies[EnemyManager.enemyNumber-1],SponePos.transform.position,Quaternion.identity,Enemy.transform.Find("Enemy").transform);
        enemy.gameObject.name = enemy.gameObject.name.Replace("(Clone)", "");
        sponeEnemy[0]=enemy;
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
