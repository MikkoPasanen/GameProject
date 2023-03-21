using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PintRush
{
    public class BeerTap : MonoBehaviour
    {
        GlassController glassController;
        [SerializeField] private int id;
        [SerializeField] private GameObject snapPos;

        [SerializeField] private bool beerOne;
        [SerializeField] private bool beerTwo;
        [SerializeField] private bool beerThree;

        [SerializeField] private enum BeerType {
            None = 0,
            Stout,
            Wheat,
            Lager
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Glass is inside the beer tap area
            // Disables the rb2d for the glass so that it cant be moved around by hitting it with another glass
            if(collision.gameObject.tag == "Glass")
            {
                if (beerOne && collision.gameObject.name.Contains("GlassOne") || beerTwo && collision.gameObject.name.Contains("GlassTwo") || beerThree && collision.gameObject.name.Contains("GlassThree"))
                {
                    glassController = collision.gameObject.GetComponent<GlassController>();
                    glassController.SetIsInsideTapArea(true);
                    glassController.SnapUnderTap(snapPos.transform.position);
                }
                else
                {
                    Debug.Log("Wrong glass!");
                }
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
            }
        }
    }
}
