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
        private Animator animator;

        //이동
        [SerializeField] private float walkSpeed = 3f;

        //달리는 속도
        [SerializeField]
        public float runSpeed = 6f;

        //반전
        private bool isFacingRight = true;

        //입력 값
        private Vector2 inputMove = Vector2.zero;

        //걷기
        private bool isMove = false;
        //뛰기
        private bool isRun = false;

        //점프
        [SerializeField]
        private float jumpForce = 5f;
        #endregion

        #region Property

        public bool IsFace
        {
            get { return isFacingRight; }
            private set
            {
                //반전 구현
                if(isFacingRight != value)   //새로 입력된 value 값이 틀리냐 > Yes - 반전
                {
                    this.transform.localScale *= new Vector2(-1, 1);    //반전시켜라
                }

                isFacingRight = value;
            }
        }


        public bool IsMove
        {
           get {  return isMove; }
           private set
            { 
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            }
        }

        public bool IsRun
        {
            get { return isRun; }
            private set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        //현재 이동 속도
        public float CurrentMoveSpeed
        {
            get
            {
                if(CannotMove)  //애니메이터 파라미터 값 읽어오기
                {
                    return 0f;
                }



                if(IsMove &&TouchingDirections.IsWall == false) //이동가능
                {
                    if9


                    if(IsRun)
                    {
                        return runSpeed;
                    }
                    else //이동불가
                    {
                        return walkSpeed;
                    }
                }
                
                else //이동불가
                {
                    return 0f;
                }
                               
            }        
            
        }

        //애니메이터의 파라미터값 가져오기
        public bool CannotMove
        {
            get
            {
                return animator.GetBool(AnimationString.CannotMove);
            }
        }

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조 -- 이 프로젝트 대부분의 참조는 Awake 로
            rb2D = this.GetComponent<Rigidbody2D>();
            animator = this.GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            //좌우이동()
            // * Time.deltaTime 을 생략
            //Why - * Time.deltaTime 삽입이유 : 어떤 디바이스에서든 똑같은 속도를 보장하기 위함
            // 이 경우 굳이...
            rb2D.linearVelocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.linearVelocity.y);

            //점프 애니메이션
            /*amulator.SetFlaot(AnimationString,Yveolco
              fof )*/

        }

        #endregion


        #region Custom Method

        //방향 전환
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x > 0f && isFacingRight == false) //오른쪽으로 이동
            {
                isFacingRight = true;
            }
            else if ( moveInput.x < 0f && isFacingRight == true) //왼쪽으로 이동
            {
                isFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            IsMove = (inputMove != Vector2.zero);
            //방향 전환
            SetFacingDirection(inputMove);

        }

        //런 입력 처리
        public void OnRun(InputAction.CallbackContext context)
        {
            if( context.started) // 버튼을 눌렀을 때
            {
                IsRun = true;
            }
            else if(context.canceled) // 버튼을 뗄 때
            {
                IsRun = false;
            }
        }

        //점프 입력 처리
        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                Debug.Log("플레이어가 점프했습니다");
                animator.SetTrigger(AnimationString.JumpTrigger);
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            }
        }

        /*
        //공격 입력 처리
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.started && TouchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
        }
        */
        #endregion
    }
}