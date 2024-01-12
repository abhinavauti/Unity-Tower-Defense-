using UnityEngine;
using Core.Health;
using UnityEngine.AI;

public class SpawnOnDestruction : MonoBehaviour
{
    public GameObject hovertankPrefab; // Assign your Hovertank prefab in the inspector

    private DamageableBehaviour damageableBehaviour;

    private void Awake()
    {
        damageableBehaviour = GetComponent<DamageableBehaviour>();

        // Subscribe to the died event
        damageableBehaviour.died += OnDied;
    }

    private void OnDied(DamageableBehaviour damageable)
    {
        // Instantiate the Hovertank at this object's position and assign it to a variable
        GameObject hovertankInstance = Instantiate(hovertankPrefab, transform.position, Quaternion.identity);

        // Activate its Nav Mesh Agent Component
        NavMeshAgent hovertankAgent = hovertankInstance.GetComponent<NavMeshAgent>();
        if (hovertankAgent != null)
        {
            hovertankAgent.enabled = true;
        }
        else
        {
            Debug.LogError("Hovertank does not have a Nav Mesh Agent Component!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the died event
        damageableBehaviour.died -= OnDied;
    }
}
