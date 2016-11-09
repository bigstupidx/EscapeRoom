using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TestSearch
{
    public class TestFocusable : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Searchable search;
        public Canvas nm;
        public enum Functions
        {
            Start,
            OnGazeEnter,
            OnGazeExit,
        }

        public void Start()
        {
            OnPointerEnter();
            OnPointerExit();
        }
        public void OnPointerEnter()
        {
            Enter(objection);
            if (objection.GetComponent<Renderer>().material.shader.name.Equals("Outlined/Diffuse"))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);

        }
        public void OnPointerExit()
        {
            Exit(objection);
            if (objection.GetComponent<Renderer>().material.shader.name.Equals("Standard"))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);

        }
    }
}
