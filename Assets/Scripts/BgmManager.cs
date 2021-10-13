using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmManager : MonoBehaviour {

    public static BgmManager instance;

    public AudioSource audioSource;
    public float originalPitch = 0.8f;
    public float updatePitch = .05f;

    private float v_bgm;

    // Use this for initialization
    void Awake () {
		if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(this);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

    }
	
	// Update is called once per frame
	void Update () {
        if (SoundManager.instance.audioSource.isPlaying == true)        {
            audioSource.volume = 0.7f * v_bgm;
        } else
        {
            audioSource.volume = 1f * v_bgm;
        }
    }

    public void GetStart()
    {
        audioSource.Play();
        audioSource.pitch = originalPitch;
        if (GameManager.instance.level != 1)
        {
            float pitch = originalPitch + (updatePitch * (GameManager.instance.level - 1));
            audioSource.pitch = pitch;
        }
    }

    public void UpdateLevel(int level)
    {
        float pitch = originalPitch + (updatePitch * (level - 1));
        audioSource.pitch = pitch;
    }

    public void SetVolume(float v)
    {
        v_bgm = v;
    }
}
