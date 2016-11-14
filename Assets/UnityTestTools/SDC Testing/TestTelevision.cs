using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestSearch
{
    public class TestTelevision : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Television search;
        public Canvas nm;
        public enum Functions
        {
            Start,
            OnPointerClick,
        }

        public void Start()
        {
            search = objection.GetComponent<Television>();
            OnPointerClick();
        }

        public void OnPointerClick()
        {
            Click(objection);
            search = objection.GetComponent<Television>();
            if (nm.GetComponentInChildren<Text>().text.Equals("You found the key!") && search.key.activeSelf )
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail("Key scenario after click didn't work out");

        }
    }
}
