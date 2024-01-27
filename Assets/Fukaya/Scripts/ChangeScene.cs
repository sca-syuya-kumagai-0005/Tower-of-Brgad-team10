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
    //キャラ選択へ
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("CharaSelect");
    }

    //キャラ選択へ戻る
    public void OnClickCSBackButton()
    {
        SceneManager.LoadScene("BattleScene");
    }

    //ランキングへ
    public void OnClickRankingButton()
    {
        SceneManager.LoadScene("RankingScene");
    }

    //設定へ
    public void OnClickOptionButton()
    {
        SceneManager.LoadScene("OptionScene");
    }

    //タイトルに戻る
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //ステージ選択へ
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
