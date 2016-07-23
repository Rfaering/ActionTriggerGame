using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
    public class ImageSetter : MonoBehaviour
    {
        public void SetImage(string path, Sprite image)
        {
            var transformInQuestion = transform.Find(path);
            if (transformInQuestion.GetComponent<Image>() != null)
            {
                transformInQuestion.gameObject.SetActive(true);
                transformInQuestion.GetComponent<Image>().sprite = image;
            }

            if (transformInQuestion.GetComponent<SpriteRenderer>() != null)
            {
                transformInQuestion.GetComponent<SpriteRenderer>().sprite = image;
            }
        }

        public void ClearImages(string underPath)
        {
            var gameObjectInQuestion = transform.Find(underPath);

            foreach (var item in gameObjectInQuestion.GetComponentsInChildren<Image>(true))
            {
                if (item.name != underPath)
                {
                    item.sprite = null;
                    item.gameObject.SetActive(false);
                }
            }

            foreach (var item in gameObjectInQuestion.GetComponentsInChildren<SpriteRenderer>(true))
            {
                if (item.name != underPath)
                {
                    item.sprite = null;
                }
            }
        }
    }
}


