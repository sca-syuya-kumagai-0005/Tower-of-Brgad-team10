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
    [SerializeField]
    private GameObject[] skills=new GameObject[5];
    private int skillPosX;
    private float[] farstSkillPosX=new float[5];
    private float[] farstSkillPosY=new float[5];
    private Vector3[] pos=new Vector3[5];
    private float f;
    [SerializeField]
    private GameObject skillsi;
    [SerializeField]
    private GameObject skillsNumber;
    [SerializeField]
    private int skillCount;
    // Start is called before the first frame update
    void Start()
    {
        SkillNumber=0;
        f=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(BreakerEditor.breakerGageMax)
        {
            skillCount=5;
        }
        else
        {
            skillCount=4;
        }
        f+=Time.deltaTime*5;
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
            SkillPosEffect();
            if(Input.GetKeyDown(KeyCode.Return)&&GameManager.state==GameManager.BattleState.skillSelect)
            {
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
                for (int i = 0; i < skillCount; i++)
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
            for (int i = 0; i < skillCount; i++)
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
            f=0;
            if(SkillNumber>0)
            {
                MoveTextController.moveTextFlag = false;
                SkillNumber -=1;
            }
            else if(SkillNumber==0)
            {
                MoveTextController.moveTextFlag = false;
                SkillNumber =skillCount-1;
                
            }
        }
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            f=0;
            if(SkillNumber<skillCount-1)
            {
                SkillNumber +=1;
            }
            else if(SkillNumber>=skillCount-1)
            {
                SkillNumber =0;
            }
        }
    }
    //現在選択されているスキルの設定
    void SelectSkill()
    {

        for(int i=0;i<skillCount;i++)
        {
             skills[SkillNumber].transform.position = new Vector3(pos[SkillNumber].x+0.3f, skills[SkillNumber].transform.position.y, 0);
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
            for(int i=0;i < skillCount; i++)
            {
                skills[i].transform.position=pos[i];
            }
        }
    }
    void SkillPosEffect()
    {
        for(int i=0;i<skillCount;i++)
        {
            skillsNumber = skills[SkillNumber];
            if (skills[SkillNumber]==skills[i])
            {
                skills[i].transform.position=new Vector3(skills[i].transform.position.x+Mathf.Cos(f)/5,skills[i].transform.position.y,skills[i].transform.position.z);
            }
            else
            { 
                skills[i].transform.position = pos[i];
            }
        }
        
    }
}
