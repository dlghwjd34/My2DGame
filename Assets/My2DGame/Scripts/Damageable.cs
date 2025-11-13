using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace My2DGame
{
    /// <summary>
    /// Health를 관리하는 클래스
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        private Animator animator;

        [SerializeField]
        private float currentHealth;

        [SerializeField]
        private float maxHealth = 100f;

        //죽음체크
        private bool isDeath = false;

        //무적모드
        private bool isInvincible = false;
        //무적 모드 타이머
        [SerializeField]
        private float invincibleTimer = 3f;
        private float countdown = 0f;

        //데미지 입을때 호출되는 이벤트 함수
        public UnityAction<float, Vector2> hitAction;

        //힐할때 호출되는 이벤트 함수
        public UnityAction <float> healAction;

        #endregion

        #region Property
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                if(currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        public float MaxHealth
        {
            get { return  maxHealth; }
            set
            {
                maxHealth = value;
            }
        }

        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        private void Update()
        {
            //무적 타이머 - 무적 모드일때
            if (isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown >= invincibleTimer)
                {
                    //타이머 구현
                    isInvincible = false;
                    //타이머 초기화
                    countdown = 0f;
                }
            }
        }

        #endregion

        #region Unity Event Method

        private void Awake()
        {
            //참조
            animator = this.GetComponent<Animator>();
        }

        private void Start()
        {
            //초기화
            CurrentHealth = MaxHealth;
        }

        #endregion

        #region Custom Method
        //데미지 주기
        public void TakeDamage(float damage, Vector2 knockback)
        {
            //죽음체크, 무적체크
            if (isDeath || isInvincible )
                return;
            //무적체크
            //if (isInvincible) return;

            CurrentHealth -= damage;
            Debug.Log($"CurrentHealth : {CurrentHealth}");

            isInvincible = true;

            //애니메이션
            animator.SetTrigger(AnimationString.HitTrigger);

            //데미지 효과 (knockback)
            //hitAction 이벤트에 등록된 함수 호출  -   HitAction 에 knockback효과 이미 들어가있음
            hitAction.Invoke(damage, knockback);

            //데미지 텍스트 연출 효과
            CharacterEvents.characterDamaged?.Invoke(this.transform, damage);
        }

        //힐 하기, 힐 성공 시 true, 실패시 false
        public bool Heal(float healAmount)
        {

            //죽음체크, 무적체크
            if (isDeath)    //죽었으면 힐 할 필요가 없음 but 무적시엔 힐 가능
                return false;

            //체력 만땅 체크
            if(CurrentHealth >= maxHealth)
            {
                return false;
            }

            //리얼 힐 값 구하기    
            float maxHeal = MaxHealth - CurrentHealth; //최대로 힐 할수 있는 값
            float realHeal = (maxHeal < healAmount) ? maxHeal : healAmount; //실제로 힐 하는 값
           
            //힐 하기
            CurrentHealth += realHeal;    //무조건 더한다
            Debug.Log($"CurrentHealth : {CurrentHealth}");

            // healAction null체크 :  healAction 에 등록된 함수가 있는지 여부 체크
            /*if (healAction != null)
            {
                healAction.Invoke(realHeal);
            }*/

            healAction?.Invoke(realHeal);   //위의 if문과 같은 내용을 담고있음

            //데미지 텍스트 연출 효과
            CharacterEvents.characterHeal?.Invoke(this.transform, realHeal);
            //UIManager 작성안되어있음 그거해야함

            return true;           

        }

        #endregion

    }
}