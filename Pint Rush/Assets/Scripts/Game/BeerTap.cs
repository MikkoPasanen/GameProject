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
            // Disables the rb2d for the glass so that it cant be moved around by hitting it with another glass
            if(collision.gameObject.tag == "Glass")
            {
                Debug.Log($"{id}");
                glassController = collision.gameObject.GetComponent<GlassController>();
                glassController.SetIsInsideTapArea(true);
                glassController.SnapUnderTap(this.gameObject.transform.position);
                collision.gameObject.transform.position = this.gameObject.transform.position;
                glassController.SetFill(true);
                glassController.rb2d.simulated = false;
            }
        }

        // Glass is taken out of the tap
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Glass")
            {
                glassController = collision.gameObject.GetComponent<GlassController>();
                glassController.SetIsInsideTapArea(false);
                glassController.SetIsUnderTap(false);
                glassController.rb2d.simulated = true;
            }
        }
    }
}
