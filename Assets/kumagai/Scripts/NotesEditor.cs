using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesEditor : MonoBehaviour
{
    [SerializeField]
    private float speed;   //生成したコマンドの移動速度
    public static float s;     //インスペクターからアタッチされたCSVファイルを格納する動的配列//リストに変換した↑のデータを格納する動的配列
    [SerializeField]
    private GameObject[] notes;        //コマンドの画像を格納する配列
    public static string skillName;
    [SerializeField]
    private float minWait;
    [SerializeField]
    private float maxWait;
    public static bool commandStart;
    [SerializeField]
    TextAsset notesDatas;
    private int skillCommandCount;
    public enum NotesType
    {
        i=-1,
        w=0,
        a=1,
        s=2,
        d=3,
        L=4,
        U=5,
        R=6,
        D=7
    }

    //public enum NotesDirection
    //{
    //    Left=0,
    //    Up,
    //    Right,
    //    Down
    //}

   // public static NotesDirection direction;

    void Start()
    {
        skillName="スラッシュ";
    }

    void Update()
    {
        tmplastNotes=lastNotes;
        tmpcommandEnd=commandEnd;
        notesDatas = Resources.Load<TextAsset>(skillName);
        List<string[]> data = CsvReader(notesDatas);
        skillCommandCount =data.Count;
        s = speed;

        if (GameManager.state==GameManager.BattleState.command&&!commandStart)
        { 
            StartCoroutine(NotesCreater());
            commandStart=true;
        }
    }
    [SerializeField]private bool tmplastNotes;
    public static bool lastNotes;
    [SerializeField]private bool tmpcommandEnd;
    public static bool commandEnd;
    IEnumerator NotesCreater() //引数に入力されたリストをノーツとして生成する関数
    {
        for(int i=0;i<skillCommandCount;i++)
        { 
            Debug.Log(i);
            if(i==skillCommandCount-1)
            {
                lastNotes=true;
            }
            List<string[]> data = CsvReader(notesDatas);
            NotesType c = NotesType.i;
      
            switch (data[i][0]) //配列の一つ目に入っている文字よって生成するコマンドを決定
            {
                case "w":
                    c = NotesType.w;
                    break;

                case "a":
                    c = NotesType.a;
                    break;

                case "s":
                    c = NotesType.s;
                    break;

                case "d":
                    c = NotesType.d;
                    break;

                case "←":
                    c = NotesType.L;
                    break;

                case "↑":
                    c = NotesType.U;
                    break;

                case "→":
                    c = NotesType.R;
                    break;

                case "↓":
                    c = NotesType.D;
                    break;
            }　　　　//一列目の値によってノーツの種類を決定
        
            float t = Random.Range(minWait,maxWait);　//二列目の値によってノーツが流れて来るまでの時間を決定
            //int dir=Random.Range(0,4);
            Vector3 pos=new Vector3(10,-4,0);
            //switch (dir) 
            //{
            //    case 0:
            //        {
                       
            //            direction = NotesDirection.Left;
            //        }
            //        break;

            //    case 1:
            //        {
                      
            //            direction = NotesDirection.Up;
            //        }
            //        break;

            //    case 2:
            //        {
                      
            //            direction = NotesDirection.Right;
            //        }
            //        break;

            //    case 3:
            //        {
                      
            //            direction = NotesDirection.Down;
            //        }
            //        break;
            //}　     //三列目の値によってノーツの向きを決定
            yield return new WaitForSeconds(t); //二列目の値分だけ待機
            if(c!=NotesType.i)
            { 
            Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
            }
        }
    }
    private List<string[]> CsvReader(TextAsset csvData) //引数に入力したCSVファイルをリストに変換する関数
    {
        List<string[]> skillDatas = new List<string[]>();
        StringReader reader = new StringReader(csvData.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            skillDatas.Add(line.Split(','));
        }

        string[] data = new string[skillDatas[0].Length]; //一行をまとめて格納する配列

        for (int i = 1; i != -1; ++i) //CSVファイル最後までループ
        {
            Debug.Log("ok");
            for (int j = 0; j < skillDatas[0].Length; ++j)
            {
                Debug.Log("OK");
                if (skillDatas[i][j] == "-1")
                {
                    return skillDatas;
                }
                data[j] = skillDatas[i][j];
            }
        }
        return skillDatas;
    }
}


