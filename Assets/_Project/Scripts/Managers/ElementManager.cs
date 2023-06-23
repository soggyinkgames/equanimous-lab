using System;
using UnityEngine;

// an example script to visualize some element data in the play manager inspector drawer  to edit the ui in ui builder
namespace SoggyInkGames.Equanimous.Lab.Manager
{
    [Serializable]
    public class ElementManager
    {
        public Color m_ElementColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_ElementNumber;
        [HideInInspector] public GameObject m_Instance;

        public Color color
        {
            get
            {
                Color color;
                if (m_ElementColor.r + m_ElementColor.g + m_ElementColor.b > 0.01f)
                {
                    color = m_ElementColor;
                }
                else
                {
                    var r = (float)(m_ElementNumber * 41 % 255) / 255.0f;
                    var g = (float)(m_ElementNumber * 11 % 255) / 255.0f;
                    var b = (float)(m_ElementNumber * 109 % 255) / 255.0f;

                    color = new Color(r, g, b);
                }

                return color;
            }
        }


        // Used at the start of each round to put the tank into it's default state.
        public void Reset ()
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive (false);
            m_Instance.SetActive (true);
        }
    }
}