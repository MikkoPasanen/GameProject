using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class GlassController : MonoBehaviour
    {
        private bool isDragging;
        private Vector3 offset;
        [SerializeField] private BoxCollider2D boxCollider;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnMouseDown()
        {
            //Disable the glass piles collider so you cant spawn new glasses when dragging the newly spawned glass
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            boxCollider.enabled = false;
        }

        private void OnMouseDrag()
        {

            //Move the glass
            if (isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0);
                boxCollider.enabled = true;
            }
        }

        private void OnMouseUp()
        {
            isDragging = false;
        }
    }
}
