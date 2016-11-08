using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
            print(nm.GetComponentInChildren<NotificationManager>());
            if (search.message.Equals("Nothing Here."))
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
 
        }
    }
}
