using UnityEngine;
using UnityEngine.UI;

namespace TORISOUP.UnityGifRecorder.Demo
{
    public class Animations : MonoBehaviour
    {
        [SerializeField] private Transform _cube;
        [SerializeField] private Text _text;

        private void Update()
        {
            _cube.rotation = Quaternion.AngleAxis(30.0f * Time.deltaTime, Vector3.up) * _cube.rotation;
            _text.text = Time.time.ToString("F1");
        }
    }
}