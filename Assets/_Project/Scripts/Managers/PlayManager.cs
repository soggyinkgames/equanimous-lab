using UnityEngine;
using UnityEngine.UIElements;

// an example script to visualize some element data in the play manager inspector drawer to edit the ui in ui builder
namespace SoggyInkGames.Equanimous.Lab.Manager
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayManager : MonoBehaviour
    {
        public GameObject m_ElementPrefab;

        public ElementManager[] m_Elements;
    }
}
