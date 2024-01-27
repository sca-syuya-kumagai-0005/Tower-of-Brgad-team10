using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject StartSelect;
    [SerializeField]    
    private GameObject EndSelect;
    //�L�����I����
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("CharaSelect");
    }

    //�L�����I���֖߂�
    public void OnClickCSBackButton()
    {
        SceneManager.LoadScene("BattleScene");
    }

    //�����L���O��
    public void OnClickRankingButton()
    {
        SceneManager.LoadScene("RankingScene");
    }

    //�ݒ��
    public void OnClickOptionButton()
    {
        SceneManager.LoadScene("OptionScene");
    }

    //�^�C�g���ɖ߂�
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //�X�e�[�W�I����
    public void OnClickOStageSelectButton()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    private int EndorStart=0;

    private void Start()
    {
        EndorStart=0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            EndorStart++;
        }
        EndSelect.SetActive(EndorStart%2==1);
        StartSelect.SetActive(EndorStart%2==0);
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (EndorStart % 2 == 1)
            {
                GameEnd.EndGame();
            }
            else
            {
                OnClickStartButton();
            }
        }
    }
}
