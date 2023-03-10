using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class GlassPileController : MonoBehaviour
    {
        [SerializeField] private GameObject glassPrefab;
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private int maxGlasses = 3;
        private int currentGlasses = 0;

        private void OnMouseDown()
        {
            if (canvasRect!= null && currentGlasses < maxGlasses)
            {
                Debug.Log("pile pressed!");

                // Get the position of the mouse click in screen space
                Vector2 screenPos = Input.mousePosition;

                // Convert the screen position to a position within the canvas
                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out canvasPos);

                // Instantiate the glass prefab at the converted position and add +1 to the current glasses
                GameObject glass = Instantiate(glassPrefab, canvasPos, Quaternion.identity);
                currentGlasses++;

                // Set the parent of the glass to the canvas
                glass.transform.SetParent(canvasRect.transform, false);
            }
            
        }
    }
}
