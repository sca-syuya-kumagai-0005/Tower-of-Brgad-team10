using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class EnemyEditor : MonoBehaviour
{
    [SerializeField]
    private string enemyData;
    public static List<string[]> EnemyData;
    public static bool enemyDataSet;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyDataSet=false;
    }

    // Update is called once per frame
    void Update()
    {
        var EnemyDatas=Resources.Load<TextAsset>("Character/"+enemyData);
        if(GameManager.state==GameManager.BattleState.start)
        {
            EnemyStatus(EnemyDatas);
            enemyDataSet=true;
        }
    }

    void EnemyStatus(TextAsset TAD)
    {
        List<string[]> csvDatas=new List<string[]>();
        StringReader reader =new StringReader(TAD.text);
        while(reader.Peek()!=-1)//Å[ÇPÇ™ì¸ÇÈÇ‹Ç≈ÉãÅ[Év
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
         EnemyData=csvDatas;
        
        for(int i=1;i!=-1;i++)
        {
            string[] data=new string[EnemyData[i].Length];
            for(int j=0;j<EnemyData[0].Length;++j)
            {
                if(EnemyData[i][j]=="-1")
                {
                    return;
                }
                    data[j]=EnemyData[EnemyManager.enemyNumber][j];
            }
        }
    }

    
}
