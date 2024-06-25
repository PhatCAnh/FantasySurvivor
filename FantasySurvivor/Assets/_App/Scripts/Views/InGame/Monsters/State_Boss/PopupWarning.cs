using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupWarningBossing : MonoBehaviour
{
    public TMP_Text popupText; 

    private GameObject window;
    private Animator popupAnimator;

    private void Init()
    {
        window = transform.GetChild(0).gameObject; 
        popupAnimator = window.GetComponent<Animator>();  
        window.SetActive(false); 
    }

    public void ShowPopup(string text)
    {
        window.SetActive(true); 
        popupText.text = text; 
        popupAnimator.Play("PopupWarningBoss"); 
    }



    public void HidePopup()
    {
        Destroy(gameObject);
    }

    public void ShowPopupOnSpawn(string text)
    {
        ShowPopup(text);
    }
}