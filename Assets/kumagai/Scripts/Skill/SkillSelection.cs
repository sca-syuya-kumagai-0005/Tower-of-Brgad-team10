using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    private GameObject nowChar;
    private string[] skillName;
    public bool skillSelect;
    [SerializeField]
    private GameObject skill;
    [SerializeField]
    private GameObject[] skills=new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        SkillNumber=0;
    }

    // Update is called once per frame
    void Update()
    {
        SkillSet();
        SkillSelect();
        SelectSkill();
    }
    void SkillSet()//現在行動しているキャラのスキルを設定
    {
        if (GameManager.state == GameManager.BattleState.skillSelect)
        {
            skill = CharaMoveGage.MoveChar[0].transform.Find("Skill").gameObject;
            skill.SetActive(true);
            if (skills[0] == null)
            {
                for (int i = 0; i < skill.transform.childCount; i++)
                {
                    skills[i] = skill.transform.GetChild(i).gameObject;
                }
            }
           

        }
        if (GameManager.state == GameManager.BattleState.effect)
        {
            for (int i = 0; i < skill.transform.childCount; i++)
            {
                skills[i] = null;
            }
        }
    }

    private int SkillNumber;
    void SkillSelect()//スキルを選択するときのプレイヤーからの入力処理
    {
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(SkillNumber>0)
            { 
                SkillNumber-=1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(SkillNumber<3)
            { 
                SkillNumber+=1;
            }
        }
    }

    void SelectSkill()
    {
        for(int i=0;i<4;i++)
        {
             skills[SkillNumber].transform.localScale = new Vector3(1.5f, skills[SkillNumber].transform.localScale.y, 0);
            
            if(skills[i]!=skills[SkillNumber])
            {
                skills[i].transform.localScale = new Vector3(1.0f, skills[i].transform.localScale.y, 0);
            }
        }

       
    }
}
