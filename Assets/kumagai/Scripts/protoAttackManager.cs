using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoAttackManager : MonoBehaviour
{
    [SerializeField]private GameObject partyCharacter;
    [SerializeField]private GameObject moveCharacter;
    private int attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharaSet();
        StatusReference();
    }

    private void MoveCharaSet() 
    {
        if(GameManager.state==GameManager.BattleState.skillSelect) {
            moveCharacter = CharaMoveGage.MoveChar[0];
        }

    }

    private void StatusReference() 
    {
        if(GameManager.state == GameManager.BattleState.skillSelect) {
            for(int i=0;i<4;i++) {
                if(partyCharacter.transform.GetChild(i).gameObject==moveCharacter) {
                    Debug.Log("OK");
                    attack=PlayerEditorManager.PlayerInfo.Player_ATK[i];
                    Debug.Log(attack);
                }
            }
        }
    }
}
