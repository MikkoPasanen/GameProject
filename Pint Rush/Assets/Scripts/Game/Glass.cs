using UnityEngine;
using TMPro;
using System.Collections;
using JetBrains.Annotations;

namespace PintRush
{
    public class Glass : MonoBehaviour
    {
        private bool isDragging;
        private bool isInsideTapArea = false;
        private bool isUnderTap = false;
        private bool onCustomer = false;
        private Vector3 snapToTap; 
        private Vector3 offset;
        private Animator animator;
        //[SerializeField] private CustomerController cc;
        private string glassName;
        [SerializeField] BoxCollider2D bc2d;
        [SerializeField] Rigidbody2D rb2d;
        private int pourTime;
        private bool filling = false;
        BeerTap beerTap;
        private bool snapping = false;
        [SerializeField] private bool filled = false;
        [SerializeField] private bool pouring = false;

        private enum Type { None = 0, Lager, Stout, Mystery };
        private Type type;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            glassName = this.name;
            Debug.Log($"This glass name:" + glassName);
        }

        private void Update()
        {
            if(beerTap != null)
            {
                if(beerTap.GetPouring() && !filling)
                {
                    filling = true;
                    animator.SetBool("TapTrigger", true);
                    beerTap.SetPouring(false);
                }
            }
        }

        public void SetType(string typeName)
        {
            switch (typeName)
            {
                case "Lager":
                    type = Type.Lager;
                    break;
                case "Stout":
                    type = Type.Stout;
                    break;
                case "Mystery":
                    type = Type.Mystery;
                    break;
                default:
                    type = Type.None;
                    Debug.Log($"Glass type is {type}");
                    break;
            }
            Debug.Log(type.ToString());
        }

        //When you hold your finger on the glass
        private void OnMouseDown()
        {
            if (!pouring)
            {
                isDragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        //When you drag the glass around
        private void OnMouseDrag()
        {
            //Move the glass
            if (!pouring && isDragging)
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
            if(snapping)
            {
                Debug.Log("Snapping");
                transform.position = beerTap.GetSnapPos().position;
                snapping = false;
            }

            if(filled)
            {
                int mask = 1 << LayerMask.NameToLayer("Customer");
                RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position - new Vector2(0.5f, 0), new Vector2(1, 1), 0f, new Vector2(1, 0), distance: Mathf.Infinity, layerMask: mask);
                if (hit.collider != null)
                {
                    CustomerController cc = hit.collider.gameObject.GetComponent<CustomerController>();
                    CustomerSpawnController csc = hit.collider.gameObject.GetComponentInParent<CustomerSpawnController>();

                    string chosenBeerName = cc.GetBeerName();
                    string glassName = gameObject.name;

                    if (glassName.Contains(chosenBeerName)) // CORRECT!
                    {
                        cc.SetExiting(true, true);

                        if (chosenBeerName == "GlassOne")
                        {
                            csc.AddBeerOne();
                        }
                        if (chosenBeerName == "GlassTwo")
                        {
                            csc.AddBeerTwo();
                        }
                        if (chosenBeerName == "GlassThree")
                        {
                            csc.AddBeerThree();
                        }
                    }
                    else // FALSE!
                    {
                        cc.SetExiting(true, false);
                    }
                    Destroy(gameObject);
                    csc.DespawnGlass();
                }
                isDragging = false;
            }
           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If collision is with any type of beer tap
            if(collision.gameObject.tag == "BeerTap")
            {
                beerTap = collision.gameObject.GetComponent<BeerTap>();

                // If glass and tap types match
                if((int)this.type == (int)beerTap.type)
                {
                    // Snapping to place in OnMouseUp
                    snapping = true;
                    SetGlassUnderTap(true);
                }
            }
        }

        public void SetGlassUnderTap(bool isUnderTap)
        {
            this.isUnderTap = isUnderTap;
            beerTap.SetGlassUnderTap(isUnderTap);
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
    }
}
