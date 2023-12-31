using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowCharaSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nowCharacter;
    [SerializeField]
    private List<GameObject> nextCharacter;
    [SerializeField]
    GameObject nowCharaSponePos;
    [SerializeField]
    private GameObject MoveChara;
    [SerializeField]
    private GameObject nextCharaSponePos;
    public static GameObject nextChara;
    public static GameObject nowChara;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++) {
            GameObject now=Resources.Load<GameObject>("NowChara/"+PlayerEditor.PlayerName[i]);
            GameObject next=Resources.Load<GameObject>("NextChara/"+PlayerEditor.PlayerName[i]);
            Debug.Log(next.name);
            nowCharacter.Add(Instantiate(now,nowCharaSponePos.transform.position,Quaternion.identity,MoveChara.transform));
            nextCharacter.Add(Instantiate(next,nextCharaSponePos.transform.position,Quaternion.identity,MoveChara.transform));
          
        }
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            nowCharacter[i].SetActive(false);
            nextCharacter[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.BattleState.start!=GameManager.BattleState.flagReSet)
        { 
            for(int i=0;i<PlayerEditor.PlayerName.Length;i++) 
            {
                if(SkillStorage.charaNumber==i) 
                {
                    nowCharacter[i].SetActive(true);
                    nowChara =nowCharacter[i];
                } 
                if(CharaMoveGage.MoveChar[0]==null)
                {
                    nowCharacter[i].SetActive(false);
                }

                if(SkillStorage.nextCharaNumber==i)
                {
                    nextCharacter[i].SetActive(true);
                    nextChara=nextCharacter[i];
                    Debug.Log("通過しているよ");
                }
            }
        }
      
    }
}
