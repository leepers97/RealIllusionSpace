using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalCamOnlyRot_HCH : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        // �÷��̾�� �ٸ� ��Ż���� �Ÿ��� ���ϰ�
        Vector3 playerOffsetFromPortal = playerCam.position - otherPortal.position;
        // ��ũ��Ʈ�� ���� ��Ż�� �Ÿ��� �÷��̾�� ��Ż���� �Ÿ��� ���Ѵ�
        transform.position = portal.position + playerOffsetFromPortal;

        //float angleDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        //Quaternion portalRotationDifference = Quaternion.AngleAxis(angleDifferenceBetweenPortalRotations, Vector3.up);
        //Vector3 newCamDir = portalRotationDifference * -playerCam.forward;
        //transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);

        transform.rotation = playerCam.rotation;
    }
}
