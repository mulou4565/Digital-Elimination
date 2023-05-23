using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;

namespace Console2048
{
    /// <summary>
    /// 附加到每个方格中，用于定义方格行为
    /// </summary>
    public class NumberSprite : MonoBehaviour
    {
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();    
        }
        /// <summary>
        /// 设置图片数字
        /// </summary>
        /// <param name="number"></param>
        public void SetImage(int number)
        {
            image.sprite = ResourceManager.LoadSprite(number);
        }

        //移动 合成 生成效果
        public void CreateEffect()
        {
            iTween.ScaleFrom(gameObject, Vector3.zero, 0.3f);
        }
    }
}