using UnityEngine;
using System.Collections;

public class WoodDeliveryZone : MonoBehaviour
{
    [SerializeField] private CollectWoodMission mission;
    public void RepairBridge()
    {
        if (mission == null)
        {
            Debug.LogWarning("⚠️ No se asignó la misión al puente.");
            return;
        }

        if (mission.HasEnoughWood())
        {
            Debug.Log("Usas la madera recolectada para reparar el puente...");
            StartCoroutine(RepairBridgeRoutine());
        }
        else
        {
            Debug.Log("❌ Aún no tienes suficiente madera para reconstruir el puente.");
        }
    }

    private IEnumerator RepairBridgeRoutine()
    {
        Debug.Log("Reparando...");
        yield return new WaitForSeconds(3f);
        mission.CompleteMission();
        Destroy(gameObject);
    }
}
