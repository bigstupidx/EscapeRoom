using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class MotionRecorder : Recorder
{
	// Structure to make it easier to refer to the 3 components of an animation curve
	protected class Vector3Curve
	{
		public AnimationCurve X = new AnimationCurve();

		public AnimationCurve Y = new AnimationCurve();

		public AnimationCurve Z = new AnimationCurve();

		// Reset all animation curves to new empty ones
		public virtual void Clear()
		{
			X = new AnimationCurve();
			Y = new AnimationCurve();
			Z = new AnimationCurve();
		}
	}

	protected class Vector4Curve : Vector3Curve
	{
		public AnimationCurve W = new AnimationCurve();

		public override void Clear()
		{
			base.Clear();
			W = new AnimationCurve();
		}
	}
		
	// Sample rate of 1/20 of a second
	private float samplesPerSecond = 20.0f;

	public float SamplesPerSecond {
		get { return samplesPerSecond; }
		set { samplesPerSecond = value; }
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		switch (State) {
		case RecordingState.Playing:
			SampleCurves(GetRelativeTime());
			break;
		case RecordingState.Recording:
			if (GetRelativeTime() - lastKeyTime > 1.0f / SamplesPerSecond) {
				RecordKeyFrame(GetRelativeTime());
			}
			break;
		}
	}

	public override void StartRecording(float realStartTime)
	{
		base.StartRecording(realStartTime);

		// Record a key frame at the start of recording to ensure that original position is saved
		RecordKeyFrame(0);
	}

	public override void StopRecording()
	{
		base.StopRecording();
	}

	public override void StartPlayback(float realStartTime)
	{
		base.StartPlayback(realStartTime);

		// Sample initial position
		SampleCurves(GetRelativeTime());
	}

	public override void StopPlayback()
	{
		base.StopPlayback();

		// Sample final position
		SampleCurves(GetRelativeTime());
	}

	protected abstract void RecordKeyFrame(float time);

	protected abstract void SampleCurves(float time);
}

