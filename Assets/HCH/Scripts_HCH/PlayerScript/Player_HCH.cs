using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HCH : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 40;
    public float jumpForce = 10;
    public bool isJump = false;
    float jumpMoveSpeedClamp = 10;

    public Transform rotateTarget;
    BoxCollider feetCol;
    ConstantForce cf;

    // �ӵ� ����
    [Range(0.0f, 1.0f)]
    public float drag = 0.0f;

    Rigidbody rb;
    Vector3 currentRotation;

    // ��Ŭ�� �� ī�޶� �̵� ���� ���� ����
    public bool isCamMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>(); // �����ÿ� �Ѽ� �߷� ���ϰ� ���̱�
        feetCol = GetComponentInChildren<BoxCollider>(); // ���߿� �������� ���� ��������

        currentRotation = rotateTarget.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        // ĳ���� ������
        CharacterMove();

        // ĳ���� ����
        CharacterRoatation();
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� ȸ��
        CameraRotaion();

        // ĳ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterJump();
        }

        if (IsGrounded())
        {
            print("���� ����");
        }
        else
        {
            print("������");
            cf.enabled = true;
        }
    }

    void CharacterMove()
    {
        Vector3 newVelocity = rb.velocity;
        // ���� ȿ��
        if (!isJump) newVelocity *= (1.0f - drag);

        Vector3 f = rotateTarget.forward; f.y = 0.0f; f.Normalize();
        Vector3 r = rotateTarget.right; r.y = 0.0f; r.Normalize();

        newVelocity += f * Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;
        newVelocity += r * Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;

        // ���� ���� �� �̵� �ӵ��� ����
        if (isJump)
        {
            newVelocity.x = Mathf.Clamp(newVelocity.x, -moveSpeed / jumpMoveSpeedClamp, moveSpeed / jumpMoveSpeedClamp);
            newVelocity.z = Mathf.Clamp(newVelocity.z, -moveSpeed / jumpMoveSpeedClamp, moveSpeed / jumpMoveSpeedClamp);
        }

        rb.velocity = newVelocity;
    }

    void CharacterJump()
    {
        // ���� ������ �� ���� �Ұ���
        if (isJump) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJump = true;
        //cf.enabled = true;
    }

    void CameraRotaion()
    {
        if (!isCamMove) return;
        currentRotation.y += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x - Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime, -70.0f, 70.0f);

        rotateTarget.rotation = Quaternion.Euler(currentRotation);
    }

    void CharacterRoatation()
    {
        this.transform.rotation = rotateTarget.rotation;
        // �¿� ȸ���� �����ϵ���
        //Vector3 targetRot = rotateTarget.rotation.eulerAngles;
        //Vector3 currentRot = transform.rotation.eulerAngles;
        //currentRot.y = targetRot.y;
        //transform.rotation = Quaternion.Euler(currentRot);
    }

    public float groundCheckRadius = 0.1f; // ���� ������
    public float groundCheckDistance = 0.2f;
    bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.1f);
        Vector3 rayOrigin = transform.position;
        float sphereCastDistance = GetComponent<Collider>().bounds.extents.y + groundCheckDistance;

        // SphereCast�� �ð�ȭ�ϴ� �κ�
        Debug.DrawRay(rayOrigin, Vector3.down * sphereCastDistance, Color.red);

        return Physics.SphereCast(rayOrigin, groundCheckRadius, Vector3.down, out RaycastHit hit, sphereCastDistance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ������ ���� ����
        //if(collision.gameObject.layer == 7)
        //{
        //    isJump = false;
        //}
        //print("��������");
        //print(collision.GetContact(0).point);
        isJump = false;
        cf.enabled = false;
    }

    public void FeetCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        isJump = false;
        cf.enabled = false;
    }
}
