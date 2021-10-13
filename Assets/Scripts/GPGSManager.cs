using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour {

    public Text stateText;
    private Action<bool> signInCallback;

    private void Awake()
    {
        //안드로이드 빌더 초기화
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).RequestIdToken().Build();
        //Debug.Log(config);
        //PlayGamesPlatform.InitializeInstance(config);

        //구글 플레이 로그를 확인할려면 활성화
        //PlayGamesPlatform.DebugLogEnabled = true;

        //구글 플레이 활성화
        //PlayGamesPlatform.Activate();

        //Callback 함수 정의
        signInCallback = (bool success) =>
        {
            if (success)
                stateText.text = "SignIn success!";
            else
                stateText.text = "SignIn Fail!";
        };
    }

    //로그인
    public void SignIn()
    {
        Debug.Log("signin");
        //로그아웃 상태면 호출
        //if (PlayGamesPlatform.Instance.IsAuthenticated() == false)
            //PlayGamesPlatform.Instance.Authenticate(signInCallback);
    }

    //로그아웃
    public void SignOut()
    {
        //if (PlayGamesPlatform.Instance.IsAuthenticated() == true)
        {
            stateText.text = "Bye~";
            //PlayGamesPlatform.Instance.SignOut();
        }
    }

}
