using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioClip[] boom_beat = new AudioClip[8];
    public AudioClip drop_beat;
    public AudioClip[] move_beat = new AudioClip[6];
    public AudioClip hold_beat;
    public AudioClip up_beat;

    [SerializeField]
    public AudioSource audioSource;

    private float v_eff;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        } else if (SoundManager.instance != this)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void PlayDrop()
    {

        //Debug.Log(audioSource.mute);
        if (audioSource.mute == false)
            audioSource.PlayOneShot(drop_beat);
    }

    public void PlayMove()
    {
        if (audioSource.mute == false)
            audioSource.PlayOneShot(move_beat[Random.Range(0, move_beat.Length)]);
    }

    public void PlayBoom(int combo)
    {
        if (audioSource.mute == false)
        {
            if (combo >= boom_beat.Length)
                combo = boom_beat.Length - 1;
            audioSource.PlayOneShot(boom_beat[combo]);
        }
    }

    public void PlayHold()
    {
        if (audioSource.mute == false)
            audioSource.PlayOneShot(hold_beat);
    }

    public void PlayUp()
    {
        if (audioSource.mute == false)
            audioSource.PlayOneShot(up_beat);
    }

    public void SetVolume(float v)
    {
        v_eff = v;
        audioSource.volume = v_eff;
    }
}
