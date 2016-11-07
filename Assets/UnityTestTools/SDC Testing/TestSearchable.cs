using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TestSearch { 
    public class TestSearchable : TestBase
    {
        public GameObject objection;
        public Functions callOnMethod;
        private Searchable search;
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
            Searchable search = objection.GetComponent<Searchable>();
            if (search.message.Equals("Nothing here."))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
 
        }
    }
}
