using UnityEngine;
using TMPro;
using System.Collections;
using JetBrains.Annotations;

namespace PintRush
{
    public class GlassController : MonoBehaviour
    {
        private bool isDragging;
        private bool isInsideTapArea = false;
        private bool isUnderTap = false;
        private bool filled = false;
        private bool onCustomer = false;
        private Vector3 snapToTap; 
        private Vector3 offset;
        private Animator animator;
        [SerializeField] GameManagement gm;
        [SerializeField] CustomerController cc;
        private string glassName;
        [SerializeField] BoxCollider2D bc2d;
        [SerializeField] Rigidbody2D rb2d;
        [SerializeField] int beerPourTime;
        private int pourTime;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            glassName = this.name;
            Debug.Log($"This glass name:" + glassName);
        }

        //When you hold your finger on the glass
        private void OnMouseDown()
        {
            isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //When you drag the glass around
        private void OnMouseDrag()
        {
            //Move the glass
            if (isDragging)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                transform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
            Gizmos.DrawWireCube(transform.position, new Vector3(1.0f, 1.0f, 0.0f));
        }

        //When you let go of the glass
        //If the glass is under the tap, it snaps into place
        //It starts the filling of the glass and  triggers the animation
        private void OnMouseUp()
        {
            int mask = 1 << LayerMask.NameToLayer("Customer");
            RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position - new Vector2(0.5f, 0), new Vector2(1, 1), 0f, new Vector2(1, 0), distance: Mathf.Infinity, layerMask: mask);
            if(hit.collider != null)
            {
                if(filled)
                {
                    CustomerController cc = hit.collider.gameObject.GetComponent<CustomerController>();
                    CustomerSpawnController csc = hit.collider.gameObject.GetComponentInParent<CustomerSpawnController>();

                    string chosenBeerName = cc.GetBeerName();
                    string glassName = gameObject.name;

                    if(glassName.Contains(chosenBeerName))
                    {
                        Destroy(gameObject);
                        Destroy(hit.collider.gameObject);
                        gm.RemoveGlass();
                        filled = false;
                        csc.SetCustomerSpawned(false);
                    }
                }
            }

            isDragging = false;

            if(isInsideTapArea)
            {
                this.gameObject.transform.position = snapToTap;
                animator.SetTrigger("TapTrigger");
                filled = true;
            }
        }


        public bool GetDragState()
        {
            return isDragging;
        }

        public bool GetOnCustomer()
        {
            return onCustomer;
        }

        public void SetOnCustomer(bool onCustomer)
        {
            this.onCustomer = onCustomer;
        }

        public void SetIsInsideTapArea(bool isInsideTapArea)
        {
            this.isInsideTapArea = isInsideTapArea;
        }
        public void SetIsUnderTap(bool isUnderTap)
        {
            this.isUnderTap = isUnderTap;
        }

        public bool GetIsUnderTap()
        {
            return isUnderTap;
        }

        public void SnapUnderTap(Vector3 tapPosition)
        {
            snapToTap = tapPosition;
        }

        public bool GetFill()
        {
            return filled;
        }

        //Timer for the beer pouring
        private void FixedUpdate()
        {
            if(isUnderTap)
            {
                pourTime++;
                Debug.Log($"{pourTime}");
                if (pourTime >= beerPourTime)
                {
                }
            }
            else
            {
                pourTime = 0;
            }
        }
    }
}
