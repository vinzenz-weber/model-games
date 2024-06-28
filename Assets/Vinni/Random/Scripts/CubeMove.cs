using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;




namespace Vinni
{


    public class CubeMove : MonoBehaviour
    {
        private Rigidbody rb; 

        public float moveSpeed = 5f;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(Vector3.right * moveSpeed);
        }
    }
}
