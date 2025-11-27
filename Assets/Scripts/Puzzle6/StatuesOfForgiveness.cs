using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class StatuesOfForgiveness : MonoBehaviour
{
    private float crazyRotationSpeed = 300f;
    private float calmLookSpeed = 2f;

    [SerializeField] private bool isCalm = true;
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
    }

    void Update()
    {
        if (isCalm)
        {
            // Sigue con la mirada al jugador 
            Vector3 dir = player.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * calmLookSpeed);
        }
    }

    public void TriggerJudgement()
    {
        isCalm = false;
        StartCoroutine(CrazyReaction());
    }

    public IEnumerator CrazyReaction()
    {
        float t = 1f;

        while (t > 0)
        {
            transform.Rotate(Vector3.up * crazyRotationSpeed * Time.deltaTime);
            t -= Time.deltaTime;
            yield return null;
        }

        isCalm = true;
    }
}
