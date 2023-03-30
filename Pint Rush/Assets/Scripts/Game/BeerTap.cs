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

        [SerializeField] private Sprite[] tapSprites = new Sprite[4];
        private SpriteRenderer spriteRenderer;

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

            // Set sprite to default: wooden tap
            spriteRenderer = GetComponent<SpriteRenderer>();
            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = tapSprites[0];
            }
        }

        public void SetSprite(int index)
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.sprite = tapSprites[index];
            }
        }

        public Sprite GetNextUpgradeSprite(int index)
        {
            return tapSprites[index];
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
