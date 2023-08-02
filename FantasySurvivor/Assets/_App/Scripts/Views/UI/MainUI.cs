using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using ArbanFramework;
using ArbanFramework.MVC;

using TMPro;


using UnityEngine;
using UnityEngine.UI;
namespace MR
{
    public class MainUI : View<GameApp>, IPopup
    {
        [SerializeField] private TextMeshProUGUI _txtTotalKey;
        [SerializeField] private TextMeshProUGUI _txtCollectedKey;
        [SerializeField] private CustomJoystick _joystick;

        [SerializeField] private Slider _sldOil;

        [SerializeField] private Button _btnDash;

        private GameController _gameController => Singleton<GameController>.instance;

        public void Close()
        {
            Destroy(gameObject);
        }

        public void Open()
        {
        }

        protected override void OnViewInit()
        {
            base.OnViewInit();

            Singleton<CharacterController>.instance.joystick = _joystick;
            
            _btnDash.onClick.AddListener(Singleton<CharacterController>.instance.ControlDash);

            AddDataBinding("totalKey-value", _txtTotalKey, (control, item) =>
            {
                control.text = "/ " + _gameController.map.model.totalKey.ToString();
            }, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.totalKey)));

            AddDataBinding("collectedKey-value", _txtCollectedKey, (control, item) =>
            {
                control.text = _gameController.map.model.collectedKey.ToString();
            }, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.collectedKey)));

            var character = _gameController.character.model;
            AddDataBinding("sldOil-value", _sldOil, (control, e) =>
            {
                var currentValue = control.value;
                var value = character.currentOil / character.amountOil;
                if(value > currentValue)
                {
                    control.value += Time.deltaTime / 2;
                }
                else
                {
                    control.value = value;
                }

                currentValue = value;
            }, new DataChangedValue(
                CharacterModel.dataChangedEvent,
                nameof(CharacterModel.currentOil),
                character
                )
            );
        }
    }
}
