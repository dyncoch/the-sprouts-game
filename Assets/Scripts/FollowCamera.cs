using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private Vector3 newVector;

    // Update is called once per frame
    void Update()
    {
        newVector = new Vector3() {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = 10f
        };

        transform.position = Camera.main.ScreenToWorldPoint(newVector);
    }
}
