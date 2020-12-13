using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{

    public GameObject main;
    public GameObject signup;

    public InputField mainUsername;
    public InputField mainPassword;

    public InputField signupUsername;
    public InputField signupPassword;

    NetworkClient client;

    private void Start()
    {
        client = FindObjectOfType<NetworkClient>();
    }

    public void OnPlay()
    {
        string username = mainUsername.text;
        string password = mainPassword.text;

        client.SignIn(username, password);
    }

    public void StartGame(string blackOrWhite)
    {
        FindObjectOfType<Board>().Begin(blackOrWhite);
    }

    public void OnBack()
    {
        main.SetActive(true);
        signup.SetActive(false);
       
    }

    public void SignUp()
    {
        signup.SetActive(true);
        main.SetActive(false);
    }

    public void Register()
    {
        string username = signupUsername.text;
        string password = signupPassword.text;

        client.SignUp(username, password);
    }
}
