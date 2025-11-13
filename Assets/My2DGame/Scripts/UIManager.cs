using TMPro;
using UnityEngine;

namespace My2DGame
{
  
    public class UIManager : MonoBehaviour
    {
        #region Variables
        //참조
        private Canvas gameCanvas;

        public GameObject damageTextPrefab; //데미지 텍스트 연출 프리팹
        public GameObject ealRextPrefab;    //힐 텍스트 연출 프리팹
        #endregion


        #region Unity Event Method

        private void Awake()
        {
            //참조
            gameCanvas = FindFirstObjectByType<Canvas>();
        }
        private void OnEnable()
        {
            //이벤트 함수 등록
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHeal += CharacterHeal;

        }

        private void OnDisable()
        {
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHeal -= CharacterHeal;

        }


        #endregion







        #region

        public void CharacterTakeDamage(Transform character, float damageRecieved)
        {
            //캐릭터 머리 위 위치 가져옥
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.position);

            GameObject textGo = Instantiate(damageTextPrefab, new Vector3(spawnPosition.x, spawnPosition.y + 70f, spawnPosition
                Quaternion.identity, gameCanvas.transform));

            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damageRecieved.ToString();
        }

        //캐릭터가 힐 할때 호출되는 함수 - 힐량 텍스트 연출
        //매개변수로 캐릭터의 오브젝트, 힐량 입력받아 처리
        public void CharacterHeal(Transform character, float healAmount)
        {
            //캐릭터 머리 위 위치 가져옥
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.position);

            GameObject textGo = Instantiate(damageTextPrefab, new Vector3(spawnPosition.x, spawnPosition.y + 70f,
                Quaternion.identity, gameCanvas.transform));

            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = healAmount.ToString();
        }
    }

        #endregion
    }
}
    

