using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class BeerTap : MonoBehaviour
    {
        GlassController glassController;
        [SerializeField] private int id;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Glass is inside the beer tap area
            if(collision.gameObject.tag == "Glass")
            {
                Debug.Log($"{id}");
                glassController = collision.gameObject.GetComponent<GlassController>();
                glassController.SetIsInsideTapArea(true);
                glassController.SnapUnderTap(this.gameObject.transform.position);
                collision.gameObject.transform.position = this.gameObject.transform.position;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Glass")
            {
                glassController = collision.gameObject.GetComponent<GlassController>();
                glassController.SetIsInsideTapArea(false);
                glassController.SetIsUnderTap(false);
            }
        }
    }
}
