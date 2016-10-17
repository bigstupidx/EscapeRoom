using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;


public class Recording
{
	/// <summary>
	/// Creates a new empty recording object.  This is probably much less useful externally than using the Recording.Load function to load a recording from a file,
	/// or the RecordingManager.GetRecordingFromActiveScene function to create a recording from the active scene.
	/// </summary>
	public Recording()
	{
		Timelines = new List<Timeline>();
	}

	/// <summary>
	/// A list of timelines contained in the recording.  These is one timeline for each type of data recordes for an object with one or more Recorder scripts attached.
	/// For example, the camera will have position and rotation timelines, and other objects will have input timelines.  Each timeline inclues the name of the object
	/// that it was originally recorded from, which is used to match it with recorders in the scene.
	/// </summary>
	/// <value>The timelines.</value>
	public List<Timeline> Timelines { get; set; }

	/// <summary>
	/// Saves the timeline data of this recording to a file.  The file will be loacated in Application.persistentDataPath which, on my Windows machine, is:
	/// C:\Users\{Your User Name}\AppData\LocalLow\DefaultCompany\VR Test 2
	/// </summary>
	/// <param name="fileName">The name of the file to save to</param>
	public void Save(string fileName)
	{
		var serializer = new XmlSerializer(typeof(Recording));
		using (var stream = new FileStream(Application.persistentDataPath + "\\" + fileName, FileMode.Create)) {
			serializer.Serialize(stream, this);
		}
	}

	/// <summary>
	/// Loads the specified file located in the Application.psersistentDataPath which, on my Windows machine, is:
	/// C:\Users\{Your User Name}\AppData\LocalLow\DefaultCompany\VR Test 2
	/// Note that this is static so "Recording.Load" can be called instead of using a constructor to create a Recording
	/// from a file.
	/// </summary>
	/// <param name="fileName">The name of the saved file load from</param>
	public static Recording Load(string fileName)
	{
		var serializer = new XmlSerializer(typeof(Recording));
			using (var stream = new FileStream(Application.persistentDataPath + "\\" + fileName, FileMode.Open)) {
			return serializer.Deserialize(stream) as Recording;
		}
	}

	public abstract class Key
	{
		// The time the key occured, relative to the start of the recording in seconds
		[XmlAttribute]
		public float Time { get; set; }
	}

	public class PositionKey : Key
	{
		[XmlAttribute]
		public float X { get; set; }

		[XmlAttribute]
		public float Y { get; set; }

		[XmlAttribute]
		public float Z { get; set; }
	}

	public class RotationKey : Key
	{
		[XmlAttribute]
		public float X { get; set; }

		[XmlAttribute]
		public float Y { get; set; }

		[XmlAttribute]
		public float Z { get; set; }

		[XmlAttribute]
		public float W { get; set; }
	}

	public enum EventType
	{
		OnPointerEnter,
		OnPointerExit,
		OnPointerDown,
		OnPointerUp,
		OnPointerClick}

	;

	public class RecordedEvent : Key
	{
		// The type of event that occured
		[XmlAttribute]
		public EventType Type { get; set; }
	}

	[XmlInclude(typeof(PositionTimeline))]
	[XmlInclude(typeof(RotationTimeline))]
	[XmlInclude(typeof(InputTimeline))]
	public abstract class Timeline
	{
		[XmlAttribute]
		public string ObjectName { get; set; }
	}

	public class PositionTimeline : Timeline
	{
		public PositionTimeline()
		{
			Positions = new List<PositionKey>();
		}

		public List<PositionKey> Positions { get; set; }
	}

	public class RotationTimeline : Timeline
	{
		public RotationTimeline()
		{
			Rotations = new List<RotationKey>();
		}

		public List<RotationKey> Rotations { get; set; }
	}

	public class InputTimeline : Timeline
	{
		public InputTimeline()
		{
			Events = new List<RecordedEvent>();
		}

		public List<RecordedEvent> Events { get; set; }
	}
}

