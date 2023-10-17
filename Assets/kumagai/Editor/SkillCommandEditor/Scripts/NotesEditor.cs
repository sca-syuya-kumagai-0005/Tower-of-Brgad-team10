using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesEditor : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; //キャンバス
    [SerializeField]
    private float speed;   //生成したコマンドの移動速度
    public static float s;     //インスペクターからアタッチされたCSVファイルを格納する動的配列//リストに変換した↑のデータを格納する動的配列
    [SerializeField]
    private GameObject[] notes;        //コマンドの画像を格納する配列
    [SerializeField]
    private string skillName;

    public enum NotesType
    {
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
        Left=0,
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
        s = speed;
        var notesDatas = Resources.Load<TextAsset>(skillName);
        if (Input.GetKeyDown(KeyCode.Return)) //試験的に、Enterキー入力でインスペクターにアタッチした一つ目のスキルを実行
        {
            StartCoroutine(NotesCreater(notesDatas));
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
        var notesData=csvDatas;
        for (int i = 1; i != -1; ++i)
        {
            NotesType c=NotesType.w;
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
                        pos = new Vector3(1010 + canvas.pixelRect.width / 2, canvas.pixelRect.height / 2, 0);
                        direction = NotesDirection.Left;
                    }
                    break;

                case "U":
                    {
                        pos = new Vector3(canvas.pixelRect.width / 2, -590 + canvas.pixelRect.height / 2, 0);
                        direction = NotesDirection.Up;
                    }
                    break;

                case "R":
                    {
                        pos = new Vector3(-1010 + canvas.pixelRect.width / 2, canvas.pixelRect.height / 2, 0);
                        direction = NotesDirection.Right;
                    }
                    break;

                case "D":
                    {
                        pos = new Vector3(canvas.pixelRect.width / 2, 590 + canvas.pixelRect.height / 2, 0);
                        direction = NotesDirection.Down;
                    }
                    break;
            }　     //三列目の値によってノーツの向きを決定
            yield return new WaitForSeconds(t); //二列目の値分だけ待機
            Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
        }
    }

}
