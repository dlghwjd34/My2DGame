using UnityEngine;

namespace My2DGame
{
    //시차에 의한 배경 움직임 구현 
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public Camera cam;              //카메라 오브젝트
        public Transform followTarget;  //플레이어

        private Vector2 startPosition;  //배경 오브젝트의 최초 위치
        [SerializeField]
        private float startZ = 5f;           //배경의 깊이
        #endregion

        #region Property
        //시작지점으로 부터 카메라의 이동 거리
        public Vector2 CamMoveSinceStart => startPosition - (Vector2) cam.transform.position;

        //플레이어와 배경과의 거리
        public float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

        //
        public float clippingPlane => cam.transform.position.z + (zDistanceFromTarget < 0f ? cam.farClipPlane : cam.nearClipPlane);

        public float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

        // 시차 계수
        private void Start()
        {
            //초기화
            startPosition = this.transform.position;
            startZ = this.transform.position.z;
        }

        private void Update()
        {
            Vector2 newPosition = startPosition * CamMoveSinceStart * ParallaxFactor;
            this.transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
        }


        #endregion



    }
}