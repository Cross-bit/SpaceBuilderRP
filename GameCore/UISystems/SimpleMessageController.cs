using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMessageController : MonoBehaviour
{
    public bool move;
    private Vector4 screenBounds;
    public float distanceFromEdges = 5f;
    public Vector3 translateSpeed;

    private void OnEnable(){
        this.transform.localPosition = new Vector3(Screen.width/2, Screen.height/2, 0f);
    }

    private void Start()
    {
        screenBounds = new Vector4(distanceFromEdges, Screen.width - distanceFromEdges, distanceFromEdges, Screen.height - distanceFromEdges);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
        if (this.transform.localPosition.x  >= this.screenBounds.x && this.transform.localPosition.x <= this.screenBounds.y && this.transform.localPosition.y >= this.screenBounds.z && this.transform.localPosition.y <= this.screenBounds.w)
        {
            this.transform.localPosition += translateSpeed;
        }
        else
        {
            this.gameObject.SetActive(false);
            move = false;
        }
    }
}
