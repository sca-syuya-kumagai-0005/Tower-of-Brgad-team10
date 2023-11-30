using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSponer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SponePos;
    private GameObject sponeChar;
    [SerializeField]
    private GameObject partyChar;
    [SerializeField]
    private GameObject SkillSponer;
    private GameObject SponeChara;
    // Start is called before the first frame update
    void Awake()
    {

        for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            sponeChar = Resources.Load<GameObject>("Prefabs/" + PlayerEditor.PlayerName[i]);
            SponeChara=Instantiate(sponeChar,SponePos[i].transform.position,Quaternion.identity,partyChar.transform);
            SponeChara.transform.GetChild(3).position=SkillSponer.transform.position;
            SponeChara.gameObject.name = SponeChara.gameObject.name.Replace("(Clone)", "");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
