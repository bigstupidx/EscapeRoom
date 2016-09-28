using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;


public class Recording
{
	public Recording()
	{
		Timelines = new List<Timeline>();
	}

	public List<Timeline> Timelines { get; set; }

	public void Save(string fileName)
	{
		var serializer = new XmlSerializer(typeof(Recording));
		using (var stream = new FileStream(Application.persistentDataPath + "\\" + fileName, FileMode.Create)) {
			serializer.Serialize(stream, this);
		}
	}

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

