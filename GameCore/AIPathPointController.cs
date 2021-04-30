using UnityEngine;

public class AIPathPointController : MonoBehaviour
{
    public bool renderPoints;
    private void OnDrawGizmos()
    {
        if (renderPoints)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(this.transform.position, 1f);
        }
    }
}
