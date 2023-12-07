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
    private float speed;   //���������R�}���h�̈ړ����x
    public static float s;     //�C���X�y�N�^�[����A�^�b�`���ꂽCSV�t�@�C�����i�[���铮�I�z��//���X�g�ɕϊ��������̃f�[�^���i�[���铮�I�z��
    [SerializeField]
    private GameObject[] notes;        //�R�}���h�̉摜���i�[����z��
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
        skillName="�X���b�V��";
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
    IEnumerator NotesCreater() //�����ɓ��͂��ꂽ���X�g���m�[�c�Ƃ��Đ�������֐�
    {
        for(int i=0;i<skillCommandCount;i++)
        { 
            if(i==skillCommandCount-1)
            {
                lastNotes=true;
            }
            List<string[]> data = CsvReader(notesDatas);
            NotesType c = NotesType.i;
      
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
        
            float t = Random.Range(minWait,maxWait);�@//���ڂ̒l�ɂ���ăm�[�c������ė���܂ł̎��Ԃ�����
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
            }�@     //�O��ڂ̒l�ɂ���ăm�[�c�̌���������
            yield return new WaitForSeconds(t); //���ڂ̒l�������ҋ@
            if(c!=NotesType.i)
            { 
                speedManager=Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //����
                speedManager=speedManager.transform.GetChild(0).gameObject;
                speedManager.name=(float.Parse(data[i][2])).ToString();
            }
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
    //public static IEnumerator good(GameObject obj)
    //{
    //    tmpgoodText.SetActive(goodflg);
    //    yield return 1;
    //    Destroy(obj);
    //}

}


