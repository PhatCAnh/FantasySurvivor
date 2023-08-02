using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

namespace MR
{
    public class MapModel : Model<GameApp>
    {
        public static EventTypeBase dataChangedEvent = new EventTypeBase (nameof (MapModel) + ".dataChanged");
        public MapModel(int totalKey) : base (dataChangedEvent)
        {
            this.totalKey = totalKey;
            this.collectedKey = 0;
        }

        private int _totalKey;
        private int _collectedKey;

        public MapModel() : base (dataChangedEvent)
        {
        }

        public int totalKey
        {
            get => _totalKey;
            set
            {
                //if( totalKey != value )
                //{
                _totalKey = value;
                RaiseDataChanged (nameof (totalKey));
                //}
            }
        }

        public int collectedKey
        {
            get => _collectedKey;
            set
            {
                //if( collectedKey != value )
                //{
                _collectedKey = value;
                RaiseDataChanged (nameof (collectedKey));
                //}
            }
        }
    }
}
