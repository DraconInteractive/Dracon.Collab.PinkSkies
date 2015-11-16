using System;
using UnityEngine;


namespace UnityStandardAssets.Utility{

    public class FollowTarget : MonoBehaviour{
        public Transform target;
        public Vector3 offset, origin;
		public bool moving;
		public float rotateSpeed = 5;
		public Quaternion OriginQ;


		void Start(){
			offset = target.transform.position - transform.position;
			moving = true;
			origin = target.transform.position;
			OriginQ = new Quaternion (0,0,0,0);
		}
		        
		public void LateUpdate(){
			if (Input.GetMouseButtonDown (1)) {
				moving = false;
			} else if (Input.GetMouseButtonUp (1)){
				moving = true;
				target.transform.rotation = OriginQ;
			}

			if (moving == false) {
				float camX = Input.GetAxis ("Mouse X") * rotateSpeed;
				float camY = Input.GetAxis ("Mouse Y") * rotateSpeed;
				target.transform.Rotate (camY, camX, 0);
				
				float desiredAngleY = target.transform.eulerAngles.y;
				float desiredAngleX = target.transform.eulerAngles.x;
				Quaternion rotation1 = Quaternion.Euler (desiredAngleX, desiredAngleY, 0);
				transform.position = target.transform.position - (rotation1 * offset);
			} else {
				float desiredAngleY = target.transform.eulerAngles.y;
				float desiredAngleX = target.transform.eulerAngles.x;
				Quaternion rotation1 = Quaternion.Euler (desiredAngleX, desiredAngleY, 0);
				transform.position = target.transform.position - (rotation1 * offset);
			}
			transform.LookAt(target.transform);
		}
   
    }
}
