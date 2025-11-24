using UnityEngine;
using System.Collections;

public class WoodDelivery : MonoBehaviour
{
    [SerializeField] private CollectWoodMission mission;
    public void RepairBridge()
    {
        if (mission == null)
        {
            Debug.LogWarning("No se asignó la misión al puente");
            return;
        }

        mission.DeliverWood();

        if (mission.HasEnoughWood())
        {
            Debug.Log("Ya tienes suficiente!");
            StartCoroutine(RepairBridgeRoutine());
        }
        else
        {
            Debug.Log("Aún no tienes suficiente madera");
        }
    }

    private IEnumerator RepairBridgeRoutine()
    {
        Debug.Log("Reparando...");
        yield return new WaitForSeconds(3f);
        mission.CompleteMission();
        Debug.Log("Mision completa!");
        Destroy(gameObject);
    }
}
