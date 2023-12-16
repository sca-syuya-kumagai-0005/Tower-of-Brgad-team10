using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoveTextController;

public class SkillSelection : MonoBehaviour
{
    private GameObject nowChar;
    [SerializeField]
    private GameObject mainCanvas;
    private string[] skillName;
    public static bool skillSelect;
    public static bool breakerFlag;
    [SerializeField]
    private GameObject skill;
    private GameObject[] skills=new GameObject[5];
    private int skillPosX;
    private float[] farstSkillPosX=new float[5];
    private float[] farstSkillPosY=new float[5];
    private Vector3[] pos=new Vector3[5];
    // Start is called before the first frame update
    void Start()
    {
        SkillNumber=0;

    }

    // Update is called once per frame
    void Update()
    {
        SkillPosReset();
        SkillSet();
        if(SkillNumber==4&&GameManager.state==GameManager.BattleState.skillSelect)
        {
            breakerFlag=true;
        }
        else if( GameManager.state == GameManager.BattleState.skillSelect)
        {
            breakerFlag=false;
        }
        if (GameManager.state==GameManager.BattleState.skillSelect)
        { 

            SkillSelect();
            SelectSkill();
            if(Input.GetKeyDown(KeyCode.Return)&&GameManager.state==GameManager.BattleState.skillSelect)
            {
                Debug.Log(skills[SkillNumber].name);
               // NotesEditor.skillName = skills[SkillNumber].name;
                skillSelect=true;
                if(SkillNumber==4)
                {

                }
            }
        }
    }
    void SkillSet()//現在行動しているキャラのスキルを画面左側に設定
    {
        if (GameManager.state == GameManager.BattleState.skillSelect&&CharaMoveGage.MoveChar[0].name!= "EnemyMoveGage")
        {
            skill = CharaMoveGage.MoveChar[0].transform.Find("Skill").gameObject;
            skill.SetActive(true);
            if (skills[0] == null)
            {
                Debug.Log(skills.Length);
                for (int i = 0; i < skill.transform.childCount; i++)
                {
                    skills[i] = skill.transform.GetChild(i).gameObject;
                    pos[i]=skills[i].transform.position;
                }
            }
        }
        else if(GameManager.state==GameManager.BattleState.flagReSet&&!EnemyMove.enemyMove)
        {
            skill.SetActive(false);
        }
        if (GameManager.state == GameManager.BattleState.flagReSet&&!EnemyMove.enemyMove)
        {
            for (int i = 0; i < skill.transform.childCount; i++)
            {
                skills[i] = null;
            }
        }
    }

    public static int SkillNumber;
    void SkillSelect()//スキルを選択するときのプレイヤーからの入力処理
    {
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(SkillNumber>0)
            {
                MoveTextController.moveTextFlag = false;
                SkillNumber -=1;
            }
            else if(SkillNumber==0)
            {
                MoveTextController.moveTextFlag = false;
                SkillNumber =skills.Length-1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(SkillNumber<skills.Length-1)
            {
                SkillNumber +=1;
            }
            else if(SkillNumber>=skills.Length-1)
            {
                SkillNumber =0;
            }
        }
    }
    //現在選択されているスキルの設定
    void SelectSkill()
    {

        for(int i=0;i<skills.Length;i++)
        {
             skills[SkillNumber].transform.position = new Vector3(pos[SkillNumber].x+1, skills[SkillNumber].transform.position.y, 0);
            if(skills[i]!=skills[SkillNumber])
            {
                skills[i].transform.position = new Vector3(pos[i].x, skills[i].transform.position.y, 0);
            }
        }
    }
    void SkillPosReset()
    {
        if(GameManager.state==GameManager.BattleState.move&&!EnemyMove.enemyMove)
        {
            for(int i=0;i < skill.transform.childCount; i++)
            {
                skills[i].transform.position=pos[i];
            }
        }
    }
}
