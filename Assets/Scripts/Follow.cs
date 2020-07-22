using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject FollowObject;
    public float Z;
    public float MaxZoomout;
    public GameObject PlayerShip;

    private void CalculateCameraOffset()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
        Vector3 mousePos = Input.mousePosition;
        Vector3 dirVector = mousePos - screenCenter;
        float distance = Mathf.Sqrt(Mathf.Pow(dirVector.x, 2) + Mathf.Pow(dirVector.y, 2));
        gameObject.transform.position += dirVector.normalized * distance / 200;
        // Debug.Log(distance);
        // Debug.Log($"X:{dirVector.x}, Y:{dirVector.y}, Z:{dirVector.z}");
    }
    private void FollowObj()
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = new Vector3(FollowObject.transform.position.x, FollowObject.transform.position.y, FollowObject.transform.position.z + Z);
    }
    // Start is called before the first frame update
    private void Start()
    {
        PlayerShip = GameObject.FindGameObjectWithTag("PlayerShip");

    }

    // Update is called once per frame
    private void Update()
    {
        FollowObj();
        CalculateCameraOffset();
    }
}
