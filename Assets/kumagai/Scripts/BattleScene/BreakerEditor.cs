using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    TextAsset notesDatas;
    [SerializeField] private bool tmplastNotes;
    public static bool lastNotes;
    [SerializeField] private bool tmpcommandEnd;
    public static bool commandEnd;
    [SerializeField] private GameObject breakerBackGorund;
    [SerializeField]
    private GameObject SpeedObject;
    public static float NotesOKCount;
    public static bool NotesCreate=false;
    public static int BreakerGageCount=0;
    public static bool breakerGageMax;
    [SerializeField]
    private Image BreakerGageImage;
    [SerializeField]
    private GameObject ready;
    [SerializeField]
    private GameObject readyEff;
    [SerializeField]
    private ParticleSystem lightning;
    [SerializeField]
    private GameObject breakerChara;
    public List<GameObject> Chara;
    [SerializeField]
    private GameObject Judge;
    public static bool circleSet;
    [SerializeField]
    private GameObject circle;
    private Vector3 tmpSize;
    [SerializeField]
    private float scaleSize;
    [SerializeField]
    private GameObject light;
    [SerializeField]
    private ParticleSystem line;
    public static float allTime;
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
        NotesOKCount =0;
        tmpSize = circle.GetComponent<RectTransform>().localScale;
        BreakerGageImage.fillAmount=70f/70f;
        for(int i = 0; i < PlayerEditor.PlayerName.Length; i++) {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                Chara.Add(breakerChara.transform.GetChild(i).gameObject);
            }
        }
        light.SetActive(false);
        readyFlag=false;
    }

    void Update()
    {
        BreakerGageImage.fillAmount = BreakerGageCount / 70f;
        if(!breakerGageMax)
        {
            readyFlag=false;
        }
        if(BreakerGageImage.fillAmount>=1)
        {
            ready.SetActive(true);
            readyEff.SetActive(true);
            breakerGageMax=true;
            if(!readyFlag)
            { 
                 StartCoroutine(ReadyEffect());
            }
            readyFlag=true;
        }
        else
        {
            ready.SetActive(false);
           
        }
        if (SkillSelection.breakerFlag&&GameManager.state==GameManager.BattleState.breakerCommand)
        {
            Chara[SkillStorage.charaNumber].SetActive(true);
           
            CircleRotation();
            line.Play();
            Judge.SetActive(true);
            breakerChara.SetActive(true);
            StartCoroutine(CircleMove());
            breakerBackGorund.SetActive(true);
            notesDatas = Resources.Load<TextAsset>("Skill/" + skillName);
            List<string[]> csvdata = CsvReader(notesDatas);
            SkillStorage.CommandCount = csvdata.Count - 2;
            CommandController.Count = csvdata.Count - 2;
            if (commandDestroy == csvdata.Count - 2)
            {
                commandEnd = true;
            }

            if (!NotesCreate) //試験的に、Enterキー入力でインスペクターにアタッチした一つ目のスキルを実行
            {
                light.SetActive(true);
                Debug.Log(notesDatas.name);
                if(circleSet)
                { 
                  
                    StartCoroutine(NotesCreater(notesDatas));
                    NotesCreate =true;
                }
              
            }
        }
        if(GameManager.state==GameManager.BattleState.move)
        {
            line.Stop();
            circle.GetComponent<RectTransform>().localScale=new Vector3(0.3f,0.3f,0.3f);
            light.SetActive(false);
            if(CharaMoveGage.MoveChar[0].CompareTag("Player"))
            {
                Chara[SkillStorage.charaNumber].SetActive(false);
            }
            Judge.SetActive(false);
            breakerBackGorund.SetActive(false);
            breakerChara.SetActive(false);
               
            
            
        }
    }

    IEnumerator NotesCreater(TextAsset TAD) //引数に入力されたリストをノーツとして生成する関数
    {
        yield return new WaitForSeconds(0.5f);
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
            Debug.Log("138行目は"+notesDatas.name);
            List<string[]> data = CsvReader(notesDatas); //一行をまとめて格納する配列

            for (int j = 0; j < notesData[0].Length; ++j)
            {
                if (notesData[i][j] == (-1).ToString()) //-1が来たら関数終了
                {
                    yield break;
                }

                data[j] = notesData[i]; //一行を配列に格納
            }

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
            t = float.Parse(data[i][1]);　//二列目の値によってノーツが流れて来るまでの時間を決定
            switch (data[i][3])
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
                SpeedObject=Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //生成
                SpeedObject.GetComponent<Transform>().localScale=new Vector3(15,15,1);
                SpeedObject.transform.tag=data[i][3].ToString();
                SpeedObject=SpeedObject.transform.GetChild(0).gameObject;
                SpeedObject.name  = (float.Parse(data[i][2])).ToString();
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

   
    
    private IEnumerator CircleMove()
    {
       // tmpSize=circle.GetComponent<RectTransform>().localScale;
        Vector3 size=tmpSize;
        if(!circleSet)
        { 
            while(GameManager.state==GameManager.BattleState.breakerCommand)
            { 
                size= new Vector3(size.x - scaleSize*Time.deltaTime/allTime, size.y - scaleSize*Time.deltaTime/allTime, 0);
                circle.GetComponent<RectTransform>().localScale=size;
                yield return new WaitForSeconds(Time.deltaTime);
                circleSet = true;
            }
        }
    }

    private void CircleRotation()
    {
        Quaternion rot=circle.transform.rotation;
        circle.transform.Rotate(new Vector3(0,0,60*Time.deltaTime));
    }

    public static bool readyFlag=false;
    private IEnumerator ReadyEffect()
    {
       
            while(ready.activeSelf)
            {
            float alpha = 1;
            Vector3 size = ready.GetComponent<RectTransform>().localScale;
                while (alpha>0)
                {
                    size = new Vector3(size.x + scaleSize * Time.deltaTime , size.y + scaleSize * Time.deltaTime , 0);
                    alpha-=Time.deltaTime*2;
                    readyEff.GetComponent<Image>().color=new Color(1,1,1,alpha);
                    readyEff.GetComponent<RectTransform>().localScale = size;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            yield return new WaitForSeconds(0.5f);
        }
       

    }
}
