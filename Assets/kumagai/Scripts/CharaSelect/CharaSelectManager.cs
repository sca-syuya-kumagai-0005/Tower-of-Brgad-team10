using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class CharaSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slotSelect;
    public static int selectSlot;//デバッグ用にパブリックにしている
    [SerializeField]
    private GameObject[] sceneButtonSelect;
    private bool slot;
    private int sceneSelect;
    public static bool charaSelectScreen;
    [SerializeField]
    private GameObject charaSelectBackGround;
    public static int charaCount;

    // Start is called before the first frame update
    void Start()
    {
        slot=true;
        FloarManager.nowFloar=0;
        sceneSelect=1;
    }

    // Update is called once per frame
    void Update()
    {
        CorsolManager();
        charaSelectBackGround.SetActive(charaSelectScreen);
        
    }

    void CorsolManager()
    {
        if(!charaSelectScreen)
        { 
            if(slot)
            { 
                sceneSelect=1;//キャラ編成が終わった後に下もしくは上を押すとすぐに次へボタンにカーソルが移動するようにするための処理
                if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if(selectSlot<3) 
                    {
                        selectSlot++;
                    }

                    else if(selectSlot==3)
                    {
                        selectSlot=0;
                    }
                }

                if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)) 
                {
                    if(selectSlot>0) 
                    {
                        selectSlot--;
                    }

                    else if(selectSlot==0) 
                    {
                        selectSlot=3;
                    }
                }
                for(int i = 0; i < 4; i++) {
                    if(i == selectSlot) {
                        slotSelect[i].SetActive(true);
                    } else {
                        slotSelect[i].SetActive(false);
                    }
                }
                for(int i=0;i<2;i++) {
                    sceneButtonSelect[i].SetActive(false);
                }
            } 
            else 
            {
                selectSlot=0;
                if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) 
                {
                    if(sceneSelect==0) 
                    {
                        sceneSelect++;
                    }
                    else 
                    {
                        sceneSelect--;
                    }
                
                }
                for(int i=0;i<4;i++) 
                {
                    slotSelect[i].SetActive(false);
                }
                for(int i=0;i<2;i++)
                {
                    if(i == sceneSelect) 
                    { 
                        sceneButtonSelect[i].SetActive(true);
                    }
                    else 
                    {
                        sceneButtonSelect[i].SetActive(false);
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (slot)
                {
                    if(selectSlot<2)
                    {
                        sceneSelect=0;
                    }
                    else
                    {
                        sceneSelect=1;
                    }
                }
                slot =!slot;
            }

            if(Input.GetKeyDown(KeyCode.Return) && !charaSelectScreen&&slot) 
            {
                charaSelectScreen = true;
            }
            if(Input.GetKeyDown(KeyCode.Return)&&!slot) {
                if(sceneSelect==1) {
                    for(int i=0;i<4;i++) {
                        if(TeamCharacter.charaName[i]!=null&&TeamCharacter.charaName[i]!="") {
                            charaCount++;
                        }
                    }
                    if(charaCount==4)
                    {
                        SceneManager.LoadScene("FakeLoad");
                    }
                   else
                    {
                        charaCount=0;
                    }
                }
            }
        }
    }
  
}
