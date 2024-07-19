using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPOOutline;

public class Grab3_HCH : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float lookSpeed = 2f;
    public float jumpForce = 5f;
    public float fixedGrabDistance = 1f;
    public Image crosshairImage;
    public float dropMoveSpeedMultiplier = 10f;
    public float collisionThreshold = 0.01f;
    public float minLookAngle = -45f;
    public float maxLookAngle = 45f;
    public float minScale = 0.1f;
    public float groundCheckDistance = 0.1f;
    public Camera playerCamera;
    public GameObject grabbedObject;

    private GameObject clonedObject;
    private bool isGrabbing = false;
    private bool isDropping = false;
    private Vector3 initialScale;
    private float initialDistance;
    private Vector3 dropDirection;
    private Collider grabbedCollider;
    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded = true;
    private Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider>();

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        if (crosshairImage != null)
        {
            crosshairImage.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGrabbing)
            {
                PrepareToDropObject();
            }
            else
            {
                TryGrabObject();
            }
        }

        if (isGrabbing && grabbedObject != null)
        {
            MoveObjectToCrosshair();
            VisualizeRayThroughObject();
        }

        if (isDropping)
        {
            ContinueDropObject();
        }
    }

    void TryGrabObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if (hit.collider != null && hit.collider.CompareTag("Grab"))
            if (hit.collider != null && hit.transform.gameObject.layer == 9)
            {
                // 잡는 사운드
                SoundManager.instance.PlaySound("GrabStart", this.transform);
                grabbedObject = hit.collider.gameObject;
                grabbedCollider = grabbedObject.GetComponent<Collider>();
                print(grabbedObject.gameObject.name);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                // 외곽선 표시
                Outlinable outline = grabbedObject.GetComponent<Outlinable>();
                outline.enabled = true;

                isGrabbing = true;
                grabbedObject.transform.SetParent(GameManager.instance.player.transform);

                Physics.IgnoreCollision(grabbedCollider, playerCollider, true);

                initialDistance = Vector3.Distance(playerCamera.transform.position, grabbedObject.GetComponent<Renderer>().bounds.center);
                initialScale = grabbedObject.transform.localScale;
            }
        }
    }

    void PrepareToDropObject()
    {
        if (grabbedObject != null)
        {
            isGrabbing = false;
            isDropping = true;

            Vector3 objectCenter = grabbedObject.GetComponent<Renderer>().bounds.center;
            dropDirection = (objectCenter - playerCamera.transform.position).normalized;

            Physics.IgnoreCollision(grabbedCollider, playerCollider, false);

            // Check camera rotation before cloning the object
            //float cameraRotationX = playerCamera.transform.localRotation.eulerAngles.x;
            float cameraRotationX = GameManager.instance.player.transform.localRotation.eulerAngles.x;
            // Clone the object only if camera is looking downwards (rotationX >= 90)
            if (cameraRotationX >= 0f)
            {
                // Clone the object and keep it in front of the camera
                clonedObject = Instantiate(grabbedObject, grabbedObject.transform.position, grabbedObject.transform.rotation);
                clonedObject.transform.localScale = grabbedObject.transform.localScale;
                clonedObject.GetComponent<Rigidbody>().isKinematic = true;

                // Disable the collider of the cloned object to prevent collisions
                Collider clonedCollider = clonedObject.GetComponent<Collider>();
                if (clonedCollider != null)
                {
                    clonedCollider.enabled = false;
                }
                //StartCoroutine(DestroyCloneAfterDelay(0.25f)); // Set delay to 0.1 seconds
            }
        }
    }

    void ContinueDropObject()
    {
        if (grabbedObject != null)
        {
            // 외곽선 해제
            Outlinable outline = grabbedObject.GetComponent<Outlinable>();
            outline.enabled = false;

            grabbedObject.transform.SetParent(null);
            float step = moveSpeed * dropMoveSpeedMultiplier * Time.deltaTime;
            Vector3 currentPos = grabbedObject.GetComponent<Renderer>().bounds.center;
            float currentDistance = Vector3.Distance(playerCamera.transform.position, currentPos);

            float scaleMultiplier = currentDistance / initialDistance;
            Vector3 newScale = new Vector3(
                Mathf.Max(initialScale.x * scaleMultiplier, minScale),
                Mathf.Max(initialScale.y * scaleMultiplier, minScale),
                Mathf.Max(initialScale.z * scaleMultiplier, minScale)
            );
            grabbedObject.transform.localScale = newScale;

            Vector3 nextPosition = grabbedObject.transform.position + dropDirection * step;

            RaycastHit hit;
            if (Physics.Raycast(nextPosition, Vector3.down, out hit, groundCheckDistance))
            {
                if (Vector3.Dot(hit.normal, Vector3.up) > 0.7f)
                {
                    nextPosition.y = hit.point.y + (grabbedObject.GetComponent<Collider>().bounds.extents.y);
                }
            }

            if (IsPositionValid(nextPosition))
            {
                grabbedObject.transform.position = nextPosition;
            }
            else
            {
                isDropping = false;
                // 놓는 사운드
                SoundManager.instance.PlaySound("GrabEnd", this.transform);

                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                // 나중에 리팩토링
                if (grabbedObject.CompareTag("DivideCube"))
                {
                    grabbedObject.GetComponent<DividedCube_HCH>().DivideCube();
                }
                grabbedObject = null;
                Destroy(clonedObject);
            }
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, grabbedCollider.bounds.extents, grabbedObject.transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != grabbedObject)
            {
                return false;
            }
        }
        return true;
    }

    //bool IsGrounded()
    //{
    //    return Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f);
    //}

    void DropObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    void MoveObjectToCrosshair()
    {
        Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * fixedGrabDistance;
        grabbedObject.transform.position = targetPosition;

        float scaleMultiplier = fixedGrabDistance / initialDistance;
        grabbedObject.transform.localScale = initialScale * scaleMultiplier;
    }

    void VisualizeRayThroughObject()
    {
        if (grabbedObject != null)
        {
            Vector3 objectCenter = grabbedObject.GetComponent<Renderer>().bounds.center;
            Vector3 direction = objectCenter - playerCamera.transform.position;
            float maxDistance = 300f;

            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + direction * maxDistance, Color.red);
        }
    }
}
