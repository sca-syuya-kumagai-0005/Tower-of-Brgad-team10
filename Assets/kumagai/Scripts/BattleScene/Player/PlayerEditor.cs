using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerEditor : MonoBehaviour
{

    public static int partyTheNumberOf;
    [SerializeField]
    private string[] name;
    public static string[] PlayerName;
    public static List<string[]> playerData;
    public static List<string[]>[] playerDatas;
    private TextAsset[] PlayerDatas;
    [SerializeField] TextAsset[] DebugDatas;
    

    // Start is called before the first frame update
    void Awake()
    {
        partyTheNumberOf=name.Length;
        PlayerName=new string[partyTheNumberOf];
        PlayerName=TeamCharacter.charaName;
        //PlayerName = name;
        playerDatas =new List<string[]>[name.Length];//パーティのキャラ数を入れる今は最大値の4を仮で入れる
        PlayerDatas=new TextAsset[name.Length];
        for (int i = 0; i < playerDatas.Length; i++)
        {
            if(PlayerName[i]!=""&&PlayerName[i]!=null) {
                PlayerDatas[i] = Resources.Load<TextAsset>("Character/" + PlayerName[i]);
                PlayerStatus(PlayerDatas[i], i);
                DebugDatas[i] = PlayerDatas[i];
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerStatus(TextAsset TAD,int Integer)
    {
            List<string[]> csvDatas=new List<string[]>();
            StringReader reader =new StringReader(TAD.text);
            while(reader.Peek()!=-1)//ー１が入るまでループ
            {
                string line = reader.ReadLine();
                csvDatas.Add(line.Split(','));
            }
            if(reader.Peek()==-1)
            {
            playerDatas[Integer] = csvDatas;
            return;
            }

        for (int i = 2; i != -1; i++)
        {
            string[] data = new string[playerDatas[Integer][i].Length];
            for (int j = 0; j < playerDatas[Integer][0].Length; ++j)
            {
                if (playerDatas[Integer][i][j] == "-1")
                {
                    Debug.Log("-1");
                    return;
                }

            }
        }
    }

    
}
