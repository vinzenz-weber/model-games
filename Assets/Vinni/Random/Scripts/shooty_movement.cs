using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinni
{


    public class shooty_movement : MonoBehaviour
    {
        //public Interaction_types initeraction_type;
        public LayerMask ignoreMe;
        public Transform crosshair_t;
        public Transform push_object;
        public float pushing_strength = 1f;

        public GameObject[] jumpy_objects;
        public GameObject[] away_objects;

        private int gameplay_layer = 3;
        private int push_object_layer = 7;

        // Start is called before the first frame update
        void Start()
        {
            // jumpy_objects = GameObject.FindGameObjectsWithTag("jumpy");
            // away_objects = GameObject.FindGameObjectsWithTag("away");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastHit hit;
            var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            crosshair_t.position = new Vector3(crosshair_t.position.x, -1f, crosshair_t.position.z); //below floor




            ignoreMe |= (1 << gameplay_layer); //add gameplay_layer
            ignoreMe |= (1 << push_object_layer); //add push_object_layer

            if (Physics.Raycast(ray, out hit, 500f, ~ignoreMe) && hit.collider.name == "Floor")
            {
                Vector3 power_direction = hit.point - push_object.GetComponent<Transform>().position;
                float length = power_direction.magnitude;
                push_object.GetComponent<Rigidbody>().AddForce(power_direction * 1000f * pushing_strength + power_direction.normalized * 5, ForceMode.Force);
            }
        }
    }
}
