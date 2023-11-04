using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //キャラ選択へ
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("CharaSelectScene");
    }

    //キャラ選択へ戻る
    public void OnClickCSBackButton()
    {
        SceneManager.LoadScene("CharaSelectScene");
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
}
