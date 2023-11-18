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
        DamageCalculation();
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
                    Debug.Log("プレイヤーの攻撃値は"+attack);
                }
            }
        }
    }

    void DamageCalculation() //プレイヤーからエネミーへの攻撃の処理を行う
    {
        if(GameManager.state==GameManager.BattleState.move&&SkillSelection.skillSelect) {
            EnemyManager.EnemyInfo.Enemy_HP-=attack;
            float ehp =EnemyManager.EnemyInfo.Enemy_HP;
            EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.maxEnemyHP;
            GameManager.moveEnd=true;
            Debug.Log("攻撃をした");
        }
    }
}
