using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesEditor : MonoBehaviour
{
    [SerializeField]private GameObject notesBackGround;
    [SerializeField]private GameObject moveText;
    [SerializeField]private GameObject moveTextImage;
    [SerializeField]private GameObject Judge;
    [SerializeField]private GameObject goodText;
    public static bool goodflg=false;
    public static GameObject tmpgoodText;
    public static int commandDestroy=0;
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
    public static int skillCommandCount;
    public static float NotesOKCount;
    GameObject speedManager;
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

    public enum NotesDirection
    {
        Left = 0,
        Up,
        Right,
        Down
    }

    public static NotesDirection direction;

    void Start()
    {
        skillName="スラッシュ";
        tmpgoodText=goodText;
    }

    void Update()
    {
        if(!SkillSelection.breakerFlag)
        { 
        tmplastNotes=lastNotes;
        tmpcommandEnd=commandEnd;
        notesDatas = Resources.Load<TextAsset>("Skill/" + skillName);
        List<string[]> csvdata= CsvReader(notesDatas);
        if (commandDestroy==csvdata.Count-2)
        {
           commandEnd=true;
        }
       if(GameManager.state==GameManager.BattleState.command)
        {
            moveTextImage.SetActive(false);
            moveText.SetActive(false);
            notesBackGround.SetActive(true);
            Judge.SetActive(true);
        }
        else
        {
            moveTextImage.SetActive(true);
            moveText.SetActive(true);
            notesBackGround.SetActive(false);
            Judge.SetActive(false);
        }
        if (CharaMoveGage.MoveChar[0] != null&&GameManager.state==GameManager.BattleState.command)
        {
            notesDatas = Resources.Load<TextAsset>("Skill/" + skillName);
            List<string[]> data = CsvReader(notesDatas);
        
        skillCommandCount =data.Count;
        SkillStorage.CommandCount=data.Count-2;
        CommandController.Count=data.Count-2;
        s = speed;

            if (GameManager.state==GameManager.BattleState.command&&!commandStart)
            { 
                StartCoroutine(NotesCreater());
                NotesOKCount=0;
                commandStart=true;
            }
        }
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
            int dir = Random.Range(0, 4);
            Vector3 pos = new Vector3(10, -4, 0);
            switch (dir)
            {
                case 0:
                    {
                       
                        direction = NotesDirection.Left;
                    }
                    break;

                case 1:
                    {
                    
                        direction = NotesDirection.Up;
                    }
                    break;

                case 2:
                    {
                      
                        direction = NotesDirection.Right;
                    }
                    break;

                case 3:
                    {
                        
                        direction = NotesDirection.Down;
                    }
                    break;
            }　     //三列目の値によってノーツの向きを決定
            yield return new WaitForSeconds(t); //二列目の値分だけ待機
            if(c!=NotesType.i)
            { 
                speedManager=Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
                speedManager=speedManager.transform.GetChild(0).gameObject;
                speedManager.name=(float.Parse(data[i][2])).ToString();
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
            for (int j = 0; j < skillDatas[0].Length; ++j)
            {
                if (skillDatas[i][j] == "-1")
                {
                    return skillDatas;
                }
                data[j] = skillDatas[i][j];
            }
        }
        return skillDatas;
    }
    //public static IEnumerator good(GameObject obj)
    //{
    //    tmpgoodText.SetActive(goodflg);
    //    yield return 1;
    //    Destroy(obj);
    //}

}


