using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasySurvivor
{
    public class TutorialNode : MonoBehaviour
    {
        public TutorialNodeType nodeType;
        [SerializeField] GameObject goNode;

        private void Start()
        {
            TutorialController.instance.AddNode(this);
        }

        public void Show()
        {

        }

        public void Click()
        {
            TutorialController.instance.TutorialNodeClick(this);
        }
    }
}
