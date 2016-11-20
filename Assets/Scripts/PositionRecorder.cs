using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PositionRecorder : MotionRecorder
{
	// Animation curves to store recordings in
	private Vector3Curve translations = new Vector3Curve();

	protected override void RecordKeyFrame(float realTime)
	{
		// Save times relative to the begining of the recording
		float time = realTime - recordingStartTime;

		// Record position
		Vector3 localPosition = this.transform.localPosition;

		translations.X.AddKey(time, localPosition.x);
		translations.Y.AddKey(time, localPosition.y);
		translations.Z.AddKey(time, localPosition.z);

		lastKeyTime = time;
	}

	protected override void SampleCurves(float realTime)
	{
		// Get the time relative to the start of the animation playback
		float time = realTime - playbackStartTime;

		// Sample position from curves
		Vector3 newLocalPosition = new Vector3();

		newLocalPosition.x = translations.X.Evaluate(time);
		newLocalPosition.y = translations.Y.Evaluate(time);
		newLocalPosition.z = translations.Z.Evaluate(time);

		this.transform.localPosition = newLocalPosition;
	}

	public override List<Recording.Timeline> GetTimelines()
	{
		List<Recording.Timeline> result = new List<Recording.Timeline>();
		Recording.PositionTimeline posTimeline = new Recording.PositionTimeline();

		posTimeline.ObjectName = this.name;

		int count = translations.X.keys.Length;

		if (translations.Y.keys.Length != count || translations.Z.keys.Length != count) {
			throw new UnityException("Unexpected mismatch in X, Y, and Z position key frames");
		}

		for (int i = 0; i < count; ++i) {
			Recording.PositionKey key = new Recording.PositionKey();
			key.Time = translations.X.keys[i].time;
			key.X = translations.X.keys[i].value;
			key.Y = translations.Y.keys[i].value;
			key.Z = translations.Z.keys[i].value;
			posTimeline.Positions.Add(key);
		}

		result.Add(posTimeline);

		return result;
	}

	public override void SetTimelines(List<Recording.Timeline> timelines)
	{
		Clear();

		foreach (Recording.Timeline t in timelines) {
			Recording.PositionTimeline posTimeline = t as Recording.PositionTimeline;

			if (posTimeline != null) {
				foreach (Recording.PositionKey key in posTimeline.Positions) {
					translations.X.AddKey(key.Time, key.X);
					translations.Y.AddKey(key.Time, key.Y);
					translations.Z.AddKey(key.Time, key.Z);
				}
			}
		}
	}

	public override void Clear()
	{
		translations.Clear();
	}

	public override float Length {
		get {
			float translationLength = 0;

			if (translations.X.length > 0) {
				translationLength = translations.X.keys[translations.X.length - 1].time;
			}

			return translationLength;
		}
	}
}