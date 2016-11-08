using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TestSearch
{
    public class TestTelevision : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Television search;
        public enum Functions
        {
            Start,
            OnPointerClick,
        }

        public void Start()
        {
            search = objection.GetComponent<Television>();
            print(search.key.activeSelf);
            OnPointerClick();
        }

        public void OnPointerClick()
        {
            Click(objection);
            search = objection.GetComponent<Television>();
            if (search.message.Equals("You found the key!") && search.key.activeSelf )
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail("Key scenario after click didn't work out");

        }
    }
}
