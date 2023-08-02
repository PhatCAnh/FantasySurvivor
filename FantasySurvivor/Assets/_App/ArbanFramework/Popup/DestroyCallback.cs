using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ArbanFramework
{
    public class DestroyCallback: MonoBehaviour
    {
        public Action onClose;

        private void OnDestroy()
        {
            onClose?.Invoke();
        }
    }
}
