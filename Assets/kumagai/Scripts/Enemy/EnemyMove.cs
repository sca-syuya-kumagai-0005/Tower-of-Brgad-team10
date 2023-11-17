using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void protEnemyMove()//プロト版でのエネミーの行動
    {
        if (GameManager.state == GameManager.BattleState.move)
        {
            int move=Random.Range(0,4);
        }
    }
}
