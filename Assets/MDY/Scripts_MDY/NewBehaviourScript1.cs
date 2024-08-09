using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EPOOutline;

public class Grab3_HCH2 : MonoBehaviour
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
    [SerializeField]
    private bool isDropping = false;
    private Vector3 initialScale;
    private float initialDistance;
    private Vector3 dropDirection;
    private Collider grabbedCollider;
    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded = true;
    private Collider playerCollider;

    Ray ray;
    RaycastHit hit;
    public Image aimImage;
    public Image canGrabImage;
    public Image grabbedImage;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider>();
        aimImage.enabled = true;

        if (crosshairImage != null)
        {
            crosshairImage.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAimImage();

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

    void ChangeAimImage()
    {
        ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit))
        {
            if (grabbedObject == null)
            {
                if (hit.transform.gameObject.layer == 9 || hit.transform.gameObject.CompareTag("Clone"))
                {
                    aimImage.enabled = false;
                    grabbedImage.enabled = false;
                    canGrabImage.enabled = true;
                }
                else
                {
                    grabbedImage.enabled = false;
                    canGrabImage.enabled = false;
                    aimImage.enabled = true;
                }
            }
            else
            {
                aimImage.enabled = false;
                canGrabImage.enabled = false;
                grabbedImage.enabled = true;
            }
        }
    }

    void TryGrabObject()
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.transform.gameObject.layer == 9)
            {
                SoundManager.instance.PlaySound("GrabStart", this.transform);
                grabbedObject = hit.collider.gameObject;
                grabbedCollider = grabbedObject.GetComponent<Collider>();
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                Outlinable outline = grabbedObject.GetComponent<Outlinable>();
                outline.enabled = true;

                isGrabbing = true;
                grabbedObject.transform.SetParent(playerCamera.transform);

                IgnoreCollisionsWithPlayer(grabbedObject, true); // 콜라이더 충돌 무시

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

            IgnoreCollisionsWithPlayer(grabbedObject, false); // 콜라이더 충돌 다시 활성화

            float cameraRotationX = GameManager.instance.player.transform.localRotation.eulerAngles.x;

            if (cameraRotationX >= 0f)
            {
                clonedObject = Instantiate(grabbedObject, grabbedObject.transform.position, grabbedObject.transform.rotation);
                clonedObject.transform.localScale = grabbedObject.transform.localScale;
                clonedObject.GetComponent<Rigidbody>().isKinematic = true;

                Collider clonedCollider = clonedObject.GetComponent<Collider>();
                if (clonedCollider != null)
                {
                    clonedCollider.enabled = false;
                }
            }
        }
    }

    void ContinueDropObject()
    {
        if (grabbedObject != null)
        {
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
                SoundManager.instance.PlaySound("GrabEnd", this.transform);

                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
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
        RaycastHit hit;

        // 레이캐스트를 사용하여 물체가 이동할 수 있는 최대 거리를 계산
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, fixedGrabDistance))
        {
            // 레이캐스트가 다른 오브젝트와 충돌한 경우, 충돌 지점을 목표 위치로 설정
            targetPosition = hit.point;

            // 레이 시각화
            Debug.DrawLine(playerCamera.transform.position, hit.point, Color.green);
        }
        else
        {
            // 충돌이 없을 경우 전체 레이 시각화
            Debug.DrawLine(playerCamera.transform.position, targetPosition, Color.green);
        }

        grabbedObject.transform.position = targetPosition;

        // 물체의 스케일을 조정하여 거리에 맞게 조정
        float scaleMultiplier = (targetPosition - playerCamera.transform.position).magnitude / initialDistance;
        grabbedObject.transform.localScale = initialScale * scaleMultiplier;
    }

    void VisualizeRayThroughObject()
    {
        if (grabbedObject != null)
        {
            Vector3 objectCenter = grabbedObject.GetComponent<Renderer>().bounds.center;
            Vector3 direction = objectCenter - playerCamera.transform.position;
            float maxDistance = 300f;

            // 레이 시각화
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + direction * maxDistance, Color.red);
        }
    }

    void IgnoreCollisionsWithPlayer(GameObject objectToIgnore, bool ignore)
    {
        Collider[] playerColliders = playerCollider.GetComponentsInChildren<Collider>();
        Collider[] objectColliders = objectToIgnore.GetComponentsInChildren<Collider>();

        foreach (Collider playerCol in playerColliders)
        {
            foreach (Collider objCol in objectColliders)
            {
                Physics.IgnoreCollision(playerCol, objCol, ignore);
            }
        }
    }
}