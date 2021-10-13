using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
// 광고 사용하기.

public class AdManager : MonoBehaviour {

    // 광고 옵션값
    //ShowOptions m_cShowOptions = new ShowOptions();
    // 광고보기 버튼.(광고 준비가 안될 때에 비활성 시키려고 들고있음)
    // 보상 테스트
    int m_nGold = 0;
    // 현재 광고상태   
    //ShowResult m_eResult = ShowResult.Failed;
    // 혹시 버튼 클릭했는데, 콜백이 안넘어 오는지 체크위함.
    bool m_bClicked = false;
    // 광고 클릭 카운트
    int m_nClickCount = 0;

    void Awake()
    {
        // 광고 최초 초기화 해준다.
        // Initialize(광고 번호, 테스트 모드인가)
        //Advertisement.Initialize("2798955", true);
        // 광고 완료할 때 처리할 콜백함수를 넣어준다.
        // System.Action<showresult> 형태
       // m_cShowOptions.resultCallback = OnAdsShowResultCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        //// 광고가 사용가능 한지 여부를 체크한다.
        //// Adverisement.IsReady(광고 아이디)
        //// 아까 생성한 곳 보면 광고 아이디가 있습니다.
        //// 여러 광고중 하고자 하는 아이디를 넣어주면 됩니다.
        //if (Advertisement.IsReady("rewardedVideoZone"))
        //{
        //    m_gObjEventButton.SetActive(true);
        //}
        //else
        //{
        //    m_gObjEventButton.SetActive(false);
        //}
    }

    // 광고 버튼 클릭을 눌렀을 때 발동.
    public void OnClickButton()
    {
        m_bClicked = true;
        m_nClickCount++;
        // 해당 아이디의 광고를 보여준다.
        //Advertisement.Show("rewardedVideoZone", m_cShowOptions);
    }
    // 광고 보기 완료 후 호출되는 콜백함수
    //void OnAdsShowResultCallBack(ShowResult _result)
   // {
       // m_eResult = _result;
        // 정상적으로 완료 될 때에만 처리하도록 하겠습니다.
        //if (_result == ShowResult.Finished)
            //m_nGold += 50;

        // 정상적으로 CallBack이 들어왔다면, 클릭여부 false.
       // m_bClicked = false;
  //  }

    void OnGUI()
    {
        // 현재 보상받은거.
       // GUI.Label(new Rect(10, 50, 500, 30), "Gold : " + m_nGold.ToString());
        // 콜백 결과
        //GUI.Label(new Rect(10, 100, 500, 30), "Result : " + m_eResult.ToString());
        // 광고 클릭 카운트와, 클릭상태 여부 표시.
       // GUI.Label(new Rect(150, 100, 500, 30), "ClickCount : " + m_nClickCount.ToString()
                //+ ", Clicked : " + m_bClicked.ToString());
    }
}
