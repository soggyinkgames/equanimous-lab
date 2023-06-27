using UnityEngine;
using UnityEngine.UIElements;

// an example script to visualize some element data in the play manager inspector drawer to edit the ui in ui builder
namespace SoggyInkGames.Equanimous.Lab.Managers
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayManager : MonoBehaviour
    {
        [Header("This is a Header")]
        public float m_ElementRandomRange = 20;
        [Range(10, 40)] public float m_ElementForce = 25;
        public int m_ElementCount = 10;
        public float m_ElementDelay = 0.1f;
        public GameObject m_ElementPrefab;

        public ElementManager[] m_Elements;
    }
}
