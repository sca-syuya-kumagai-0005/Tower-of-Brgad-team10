using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamCharacter : MonoBehaviour
{
    [SerializeField]
    private string[] charaName=new string[4];
    [SerializeField]
    private int[] charaNumbars;
    [SerializeField]
    private GameObject[] sponePos;
    [SerializeField]
    private GameObject Character;
    [SerializeField]
    private GameObject notSelectIcon;
    [SerializeField]
    private int selectCharaNumber;
    private string[] chara;
    [SerializeField]
    private GameObject characters;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            
        SelectCharaJudge();
        
    }
    [SerializeField]
    private int selectCharaNumberCorrection;
    [SerializeField]
    private int num;

    private void SelectCharaJudge()
    {
        if(CharaSelectManager.charaSelectScreen)
        {
            selectCharaNumber=selectCharaNumberCorrection+num;
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (num < 3)
                {
                    num++;
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (num > 0)
                {
                    num--;
                }
            }

            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(selectCharaNumberCorrection==4)
                {
                    selectCharaNumberCorrection=0;
                }
            }
            if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(selectCharaNumberCorrection==0)
                {
                    selectCharaNumberCorrection=4;
                }
            }
           CharaInstantiate();
        }

        for(int i=0;i<notSelectIcon.transform.childCount;i++)
        {
            GameObject obj = notSelectIcon.transform.GetChild(i).gameObject;
            if (i==selectCharaNumber)
            {
                obj.SetActive(false);
            }
            else
            {
               obj.SetActive(true);
            }
        }
    }
    [SerializeField]
    private int count;
    private void CharaInstantiate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            if(count%2==0)
            {
                GameObject selectChara = characters.transform.GetChild(selectCharaNumber).gameObject;
                charaName[CharaSelectManager.selectSlot] = selectChara.name;
                GameObject obj = Resources.Load<GameObject>("PartyCharacter/" + charaName[CharaSelectManager.selectSlot]);
                charaNumbars[CharaSelectManager.selectSlot]=selectCharaNumber;
                Instantiate(obj, sponePos[CharaSelectManager.selectSlot].transform.position, Quaternion.identity, Character.transform);
                CharaSelectManager.charaSelectScreen = false;
            }
            else
            {
                CharaSelectManager.charaSelectScreen=true;
            }
            
        }
           
        
    }
}
