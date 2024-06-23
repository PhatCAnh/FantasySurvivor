using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupWarning : MonoBehaviour
{
    public TMP_Text popupText;  // Text để hiển thị thông báo

    private GameObject window;  // Cửa sổ chứa popup
    private Animator popupAnimator;  // Animator để quản lý hoạt ảnh của popup

    private bool isActive;  // Biến để kiểm tra trạng thái của popup

    private void Init()
    {
        window = transform.GetChild(0).gameObject;  // Lấy đối tượng con đầu tiên làm cửa sổ popup
        popupAnimator = window.GetComponent<Animator>();  // Lấy component Animator
        window.SetActive(false);  // Bắt đầu với popup ẩn đi
    }

    // Phương thức để hiển thị popup với một thông báo
    public void ShowPopup(string text, float timeshow)
    {
        isActive = true;
        window.SetActive(true);  // Hiển thị cửa sổ popup
        popupText.text = text;  // Cập nhật văn bản thông báo
        popupAnimator.Play("PopupWarningBoss");  // Chạy hoạt ảnh hiển thị popup
        StartCoroutine(HideAfterDelay(5f));  // Tự động ẩn popup sau 5 giây
    }

    // Phương thức để bắt đầu quá trình hiển thị popup khi được tạo ra
    public void ShowPopupOnSpawn(string text, float timeshow)
    {
        ShowPopup(text, timeshow);
    }

    // Coroutine để ẩn popup sau một khoảng thời gian trì hoãn
    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Chờ trong thời gian đã định
        window.SetActive(false);  // Ẩn cửa sổ popup
        isActive = false;  // Cập nhật trạng thái của popup
    }


}