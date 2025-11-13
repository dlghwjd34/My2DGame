using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2DGame
{
    /// <summary>
    /// 트리거 충돌체에 들어오는 모든 충돌체 감지해서 리스트에 수집
    /// </summary>
    public class DetectionZone : MonoBehaviour
    {
        //Detection - 순간 X / 감지된것을 오래 들고있어야 하는 경우 사용
        //e.g : Enemy가 Player의 존재를 감지하고 계속 공격을 해야 하는경우



        #region Variables
        //감지된 충돌체 리스트
        public List<Collider2D> detectedColliders = new List<Collider2D>();

        //모든 충돌체의 갯수가 0이 되는순간 호출되는 이벤트 함수
        public UnityAction noRemainColliders;

        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //충돌체가 들어오면 리스트에 추가
            detectedColliders.Add(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //충돌체에서 나가면 리스트에서 제거
            detectedColliders.Remove(collision);

            //더이상 충돌체 리스트에 아무것도 없을 때 //순간을 찾아서 그 시점에만 반전을 시켜주어야 함.... 어렵다
            //등록된 함수 호출
            noRemainColliders?.Invoke();
                             
        }

        #endregion

    }
}