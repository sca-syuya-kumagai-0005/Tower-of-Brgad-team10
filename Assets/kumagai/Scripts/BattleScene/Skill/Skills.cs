using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    int[] Actions;
    int ActionNumber;
    int allAction;
    int NowAction ;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            protEnemyskill();
        }
    }
    // Start is called before the first frame update
    void protEnemyskill()
    {
        Actions=new int[3];//�G�l�~�[�̍s�����
        Actions[0]=10;//�s��1
        Actions[1]=10;//�s��2
        Actions[2]=5;//�s��3
        NowAction=Random.Range(1,Actions[1]+Actions[1]+Actions[2]);
        Debug.Log(NowAction);
        for(int i=0;i<Actions.Length;i++)
        {
            NowAction-=Actions[i];
            if(NowAction<=0)
            {
                ActionNumber=i;
                Debug.Log(i);
                break;
            }
        }


    }
}
