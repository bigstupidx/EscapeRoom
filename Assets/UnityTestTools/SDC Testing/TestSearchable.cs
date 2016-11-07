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
            Click(objection);
        }
    }
}
