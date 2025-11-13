using UnityEngine;
using UnityEngine.Events;

namespace My2DGame
{

    public class CharacterEvents : MonoBehaviour
    {
        //캐릭터가 데미지를 입을때 함수 호출하는 이벤트 함수
        public static UnityAction <Transform, float> characterDamaged;

        //캐릭터가 힐할때 동록된 함수 호출하는 이벤트 함수
        public static UnityAction <float> characterHeal;

    }
}