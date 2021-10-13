using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

    public GameObject pn_option;
    public GameObject sd_bgm;
    public GameObject sd_eff;
    public GameObject img_bgm;
    public GameObject img_eff;
    public Sprite[] sp_bgm = new Sprite[2];
    public Sprite[] sp_eff = new Sprite[2];

    public GameObject btn_move;
    public GameObject btn_change;

    private float temp_eff;
    private float temp_bgm;

    public static OptionManager instance;

    private OptionData optionData = new OptionData();

    public void EffChange(float value)
    {
        temp_eff = value;
        SetEff();
    }

    public void BgmChange(float value)
    {
        temp_bgm = value;
        SetBgm();
    }

    public void StartBtn()
    {
        #if UNITY_ANDROID
            Debug.Log("Android");
            btn_move.SetActive(true);
            btn_change.SetActive(true);
        #else
            Debug.Log("else");
        #endif
    }

    public void OnSaveBtn()
    {
        optionData.v_bgm = temp_bgm;
        optionData.v_eff = temp_eff;
        //Debug.Log(optionData.v_bgm);
        Setting();
        pn_option.SetActive(false);
        GameManager.instance.SaveOption(optionData);
    }

    public void OnBgmBtn()
    {
        if (sd_bgm.GetComponent<Slider>().value != 0)
        {
            SetSliderBgm(0);
            BgmChange(0);
        } else
        {
            SetSliderBgm(1);
            BgmChange(1);
        }
    }

    public void OnEffBtn()
    {
        if (sd_eff.GetComponent<Slider>().value != 0)
        {
            SetSliderEff(0);
            EffChange(0);
        } else
        {
            SetSliderEff(1);
            EffChange(1);
        }
    }

    public OptionData SaveOptionData()
    {
        return optionData;
    }
    public void Setting()
    {
        SoundManager.instance.SetVolume(optionData.v_eff);
        BgmManager.instance.SetVolume(optionData.v_bgm);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    public void LoadOptionData(OptionData data)
    {
        //dataManager를 통해 파일에 저장된 데이터 불러오기
        optionData = data;
        Setting();
        SetSliderBgm(optionData.v_bgm);
        SetSliderEff(optionData.v_eff);
        SetBgm();
        SetEff();
    }

    public void OnCanCelClick()
    {
        pn_option.SetActive(false);
    }

    public void SetSliderBgm(float bgm)
    {
        sd_bgm.GetComponent<Slider>().value = bgm;
    }

    public void SetSliderEff(float eff)
    {
        sd_eff.GetComponent<Slider>().value = eff;
    }

    public void SetEff()
    {
        int i = Mathf.CeilToInt(temp_eff);
        img_eff.GetComponent<Image>().sprite = sp_eff[i];
    }

    public void SetBgm()
    {
        int i = Mathf.CeilToInt(temp_bgm);
        img_bgm.GetComponent<Image>().sprite = sp_bgm[i];
    }

    public void OnPauseClick()
    {
        btn_change.SetActive(false);
        btn_move.SetActive(false);
    }

    public void OnPlayClick()
    {
        btn_change.SetActive(true);
        btn_move.SetActive(true);
    }
}
