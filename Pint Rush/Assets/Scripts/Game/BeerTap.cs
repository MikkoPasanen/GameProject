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
        [SerializeField] private Glass glass;

        [SerializeField] private AudioManager audioManager;

        [SerializeField] private Sprite[] tapSpritesUp = new Sprite[4];
        [SerializeField] private Sprite[] tapSpritesDown = new Sprite[4];
        [SerializeField] private GameObject beerImage;
        private SpriteRenderer spriteRenderer;

        private bool pouring = false;
        private bool glassUnderTap = false;

        [SerializeField] private Upgrades upgrades;

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
            }

            // Set sprite to default: wooden tap
            spriteRenderer = GetComponent<SpriteRenderer>();

            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = tapSpritesUp[0];
            }
        }

        public void SetTapUp()
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = tapSpritesUp[upgrades.GetTapUpgraded()];
                beerImage.SetActive(true);
            }
        }

        public void SetTapDown()
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = tapSpritesDown[upgrades.GetTapUpgraded()];
                beerImage.SetActive(false);
            }
        }

        public Sprite GetNextUpgradeSprite(int index)
        {
            return tapSpritesUp[index];
        }

        public void TryPouring()
        {
            if(glassUnderTap && !pouring)
            {
                pouring = true;
                audioManager.PlayBeerPouring();
            }
        }

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
    }
}
