using System;
using System.Collections;
using System.Collections.Generic;

using ArbanFramework;
using ArbanFramework.MVC;

using UnityEngine;

namespace MR {
    public class Door : View<GameApp> {

        private GameController _gameController => Singleton<GameController>.instance;

        private GameObject popup;

        private void Update() {
            if (_gameController.character == null || _gameController.isStop) return;
            var distance = Vector2.Distance(_gameController.character.transform.position, transform.position);
            if (distance < 0.6f) {
                if (_gameController.map.collectedEnoughKeys ) {
                    _gameController.WinGame();
                } else if (popup == null) {
                    popup = app.resourceManager.ShowPopup(PopupType.Notification, (_gameObject) => {
                        _gameObject.GetComponent<NotificationPopup>().contentNotifi.text = "!!! Not enough key !!!";
                    });
                    //popup.GetComponent<NotificationPopup>().contentNotifi.text = "!!! Not enough key !!!";
                }
            } else if (popup != null) {
                popup.TryGetComponent(out IPopup item);
                item.Close();
                popup = null;
            }
        }
    }
}
