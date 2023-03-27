using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PintRush
{
    public class BeerTap : MonoBehaviour
    {
        [SerializeField] private float snapOffset;

        [SerializeField] private bool beerOne;
        [SerializeField] private bool beerTwo;
        [SerializeField] private bool beerThree;

        [SerializeField] private Transform snapPos;

        [SerializeField] private AudioManager audioManager;

        private bool occupied = false;
        private bool pouring = false;
        private bool glassUnderTap = false;

        public enum Type { None = 0, Lager, Stout, Mystery };
        public Type type;

        private void Awake()
        {
            if (beerOne)
            {
                type = Type.Lager;
            }
            else if (beerTwo)
            {
                type = Type.Stout;
            }
            else if (beerThree) 
            { 
                type = Type.Mystery; 
            }
            else
            {
                type = Type.None;
                Debug.Log($"Beer tap type is {type}");
            }
        }

        private void OnMouseDown()
        {
            if(glassUnderTap)
            {
                pouring = true;
                audioManager.PlayBeerPouring();
            }
        }

        /*

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Glass is inside the beer tap area
            // Disables the rb2d for the glass so that it cant be moved around by hitting it with another glass
            if(collision.gameObject.tag == "Glass")
            {
                if (beerOne && collision.gameObject.name.Contains("GlassOne") || beerTwo && collision.gameObject.name.Contains("GlassTwo") || beerThree && collision.gameObject.name.Contains("GlassThree"))
                {
                    if (!occupied)
                    {
                        Debug.Log("Trigger yeet");
                        occupied = true;
                        glass = collision.gameObject.GetComponent<Glass>();
                        glass.SetIsInsideTapArea(true);
                    }
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
                Debug.Log("Trigger exited");
                occupied = false;
                glass = collision.gameObject.GetComponent<Glass>();
                glass.SetIsInsideTapArea(false);
                glass.SetIsUnderTap(false);
            }
        }
        */
        public bool GetPouring()
        {
            return this.pouring;
        }

        public void SetPouring(bool pouring)
        {
            this.pouring = pouring;
        }
        public void SetGlassUnderTap(bool glassUnderTap)
        {
            this.glassUnderTap = glassUnderTap;
        }
        public Transform GetSnapPos()
        {
            return snapPos;
        }
        /*
        public Type GetType()
        {
            return this.type;
        }
        */

    }
}
