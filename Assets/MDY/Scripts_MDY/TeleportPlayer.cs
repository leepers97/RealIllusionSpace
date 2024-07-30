using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform targetLocation; // 목표 위치

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name); // 디버깅 메시지 추가

        Transform playerTransform = other.transform;

        // 충돌체의 부모에서 "Player" 태그를 찾기
        while (playerTransform != null)
        {
            if (playerTransform.CompareTag("Player"))
            {
                Debug.Log("Player detected: " + playerTransform.name); // 디버깅 메시지 추가
                Teleport(playerTransform);
                break;
            }
            playerTransform = playerTransform.parent;
        }
    }

    private void Teleport(Transform player)
    {
        Debug.Log("Teleporting player to: " + targetLocation.position); // 디버깅 메시지 추가
        player.position = targetLocation.position;
    }
}