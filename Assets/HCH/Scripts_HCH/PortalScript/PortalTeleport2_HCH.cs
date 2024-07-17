using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport2_HCH : MonoBehaviour
{
    // ������ ������ �ݴ��� ��Ż ������ �ڷ���Ʈ �ϰ� �ʹ�
    public Transform player;
    public Transform TeleportTargetPortal;
    public float offset;

    bool isPlayerOverlapping = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPlayerOverlapping)
        //{
        //    player.position = TeleportTargetPortal.position * offset;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOverlapping = true;
            player.position = TeleportTargetPortal.position - Vector3.forward * offset;
            player.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y + 180, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOverlapping = false;
        }
    }
}
