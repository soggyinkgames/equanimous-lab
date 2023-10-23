using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


namespace SoggyInkGames.Equanimous.Lab.WebGLView
{

    public class TakeMeToWeb : MonoBehaviour
    {
        public string webPage = "about";

        public string test = "boo";
        private Camera _mainCamera;

        private Renderer _renderer;

        private Ray _ray;
        private RaycastHit _hit;


        #region DllImport

        [DllImport("__Internal")]
        private static extern void Alert();

        // [DllImport("__Internal")]
        // private static extern void ToWebpage();

        [DllImport("__Internal")]
        private static extern void ToWebPage(string webPage);

        [DllImport("__Internal")]
        private static extern void ToWebTesting(string test);

        //     mergeInto(LibraryManager.library, 
        // {
        //     Alert: function () {
        //         window.alert("lets do something silly!")
        //     },

        //     ToWebPage: function (pageString) {
        //         window.location.assign("https://anai.netlify.app/" +Pointer_stringify(pageString))
        //     },

        //     ToWebTesting: function (pageString) {
        //         window.location.assign("https://anai.netlify.app/" +UTF8ToString(pageString))
        //     },
        // })

        #endregion

        #region UnityToJS

        public void UnityCallJSFunc()
        {
            ToWebTesting(test);
            ToWebPage(webPage);
        }

        #endregion

        private void Start()
        {
            _mainCamera = Camera.main;
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, 100f))
                {
                    if (_hit.transform == transform)
                    {

                        Debug.Log("Click");

                        UnityCallJSFunc();

                        _renderer.material.color = _renderer.material.color == Color.red ? Color.blue : Color.red;
                    }
                }
            }
        }
    }
}
