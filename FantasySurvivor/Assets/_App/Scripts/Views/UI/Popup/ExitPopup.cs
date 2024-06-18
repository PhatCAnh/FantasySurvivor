using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject exitGamePopupPrefab; // Prefab của popup thoát game
    private GameObject currentPopup;

    void Start()
    {
        exitButton.onClick.AddListener(OnExitButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Destroy(gameObject);
    }

    private void OnStayButtonClick()
    {
        // Đóng popup khi nhấn nút Stay
        Destroy(gameObject);
    }

    private void OnExitButtonClick()
    {
        // Thoát game khi nhấn nút Exit
        Debug.Log("Exit game");
        Application.Quit();
#if UNITY_EDITOR
        // Chỉ dùng cho Unity Editor để thoát play mode
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenExitGamePopup()
    {
        // Hủy popup hiện tại nếu có
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }

        // Tạo mới và lưu trữ popup thoát game
        currentPopup = Instantiate(exitGamePopupPrefab, transform);
    }

    private void OnExitGameButtonClick()
    {
        OpenExitGamePopup();
        Debug.Log("Exit Game button clicked");
    }
}
