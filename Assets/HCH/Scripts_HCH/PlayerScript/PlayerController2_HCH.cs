using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2_HCH : MonoBehaviour
{
    public float speed = 5;

    Rigidbody rigidbody;

    // °È´Â ¼Ò¸® µô·¹ÀÌ
    WaitForSeconds footstepDelay = new(0.5f);
    [SerializeField]
    bool isFootstepPlay = false;
    public GameObject groundCheckObject;
    GroundCheck groundCheck;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        groundCheck = groundCheckObject.GetComponent<GroundCheck>();

        isFootstepPlay = false;
        StartCoroutine(FootstepSound());
    }

    void FixedUpdate()
    {
        // Get targetMovingSpeed.
        float targetMovingSpeed = speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        isFootstepPlay = rigidbody.velocity.magnitude > 3f && groundCheck.isGrounded;
    }

    IEnumerator FootstepSound()
    {
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
