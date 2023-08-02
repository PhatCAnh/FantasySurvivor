using System.Collections;
using System.Collections.Generic;

using ArbanFramework.MVC;

using MR.CharacterState;

using UnityEngine;

namespace MR
{
    public class MapView : View<GameApp>
    {
        public MapModel model {get; private set;}

        public bool collectedEnoughKeys => model.collectedKey == model.totalKey;

        protected override void OnViewInit() {
            base.OnViewInit();
        }

        public void Init(MapModel model) {
            this.model = model;
        }
    }
}
