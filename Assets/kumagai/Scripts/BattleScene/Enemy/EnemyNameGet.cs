using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNameGet: MonoBehaviour
{
    public static string enemyNameText;
    // Start is called before the first frame update
    void Start()
    {
        enemyNameText=this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
