using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public string Name;

        void Start()
        {

        }

        void OnEnable()
        {
			
        }

		public void OnPointerUp(PointerEventData data) 
		{ 
			CrossPlatformInputManager.SetButtonUp(Name);
		}

		public void OnPointerDown(PointerEventData data) 
		{ 
			CrossPlatformInputManager.SetButtonDown(Name);
		}

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
	}
}
