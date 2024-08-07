using UnityEngine;

public class WindZone1 : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); // 바람의 방향
    public float windStrength = 10f; // 바람의 강도

    private void OnTriggerEnter(Collider other)
    {
        WindEffect windEffect = other.GetComponent<WindEffect>();
        if (windEffect != null)
        {
            windEffect.ApplyWind(windDirection, windStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WindEffect windEffect = other.GetComponent<WindEffect>();
        if (windEffect != null)
        {
            windEffect.RemoveWind();
        }
    }
}