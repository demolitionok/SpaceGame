using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> Engines = new List<GameObject>();
    GameObject Head;
    PlayerController playerCont;
    Rigidbody rigidBody;
    private Vector3 forwardVec;
    public void RotatePlayer()
    {
        var torque = playerCont.MoveTorque;
        rigidBody.AddForceAtPosition(-gameObject.transform.right * torque, Engines[0].transform.position, ForceMode.Force);
        rigidBody.AddForceAtPosition(gameObject.transform.right * torque, Engines[1].transform.position, ForceMode.Force);
    }
    public void MovePlayer()
    {
        var thrust = playerCont.MoveThrust;
        rigidBody.AddForce(forwardVec * thrust, ForceMode.Force);
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerCont = gameObject.GetComponent<PlayerController>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        Head = GameObject.Find("Head");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        forwardVec = (Head.transform.position - gameObject.transform.position).normalized;

        RotatePlayer();
        MovePlayer();
    }
}
