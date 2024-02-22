using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloarManager : MonoBehaviour
{
    public static int nowFloar=0;
    [SerializeField]
    private Text FloorText;
    [SerializeField]
    private GameObject BackGround;
    // Start is called before the first frame update
    void Start()
    {
        nowFloar++;
        if (nowFloar>10)
        {
            BackGround.GetComponent<Image>().sprite=Resources.Load<Sprite>("BackGround/X");
        }
        if(nowFloar>20)
        {
            BackGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("BackGround/“ƒ");
        }
        if(nowFloar/10==3&&nowFloar%10==0)
        {
            BackGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("BackGround/“ƒ2");
        }
        FloorText.text = nowFloar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.state==GameManager.BattleState.reSult)
        {
            FloorText.text=nowFloar.ToString();
            //EnemyManager.EnemyInfo.Enemy_Lv[0]=30;
        }
    }
}
