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
        if (GameManager.state==GameManager.BattleState.command&&Input.GetKeyDown(KeyCode.Return))
        { 
            StartCoroutine(NotesCreater());
        }
    }
    int maxi=5;
    public static bool lastNotes;
    public static bool commandEnd;
    IEnumerator NotesCreater() //引数に入力されたリストをノーツとして生成する関数
    {
        for(int i=0;i<maxi;i++)
        { 
            if(i==maxi-1)
            {
                lastNotes=true;
            }
        NotesType c = NotesType.w;
        int data = Random.Range(0, 8); 
            switch (data) //配列の一つ目に入っている文字よって生成するコマンドを決定
            {
                case 0:
                    c = NotesType.w;
                    break;

                case 1:
                    c = NotesType.a;
                    break;

                case 2:
                    c = NotesType.s;
                    break;

                case 3:
                    c = NotesType.d;
                    break;

                case 4:
                    c = NotesType.L;
                    break;

                case 5:
                    c = NotesType.U;
                    break;

                case 6:
                    c = NotesType.R;
                    break;

                case 7:
                    c = NotesType.D;
                    break;
            }　　　　//一列目の値によってノーツの種類を決定
        
            float t = Random.Range(minWait,maxWait);　//二列目の値によってノーツが流れて来るまでの時間を決定
            int dir=Random.Range(0,4);
            Vector3 pos=new Vector3(10,-4,0);
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
            Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
        }
    }
}


