using UnityEngine;
using TMPro;

namespace MyBird
{ 
    /// <summary>
    /// 플레이 화면 score 그리기
    /// </summary>
    public class DrawScoreUI : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
     

        private void Update()
        {
            scoreText.text = GameManager.Score.ToString();
        }


    }
}