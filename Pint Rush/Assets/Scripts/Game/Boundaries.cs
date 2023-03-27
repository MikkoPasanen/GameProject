using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class Boundaries : MonoBehaviour
    {
        [SerializeField] private GlassSpawner glassSpawner;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Glass")
            {
                Destroy(collision.gameObject);
                Debug.Log($"Destroyed: {collision.gameObject.name}");
                glassSpawner.RemoveGlass();
            }

            if(collision.gameObject.tag == "LifeBottle")
            {
                collision.GetComponent<Life>().SetDestroy();
            }
        }
    }
}
