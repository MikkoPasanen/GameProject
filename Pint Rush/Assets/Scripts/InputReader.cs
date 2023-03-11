using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PintRush
{
    public class InputReader : MonoBehaviour
    {
        
        private PlayerInput playerInput;

        // Piste pelimaailmassa, johon käyttäjä haluaa hahmon liikkuvan
        private Vector3 worldTouchPosition;
        private Vector3 startPosition;
        private Vector3 endPosition;

        private bool tapping;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void OnTouch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 touchPosition = context.ReadValue<Vector2>();
                Vector3 screenCoordinate = touchPosition;
                this.worldTouchPosition = Camera.main.ScreenToWorldPoint(screenCoordinate);
            }
        }

        public void OnPress(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                //Debug.Log("Press started!");
                tapping = true;
                startPosition = worldTouchPosition;
            }
            if (context.performed)
            {
                //Debug.Log("Pressing...");
                tapping = true;
            }
            if (context.canceled)
            {
                //Debug.Log("Press ended!");
                tapping = false;
                endPosition = worldTouchPosition;
            }
        }

        public Vector3 GetTouchWorldPosition()
        {
            return worldTouchPosition;
        }

        public Vector3 GetTouchStartPosition()
        {
            return startPosition;
        }

        public Vector3 GetTouchEndPosition()
        {
            return endPosition;
        }

        public bool GetTapState()
        {
            return tapping;
        }
        
    }
}

