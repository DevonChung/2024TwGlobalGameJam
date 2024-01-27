using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

    public class MyCameraManager : MonoBehaviour
    {
        // Start is called before the first frame update

        public Bounds cameraBounds;
        public float CameraSpeed = 1;
        public Vector3 posOffset = new Vector3(0,2,-10);

        protected GameObject Hero;

        float mapX = 10.0f;
        float mapY = 10.0f;

        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        public void Start()
        {
            Hero = GameObject.FindGameObjectWithTag("Player");
       
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            mapY = cameraBounds.extents.y * 2;
            mapX = cameraBounds.extents.x * 2;
            // Calculations assume map is position at the origin
            minX = horzExtent - mapX / 2.0f + cameraBounds.center.x;
            maxX = mapX / 2.0f - horzExtent + cameraBounds.center.x;
            minY = vertExtent - mapY / 2.0f + cameraBounds.center.y;
            maxY = mapY / 2.0f - vertExtent + cameraBounds.center.y;
       
    }

        // Update is called once per frame
        void Update()
        {
            
        }

    private void LateUpdate()
    {
        if (Hero != null) {

            
            transform.position = Hero.transform.position + posOffset;
            var v3 = transform.position;
            v3.x = Mathf.Clamp(v3.x, minX, maxX);
            v3.y = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = v3;

        }
    }
}

