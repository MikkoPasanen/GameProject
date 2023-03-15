using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PintRush
{
    public class GlassTwoAnimation : MonoBehaviour
    {
        public Sprite[] sprites;
        public int spritePerFrame = 12;
        public bool loop = false;
        public GlassController gm;
        private int index = 0;
        private Image image;
        private int frame = 0;

        void Awake()
        {
            image = GetComponent<Image>();
        }

        //If the glass is under the tap, it will start the filling animation
        void FixedUpdate()
        {
            if(gm.GetFill())
            {
                if (index == sprites.Length) return;
                frame++;
                if (frame < spritePerFrame) return;
                image.sprite = sprites[index];
                frame = 0;
                index++;

                if (index >= sprites.Length)
                {
                    if (loop) index = 0;
                }
            }
        }
    }
}

