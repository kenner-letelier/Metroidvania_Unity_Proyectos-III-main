using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{

    public enum LayerChekerType
    {
        Ray,
        Circle
    }

    [SerializeField]
    LayerChekerType layerCheckerType;

    [SerializeField] LayerMask targetMask;
    [SerializeField] Vector2 direction;
    [SerializeField] float distance;

    public bool isTouching;

   
    
    void Update()
    { 
        if(layerCheckerType == LayerChekerType.Ray)
        {
            isTouching = Physics2D.Raycast(this.transform.position, direction, distance, targetMask);
        }
        if (layerCheckerType == LayerChekerType.Circle)
        {
            isTouching = Physics2D.OverlapCircle(this.transform.position, distance, targetMask);
        }

    }

//#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isTouching)
        {
            Gizmos.color = Color.green;
        }
        else {
            Gizmos.color = Color.yellow;
             }

        if (layerCheckerType == LayerChekerType.Ray)
        {
            Gizmos.DrawRay(this.transform.position, direction * distance);
        }

        if (layerCheckerType == LayerChekerType.Circle)
        {
            Gizmos.DrawWireSphere(this.transform.position, distance);
        }

    }
}

//#endif 
