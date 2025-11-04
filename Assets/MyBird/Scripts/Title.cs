using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MyBird
{
    /// <summary>
    /// 씬 이동
    /// </summary>
    public class Title : MonoBehaviour
    {
        #region
        [SerializeField]
        private string loadToScene = "PlayScene";

        public Button playButton;
        //플레이 버튼 클릭시 호출
        public void Play()
        {
            SceneManager.LoadScene(loadToScene);
        }
        #endregion
    }
}