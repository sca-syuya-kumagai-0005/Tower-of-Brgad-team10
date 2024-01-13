using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSponer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SponePos;
    [SerializeField]
    private GameObject breakerSponePos;
    private GameObject sponeChar;
    [SerializeField]
    private GameObject partyChar;
    [SerializeField]
    private GameObject SkillSponer;
    private GameObject SponeChara;
    private GameObject breakerChara;
    [SerializeField]
    private GameObject breakerBackGround;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            sponeChar = Resources.Load<GameObject>("Prefabs/" + PlayerEditor.PlayerName[i]);
            SponeChara=Instantiate(sponeChar,SponePos[i].transform.position,Quaternion.identity,partyChar.transform);
            SponeChara.transform.GetChild(3).position=SkillSponer.transform.position;
            SponeChara.gameObject.name = SponeChara.gameObject.name.Replace("(Clone)", "");
            breakerChara=Resources.Load<GameObject>("Breaker/"+PlayerEditor.PlayerName[i]);
            Instantiate(breakerChara,breakerSponePos.transform.position,Quaternion.identity,breakerBackGround.transform);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
