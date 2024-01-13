using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BreakerEditor : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas; //�L�����o�X
    [SerializeField]
    private float speed;   //���������R�}���h�̈ړ����x
    public static float s;     //�C���X�y�N�^�[����A�^�b�`���ꂽCSV�t�@�C�����i�[���铮�I�z��//���X�g�ɕϊ��������̃f�[�^���i�[���铮�I�z��
    [SerializeField]
    private GameObject[] notes;        //�R�}���h�̉摜���i�[����z��
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
    public static int BreakerGageCount;
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
        BreakerGageCount=70;
        NotesOKCount =0;
        tmpSize = circle.GetComponent<RectTransform>().localScale;
        BreakerGageImage.fillAmount=70f/70f;
        for(int i = 0; i < PlayerEditor.PlayerName.Length; i++) {
            Chara.Add(breakerChara.transform.GetChild(i).gameObject);
        }
        light.SetActive(false);
        
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

            if (!NotesCreate) //�����I�ɁAEnter�L�[���͂ŃC���X�y�N�^�[�ɃA�^�b�`������ڂ̃X�L�������s
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

    IEnumerator NotesCreater(TextAsset TAD) //�����ɓ��͂��ꂽ���X�g���m�[�c�Ƃ��Đ�������֐�
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
            Debug.Log("138�s�ڂ�"+notesDatas.name);
            List<string[]> data = CsvReader(notesDatas); //��s���܂Ƃ߂Ċi�[����z��

            for (int j = 0; j < notesData[0].Length; ++j)
            {
                if (notesData[i][j] == (-1).ToString()) //-1��������֐��I��
                {
                    yield break;
                }

                data[j] = notesData[i]; //��s��z��Ɋi�[
            }

            switch (data[i][0]) //�z��̈�ڂɓ����Ă��镶������Đ�������R�}���h������
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

                case "��":
                    c = NotesType.L;
                    break;

                case "��":
                    c = NotesType.U;
                    break;

                case "��":
                    c = NotesType.R;
                    break;

                case "��":
                    c = NotesType.D;
                    break;
            }�@�@�@�@//���ڂ̒l�ɂ���ăm�[�c�̎�ނ�����
            t = float.Parse(data[i][1]);�@//���ڂ̒l�ɂ���ăm�[�c������ė���܂ł̎��Ԃ�����
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
            }�@     //�O��ڂ̒l�ɂ���ăm�[�c�̌���������
            yield return new WaitForSeconds(t); //���ڂ̒l�������ҋ@
                SpeedObject=Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //����
                SpeedObject.transform.tag=data[i][3].ToString();
                SpeedObject=SpeedObject.transform.GetChild(0).gameObject;
                SpeedObject.name  = (float.Parse(data[i][2])).ToString();
        }
    }
    private List<string[]> CsvReader(TextAsset csvData) //�����ɓ��͂���CSV�t�@�C�������X�g�ɕϊ�����֐�
    {
        List<string[]> skillDatas = new List<string[]>();
        StringReader reader = new StringReader(csvData.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            skillDatas.Add(line.Split(','));
        }

        string[] data = new string[skillDatas[0].Length]; //��s���܂Ƃ߂Ċi�[����z��

        for (int i = 1; i != -1; ++i) //CSV�t�@�C���Ō�܂Ń��[�v
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
            Debug.Log("BBBBAAA");
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
