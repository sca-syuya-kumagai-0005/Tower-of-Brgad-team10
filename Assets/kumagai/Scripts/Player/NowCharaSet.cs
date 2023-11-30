using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowCharaSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nowCharacter;
    GameObject nowchar;
    [SerializeField]
    GameObject nowCharaSponePos;
    [SerializeField]
    private GameObject MoveChara;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++) {
            nowchar=Resources.Load<GameObject>("NowChara/"+PlayerEditor.PlayerName[i]);
            nowCharacter.Add(Instantiate(nowchar,nowCharaSponePos.transform.position,Quaternion.identity,MoveChara.transform));
            nowCharacter[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++) 
        {
            if(SkillStorage.charaNumber==i) 
            {
                nowCharacter[i].SetActive(true);
            } 
            else if(SkillStorage.charaNumber!=-1)
            {
                nowCharacter[i].SetActive(false);
            }
        }
      
    }
}
