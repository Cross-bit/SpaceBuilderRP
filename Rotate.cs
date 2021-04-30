using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed;
    // pdate is called once per frame
    private void Start()
    {
      //  speed = Random.Range(-6.5f,8f);             
    }

    void FixedUpdate () {
        transform.Rotate(new Vector3(0, 0, Time.fixedDeltaTime * speed));

	}
}
