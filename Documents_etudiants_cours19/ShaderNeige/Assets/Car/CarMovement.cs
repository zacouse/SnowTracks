using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

 

    [SerializeField] public List<Wheel> wheels;
    public float maxWheelTurnAngle;
    public float motorStrenght;

  
	
	// Update is called once per frame
	void Update () {
        AjustWheelRotation();

    }
    
    public void AjustWheelRotation() {
        Quaternion rot;
        Vector3 pos;
        float turnAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");
        float force = motorStrenght * Input.GetAxis("Vertical");
        foreach ( Wheel wheel in wheels) {
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelMesh.transform.position = pos;
            wheel.wheelMesh.transform.rotation = rot;
            if (wheel.isFront) {
                wheel.wheelCollider.steerAngle = turnAngle;
                wheel.wheelCollider.motorTorque = force*2;
            }
        }

       
      

    }

    [System.Serializable]
    public class Wheel
    {
        public WheelCollider wheelCollider;
        public GameObject wheelMesh;
        public bool isFront;
    }
}
