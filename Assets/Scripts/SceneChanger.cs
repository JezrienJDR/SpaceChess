using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void OnBack()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void SignUp()
    {
        SceneManager.LoadScene("PlayerRegScene");
    }
}
