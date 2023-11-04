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
        
    }

    // Update is called once per frame
    void Update()
    {
        SkillSet();
        SkillSelect();
    }
    void SkillSet()//���ݍs�����Ă���L�����̃X�L����ݒ�
    {
        if (GameManager.state == GameManager.BattleState.skillSelect)
        {
            if (skills[0] == null)
            {
                for (int i = 0; i < skill.transform.childCount; i++)
                {
                    skills[i] = skill.transform.GetChild(i).gameObject;
                }
            }
            skill = CharaMoveGage.MoveChar[0].transform.Find("Skill").gameObject;
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
    void SkillSelect()//�X�L����I������Ƃ��̃v���C���[����̓��͏���
    {
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(SkillNumber<3)
            { 
                SkillNumber+=1;
            }
        }
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(SkillNumber>=0)
            { 
                SkillNumber-=1;
            }
        }
    }

    void SelectSkill()
    {

    }
}
