using UnityEngine;
using System.Collections;

public class VoidZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Caiste al vacio...");
            StartCoroutine(Respawn(other));
        }
    }

    private IEnumerator Respawn(Collider other)
    {
        CharacterController cc = other.GetComponent<CharacterController>();
        PlayerMovement movement = other.GetComponent<PlayerMovement>();

        // Deshabilita el CharacterController y el PlayerMovement del jugador
        if (movement != null) movement.enabled = false;
        if (cc != null) cc.enabled = false;

        // Espera 3 segundos antes de reaparecer al jugador
        yield return new WaitForSeconds(1f);

        // Mueve al jugador al punto de reaparición y vuelve a habilitar los componentes
        if (cc != null)
        {
            cc.transform.position = respawnPoint.position;
            cc.enabled = true;
        }
        if (movement != null) movement.enabled = true;
    }
}
