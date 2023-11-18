using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBall : MonoBehaviour
{
    [SerializeField] GameObject[] _ballPrefabs;

    void Start()
    {
        int select = PlayerPrefs.GetInt("BallSelect", 0);

        GameObject ballobj = _ballPrefabs[select];

        Instantiate(ballobj, transform.position, Quaternion.identity);
    }

    public void BallSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }
}