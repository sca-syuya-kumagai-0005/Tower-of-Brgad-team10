using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloarManager : MonoBehaviour
{
    public static int nowFloar=0;
    [SerializeField]
    private Text FloorText;
    // Start is called before the first frame update
    void Start()
    {
        nowFloar++;
        FloorText.text = nowFloar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.state==GameManager.BattleState.reSult)
        {
            FloorText.text=nowFloar.ToString();
            //EnemyManager.EnemyInfo.Enemy_Lv[0]=30;
            EnemyManager.EnemyInfo.Enemy_Lv[0]=1+(nowFloar-1)*3;
        }
    }
}
