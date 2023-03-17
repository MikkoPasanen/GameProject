using UnityEngine;
using TMPro;

namespace PintRush
{
    public class GlassController : MonoBehaviour
    {
        private bool isDragging;
        private bool isInsideTapArea = false;
        private bool isUnderTap = false;
        private bool filled = false;
        private bool onCustomer = false;
        private int pourTimer = 0;
        private Vector3 snapToTap; 
        private Vector3 offset;
        private Animator animator;
        [SerializeField] GameManagement gm;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
                GiveGlass();
            }
            isDragging = false;
            if(isInsideTapArea)
            {
                this.gameObject.transform.position = snapToTap;
                SetFill(true);
                animator.SetTrigger("TapTrigger");            
            }
        }

        public void GiveGlass()
        {
            gm.RemoveGlass();
            //transform.parent.GetComponent<CustomerSpawnController>().SetCustomerSpawned(false);
            SetFill(false);
            SetOnCustomer(false);
            Destroy(gameObject);
        }


        public bool GetDragState()
        {
            return isDragging;
        }
        public int GetPourTimer()
        {
            return pourTimer;
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

        public void SetFill(bool filled)
        {
            this.filled = filled;
        }

        //Timer for the beer pouring
        private void FixedUpdate()
        {
            if(isUnderTap)
            {
                pourTimer++;
                //Debug.Log($"{pourTimer}");
            }
            else
            {
                pourTimer = 0;
            }
        }
    }
}
