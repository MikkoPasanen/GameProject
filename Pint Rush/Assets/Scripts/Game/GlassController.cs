using UnityEngine;
using TMPro;

namespace PintRush
{
    public class GlassController : MonoBehaviour
    {
        private bool isDragging;
        private bool isInsideTapArea;
        private bool isUnderTap;
        private bool filled = false;
        private int pourTimer = 0;
        private Vector3 snapToTap; 
        private Vector3 offset;
        public Rigidbody2D rb2d;
        private void Awake()
        {
            isInsideTapArea = false;
            isUnderTap = false;
        }

        //When you hold your finger on the glass
        private void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //When you drag the glass around
        private void OnMouseDrag()
        {
            //Move the glass
            if (isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
        }

        //When you let go of the glass
        //If the glass is under the tap, it snaps into place
        private void OnMouseUp()
        {
            isDragging = false;
            if(isInsideTapArea)
            {
                isUnderTap = true;
                this.gameObject.transform.position = snapToTap;

            }
        }

        public bool GetDragState()
        {
            return isDragging;
        }
        public int GetPourTimer()
        {
            return pourTimer;
        }

        public void SetIsInsideTapArea(bool isInsideTapArea)
        {
            this.isInsideTapArea = isInsideTapArea;
        }
        public void SetIsUnderTap(bool isUnderTap)
        {
            this.isUnderTap = isUnderTap;
        }

        public void SnapUnderTap(Vector3 tapPosition)
        {
            snapToTap = tapPosition;
        }

        public bool GetFill()
        {
            return filled;
        }

        public void SetFill(bool filled)
        {
            this.filled = filled;
        }

        //Timer for the beer pouring
        private void FixedUpdate()
        {
            if(isUnderTap)
            {
                pourTimer++;
                Debug.Log($"{pourTimer}");
            }
            else
            {
                pourTimer = 0;
            }
        }
    }
}
