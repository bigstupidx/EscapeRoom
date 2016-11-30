using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TestSearch
{
    public class TestFocusable : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Focusable focus;
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
            if (gameObject.GetComponent<Focusable>().IsHighlighted(gameObject))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);

        }
        public void OnPointerExit()
        {
            Exit(objection);
            if (gameObject.GetComponent<Focusable>().IsHighlighted(gameObject))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);

        }
    }
}
