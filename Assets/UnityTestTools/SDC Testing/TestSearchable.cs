using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestSearch { 
    public class TestSearchable : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Searchable search;
        public Canvas nm;
        public enum Functions
        {
            Start,
            OnPointerClick,
        }

        public void Start()
        {
            OnPointerClick();
        }
        public void OnPointerClick()
        {
            Click(objection);
            search = objection.GetComponent<Searchable>();
            if (nm.GetComponentInChildren<Text>().text.Equals("Nothing Here."))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
 
        }
    }
}
