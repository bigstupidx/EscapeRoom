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
			// Test that it is not glowing at the start
			IntegrationTest.Assert(!SelectionGlow.IsObjectGlowing(objection));

			// Trigger the cursor to enter the object and test that it glows
			TriggerCursorEnter(objection);
			IntegrationTest.Assert(SelectionGlow.IsObjectGlowing(objection));

			// Trigger the cursor to exit the object and test that it stops glowing
			TriggerCursorExit(objection);
			IntegrationTest.Assert(!SelectionGlow.IsObjectGlowing(objection));

			IntegrationTest.Pass();
		}
	}
}
