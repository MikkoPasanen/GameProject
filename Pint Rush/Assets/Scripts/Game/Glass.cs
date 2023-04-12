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


        // Updated in the Animator
        [SerializeField] private bool filled = false;
        [SerializeField] private bool pouring = false;

        private enum Type { None = 0, Lager, Stout, Mystery };
        private Type type;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.speed = 1.0f;
            glassName = this.name;
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
            
            if(pouring)
            {
                rb2d.simulated = false;
                if(beerTap != null)
                {
                    beerTap.SetTapDown();
                }
            }
            else
            {
                rb2d.simulated = true;
                if(beerTap != null)
                {
                    beerTap.SetTapUp();
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
        private void OnMouseUp()
        {
            if(filled)
            {
                int mask = 1 << LayerMask.NameToLayer("Customer");
                RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position - new Vector2(0.5f, 0), new Vector2(0.3f, 0.3f), 0f, new Vector2(1, 0), distance: 1f, layerMask: mask);
                if (hit.collider != null)
                {
                    Customer cc = hit.collider.gameObject.GetComponent<Customer>();
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

                    csc.RemoveGlass();
                    Destroy(gameObject);
                }
                isDragging = false;
            }
            else if (snapping)
            {
                transform.position = beerTap.GetSnapPos().position;
                snapping = false;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If collision is with any type of beer tap
            if(collision.gameObject.tag == "BeerTap")
            {
                // If glass and tap types match
                if((int)this.type == (int)collision.gameObject.GetComponent<BeerTap>().type)
                {
                    beerTap = collision.gameObject.GetComponent<BeerTap>();

                    // Snapping to place in OnMouseUp
                    snapping = true;
                    SetGlassUnderTap(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "BeerTap")
            {
                SetGlassUnderTap(false);
            }
        }

        public void SetAnimatorSpeed(float speed)
        {
            Debug.Log($"Animation speed set to: {speed}");
            animator.speed = speed;
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
