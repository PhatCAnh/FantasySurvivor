using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupWarningBossing : MonoBehaviour
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
    public void ShowPopup(string text)
    {
        window.SetActive(true);  // Hiển thị cửa sổ popup
        popupText.text = text;  // Cập nhật văn bản thông báo
        popupAnimator.Play("PopupWarningBoss");  // Chạy hoạt ảnh hiển thị popup
    }


    // Phương thức để ẩn popup
    public void HidePopup()
    {
        // Hủy đối tượng PopupWarning sau khi ẩn
        Destroy(gameObject);
    }

    // Phương thức để bắt đầu quá trình hiển thị popup khi được tạo ra
    public void ShowPopupOnSpawn(string text)
    {
        ShowPopup(text);
    }
}