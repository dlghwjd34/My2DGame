using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// HitBox에 충돌한 적에게 데미지를 주는 클래스
    /// </summary>
    public class Attack : MonoBehaviour
    {
        //Attack은 딱 충돌이 일어나는 순간 처리



        #region Variables
        //공격시 적에게 주는 데미지 양
        [SerializeField]
        private float attackDamage = 10f;

        //공격시 넉백 효과
        [SerializeField]
        private Vector2 knockback = Vector2.zero;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log($"적에게 {attackDamage} 의 데미지를 준다");
            Damageable damageable = collision.GetComponent<Damageable>();
            //여 콜라이더에 Damagable 이 붙었는지 물어봄
            if( damageable != null )
            {
                Debug.Log($"damage {collision.name}");
                //넉백 효과 방향 설정
                Vector2 deliveredKnockback = this.transform.parent.localScale.x > 0f ?
                    knockback : new Vector2(knockback.x, knockback.y);

                damageable.TakeDamage(attackDamage, deliveredKnockback);
            }
        }
        #endregion

        #region
        #endregion

    }
}