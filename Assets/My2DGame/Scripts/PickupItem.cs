using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 맵에 떨어진 아이템을 픽업하는 기능
    /// 픽업시 아이템 효과 구현, 아이템 회전
    /// </summary>
    public class PickupItem : MonoBehaviour
    {

        #region Variables
        //아이템 효과 - Hp 회복
        [SerializeField]
        private float healthRestore = 10f;

        //아이템 회전 연출
        private Vector3 rotationSpeed = new Vector3(0, 180f, 0);

        #endregion

        #region Unity Event System

        private void Update()
        {
            //회전
            this.transform.eulerAngles += Time.deltaTime * rotationSpeed;
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            //아이템 픽업
            if (Pickup(collision) == true)
            {
                //아이템 픽업
                bool isPickup = Pickup(collision);

                if (isPickup)
                {
                    Destroy(gameObject);
                }
            }
        }
        #endregion

        #region Custom Method
        //픽업시 아이템 효과 구현, 픽업 성공시 true, 실패시 false - hp회복
        //힐 기능 외에 여러가지 아이템 주웠을때의 효과 구현
        protected virtual bool Pickup (Collider2D collision)
        {
            bool isUse = false;

            //Debug.Log($"아이템을 픽업 했습니다");
            Damageable damageable = collision.GetComponent<Damageable>();

            if(damageable != null)  // ==Damageable이 붙었다.
            {
                
                if(isUse)
                {
                    isUse = damageable.Heal(healthRestore);
                    //성공 - 힐성공, 실패 - 힐실패
                }

                return isUse;
            }


        }
        #endregion

    }
}