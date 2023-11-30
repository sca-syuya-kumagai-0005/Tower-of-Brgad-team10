using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //�L�����I����
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("CharaSelectScene");
    }

    //�L�����I���֖߂�
    public void OnClickCSBackButton()
    {
        SceneManager.LoadScene("CharaSelectScene");
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
}
