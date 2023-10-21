using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickRankingButton()
    {
        SceneManager.LoadScene("RankingScene");
    }
    public void OnClickOptionButton()
    {
        SceneManager.LoadScene("OptionScene");
    }
}
