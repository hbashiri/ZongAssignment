using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class PlayMenuSfxScript : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        
        private void OnClick()
        {
            MainMenu.Instance.PlaySfx();
        }
    }
}