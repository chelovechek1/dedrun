using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserMaxDistanse;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Material material;
    private float time = 0;
    private int reflectionCount;

    void Update()
    {
        time += Time.deltaTime;
        lineRenderer.sharedMaterial.SetFloat("_time", time);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, layerMask);
        if (hit)
        {
            lineRenderer.sharedMaterial.SetFloat("_length", hit.distance);
            Draw2dRay(transform.position, hit.point);
        }

        else
        {
            Draw2dRay(transform.position, transform.right * laserMaxDistanse);
            lineRenderer.sharedMaterial.SetFloat("_length", laserMaxDistanse);
        }
        Destroy(gameObject, 50 * Time.deltaTime);
    }
    void Draw2dRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPositions(new Vector3[] { startPos, endPos });
    }
}
