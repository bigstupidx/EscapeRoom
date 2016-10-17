using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


	[TestFixture]
	public class NUnitTestClass
	{
		[Test]
		public void TestCase ()
		{
		}

		[Test]
		public void TestSaveRecordingsButton()
		{
			MenuManager m = new MenuManager ();
			//EditorSceneManager.OpenScene ("MenuScene");
			m.RecordingsButtonPressed ();
			Scene currentScene = SceneManager.GetActiveScene ();

			string sceneName = currentScene.name;
			Assert.Equals("RecordingSavesMenu", sceneName);
		}
	}


