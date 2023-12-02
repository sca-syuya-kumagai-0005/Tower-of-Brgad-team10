using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BreakerEditor : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; //キャンバス
    [SerializeField]
    private float speed;   //生成したコマンドの移動速度
    public static float s;     //インスペクターからアタッチされたCSVファイルを格納する動的配列//リストに変換した↑のデータを格納する動的配列
    [SerializeField]
    private GameObject[] notes;        //コマンドの画像を格納する配列
    public static  string skillName;
    [SerializeField]
    GameObject[] SponePos;
    public static int commandDestroy = 0;
    [SerializeField]
    TextAsset notesDatas;
    [SerializeField] private bool tmplastNotes;
    public static bool lastNotes;
    [SerializeField] private bool tmpcommandEnd;
    public static bool commandEnd;
    [SerializeField] private GameObject breakerBackGorund;
    public enum NotesType
    {
        w = 0,
        a = 1,
        s = 2,
        d = 3,
        L = 4,
        U = 5,
        R = 6,
        D = 7
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
    }

    void Update()
    {
        
        if (SkillSelection.breakerFlag&&GameManager.state==GameManager.BattleState.breakerCommand)
        {
            breakerBackGorund.SetActive(true);
            tmplastNotes = lastNotes;
            tmpcommandEnd = commandEnd;
            var notesDatas = Resources.Load<TextAsset>("Skill/" + skillName);
            List<string[]> csvdata = CsvReader(notesDatas);
            Debug.Log("Countは" + csvdata.Count);
            Debug.Log("commandDestroyは" + commandDestroy);
            if (commandDestroy == csvdata.Count - 2)
            {
                commandEnd = true;
            }

            if (Input.GetKeyDown(KeyCode.Return)) //試験的に、Enterキー入力でインスペクターにアタッチした一つ目のスキルを実行
            {
                StartCoroutine(NotesCreater(notesDatas));
            }
        }
        if(GameManager.state==GameManager.BattleState.move)
        {
            breakerBackGorund.SetActive(false);
        }
    }

    IEnumerator NotesCreater(TextAsset TAD) //引数に入力されたリストをノーツとして生成する関数
    {
        List<string[]> csvDatas = new List<string[]>();
        StringReader reader = new StringReader(TAD.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        var notesData = csvDatas;
        for (int i = 1; i != -1; ++i)
        {
            NotesType c = NotesType.w;
            float t = 0.0f;
            Vector3 pos = new Vector3(0, 0, 0);
            string[] data = new string[notesData[i].Length]; //一行をまとめて格納する配列

            for (int j = 0; j < notesData[0].Length; ++j)
            {
                if (notesData[i][j] == (-1).ToString()) //-1が来たら関数終了
                {
                    yield break;
                }

                data[j] = notesData[i][j]; //一行を配列に格納
            }

            switch (data[0]) //配列の一つ目に入っている文字よって生成するコマンドを決定
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

                case "L":
                    c = NotesType.L;
                    break;

                case "U":
                    c = NotesType.U;
                    break;

                case "R":
                    c = NotesType.R;
                    break;

                case "D":
                    c = NotesType.D;
                    break;
            }　　　　//一列目の値によってノーツの種類を決定
            t = float.Parse(data[1]);　//二列目の値によってノーツが流れて来るまでの時間を決定
            switch (data[2])
            {
                case "L":
                    {
                        pos = SponePos[0].transform.position;
                        direction = NotesDirection.Left;
                    }
                    break;

                case "U":
                    {
                        pos = SponePos[1].transform.position;
                        direction = NotesDirection.Up;
                    }
                    break;

                case "R":
                    {
                        pos = SponePos[2].transform.position;
                        direction = NotesDirection.Right;
                    }
                    break;

                case "D":
                    {
                        pos = SponePos[3].transform.position;
                        direction = NotesDirection.Down;
                    }
                    break;
            }　     //三列目の値によってノーツの向きを決定
            yield return new WaitForSeconds(t); //二列目の値分だけ待機
            Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
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

}
