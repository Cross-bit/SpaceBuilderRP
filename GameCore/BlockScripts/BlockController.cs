using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // public Block b_data;

    public bool drawRay;

    public List<Vector3> origin = new List<Vector3>();
    public List<Vector3> direction = new List<Vector3>();
    public float lenght;
    public Vector3 colDetectionBoxes;

    public SymBlock block;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (drawRay)
        {
            DrawGizmo(origin, direction, lenght);
        }

       // Debug.DrawRay(new Vector3(0,0,1.5f), new Vector3(0, 0, 1) * 3f);
    }

    public void DrawGizmo(List<Vector3> origin, List<Vector3> direction, float lenght)
    {
        for (int i = 0; i < origin.Count; i++)
        {
            Debug.DrawRay(origin[i], direction[i] * lenght, Color.red);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
        Gizmos.DrawCube(this.transform.position, colDetectionBoxes);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        Debug.Log("Hello");
    }

    /*private void  (Collision collision)
    {

       // b_data.OnCollisionStay(collision);
    }*/

    /*void  (Collider other)
    {
        b_data.OnTriggerEnter(other);
    }*/
}
