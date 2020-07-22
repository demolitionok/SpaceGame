using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Controls
{
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Right;
    public KeyCode Left;
}

public class PlayerController : MonoBehaviour
{
    public Controls PlayerKeys { get; private set; }

    public float ThrustMultiplier;
    public float TorqueMultiplier;

    [HideInInspector]
    public float MoveThrust { get; private set; }
    [HideInInspector]
    public float MoveTorque { get; private set; }

    public Vector3 ForwardVec;
    private void ObtainControls()
    {
        using (StreamReader inputFile = new StreamReader("F:\\noHacker\\CSharp\\SSpace\\Assets\\Configs\\keybindings.json"))
        {
            string json = inputFile.ReadLine();
            PlayerKeys = JsonUtility.FromJson<Controls>(json);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ObtainControls();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTorque = 0;
        MoveThrust = 0;

        if (Input.GetKey(PlayerKeys.Up))
        {
            MoveThrust = ThrustMultiplier;
        }
        else if (Input.GetKey(PlayerKeys.Down))
        {
            MoveThrust = -ThrustMultiplier;
        }
        
        if (Input.GetKey(PlayerKeys.Right))
        {
            MoveTorque = TorqueMultiplier;
        }
        else if (Input.GetKey(PlayerKeys.Left))
        {
            MoveTorque = -TorqueMultiplier;
        }
    }
}
