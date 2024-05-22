using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image OnIcon;
    [SerializeField] Image OnffIcon;
    private bool muted = false;
   void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }
    public void OnButtonPress()
    {
        if (muted == false) 
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        if(muted == false) 
        {
            OnIcon.enabled = true;
            OnffIcon.enabled = false;
        }
        else
        {
            OnIcon.enabled = false;
            OnffIcon.enabled = true;
        }
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

}
