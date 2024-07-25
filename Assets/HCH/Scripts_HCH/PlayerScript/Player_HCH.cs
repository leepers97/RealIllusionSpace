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
    ConstantForce cf;

    // �ӵ� ����
    [Range(0.0f, 1.0f)]
    public float drag = 0.0f;

    Rigidbody rb;
    Vector3 currentRotation;

    // ��Ŭ�� �� ī�޶� �̵� ���� ���� ����
    public bool isCamMove = true;

    // �ȴ� �Ҹ� ������
    WaitForSeconds footstepDelay = new(0.5f);
    bool isFootstepPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>(); // �����ÿ� �Ѽ� �߷� ���ϰ� ���̱�

        currentRotation = rotateTarget.rotation.eulerAngles;

        isFootstepPlay = rb.velocity.magnitude > 3f && IsGrounded();
        StartCoroutine(FootstepSound());
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
            //print("���� ����");
        }
        else
        {
            //print("������");
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

        // �ȴ� ����
        //if(rb.velocity.magnitude < 3f)
        //{
        //    StopCoroutine(FootstepSound());
        //}
        //if(rb.velocity.magnitude > 3f && IsGrounded() && !isPlayingFootstep)
        //{
        //    print(rb.velocity.magnitude);
        //    StartCoroutine(FootstepSound());
        //}

        isFootstepPlay = rb.velocity.magnitude > 3f && IsGrounded();
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
        currentRotation.x = Mathf.Clamp(currentRotation.x - Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime, -80.0f, 80.0f);

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

    IEnumerator FootstepSound()
    {

        //isPlayingFootstep = true;
        //SoundManager.instance.PlaySound("Footstep_1", this.transform);
        //yield return footstepDelay;
        //SoundManager.instance.PlaySound("Footstep_2", this.transform);
        //yield return footstepDelay;
        //isPlayingFootstep = false;

        while (gameObject)
        {
            yield return new WaitUntil(() => isFootstepPlay);
            SoundManager.instance.PlaySound("Footstep_1", this.transform);
            yield return footstepDelay;
            yield return new WaitUntil(() => isFootstepPlay);
            SoundManager.instance.PlaySound("Footstep_2", this.transform);
            yield return footstepDelay;
        }
    }
}
