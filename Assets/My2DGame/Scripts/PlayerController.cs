using UnityEngine;
using UnityEngine.InputSystem;

namespace My2DGame
{
    /// <summary>
    /// 플레이어를 제어하는 클래스
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rb2D;

        //이동
        [SerializeField]
        private float walkSpeed = 5f;

        //입력 값
        private Vector2 inputMove = Vector2.zero;
        #endregion


        #region Unity Event Method
        private void Awake()
        {
            //참조 -- 이 프로젝트 대부분의 참조는 Awake 로
            rb2D = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //이동()

            // * Time.deltaTime 을 생략
            //Why - * Time.deltaTime 삽입이유 : 어떤 디바이스에서든 똑같은 속도를 보장하기 위함
            // 이 경우 굳이...

            //rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, inputMove.y * walkSpeed);
            //rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, 0f);
            //rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed,  inputMove.y * walkSpeed);

        }

        #endregion


        #region Custom Method
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            Debug.Log(inputMove);
        }
        #endregion
    }
}