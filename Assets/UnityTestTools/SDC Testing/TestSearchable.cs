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
            OnPointerClick,
        }

        public void OnPointerClick()
        {
            search.OnPointerClick()
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(objection, pointer, ExecuteEvents.pointerClickHandler);
            if (message == "Nothing here.")
                IntegrationTest.Pass(gameObject);
            else
                IntegrationTest.Fail(gameObject);
        }
    }
}
