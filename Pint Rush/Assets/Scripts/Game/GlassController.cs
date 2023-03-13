using UnityEngine;
using TMPro;

namespace PintRush
{
    public class GlassController : MonoBehaviour
    {
        private bool isDragging;
        private bool isInsideTapArea;
        private bool isUnderTap;

        private int pourTimer = 0;
        [SerializeField] private TextMeshProUGUI timerText;

        private Vector3 offset;
        [SerializeField] private Vector3 snapToCoordinates;

        private void Awake()
        {
            isInsideTapArea = false;
            isUnderTap = false;
        }

        private void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseDrag()
        {
            //Move the glass
            if (isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
        }

        private void OnMouseUp()
        {
            isDragging = false;
            if(isInsideTapArea)
            {
                transform.position = snapToCoordinates;
                isUnderTap = true;
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
