using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamCharacter : MonoBehaviour
{
    [SerializeField]
    private List<string> charaName;
    [SerializeField]
    private GameObject[] sponePos;
    [SerializeField]
    private GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj= Resources.Load<GameObject>("PartyCharacter/" + charaName[0]);
        Instantiate (obj,sponePos[0].transform.position,Quaternion.identity,Character.transform);
    }
}
