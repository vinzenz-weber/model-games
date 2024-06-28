using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vinni
{
    public class game_logic : MonoBehaviour
    {

        public GameObject gameObject;
        //public Collision_logic collision_logic;

        public int endScore = 3;

        private int missedHits;

        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            //missedHits = collision_logic.floorHit;
            //EndGame();
        }




        void SpawnGameObject()
        {
            // Define the area where the GameObject can spawn
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 10, Random.Range(-5f, 5f));

            // Instantiate the GameObject at the spawn position
            Instantiate(gameObject, spawnPosition, Quaternion.identity);
        }

        void Start()
        {
            // Call the SpawnGameObject method every 5 seconds, starting after 0 seconds
            InvokeRepeating("SpawnGameObject", 0f, 5f);
        }
    }

}

