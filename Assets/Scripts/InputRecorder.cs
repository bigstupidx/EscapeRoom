using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputRecorder : Recorder, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	Queue<Recording.RecordedEvent> events = new Queue<Recording.RecordedEvent>();
	Queue<Recording.RecordedEvent> finishedEvents = new Queue<Recording.RecordedEvent>();

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (State == RecordingState.Playing) {
			// Check if the time for any events on the queue has come yet
			float playbackTime = GetRelativeTime();

			while (events.Count > 0) {
				Recording.RecordedEvent evt = events.Peek();

				if (evt.Time <= playbackTime) {
					// Dequeue and dispatch the event since its time has come
					evt = events.Dequeue();

					DispatchEvent(evt);

					finishedEvents.Enqueue(evt);
				} else {
					// Stop looping after we hit the first one that is in the future, since they are in time order
					break;
				}
			}
		}
	}

	public override void StopPlayback()
	{
		base.StopPlayback();

		// Re-enqueue all played events
		while (finishedEvents.Count > 0) {
			events.Enqueue(finishedEvents.Dequeue());
		}
	}

	private void DispatchEvent(Recording.RecordedEvent evt)
	{
		EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;

		switch (evt.Type) {
		case Recording.EventType.OnPointerEnter:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerEnterHandler);
			break;
		case Recording.EventType.OnPointerExit:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerExitHandler);
			break;
		case Recording.EventType.OnPointerDown:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerDownHandler);
			break;
		case Recording.EventType.OnPointerUp:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerUpHandler);
			break;
		case Recording.EventType.OnPointerClick:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerClickHandler);
			break;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new Recording.RecordedEvent() {
				Time = GetRelativeTime(),
				Type = Recording.EventType.OnPointerEnter
			});
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new Recording.RecordedEvent() {
				Time = GetRelativeTime(),
				Type = Recording.EventType.OnPointerExit
			});
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new Recording.RecordedEvent() {
				Time = GetRelativeTime(),
				Type = Recording.EventType.OnPointerDown
			});
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new Recording.RecordedEvent() {
				Time = GetRelativeTime(),
				Type = Recording.EventType.OnPointerUp
			});
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new Recording.RecordedEvent() {
				Time = GetRelativeTime(),
				Type = Recording.EventType.OnPointerClick
			});
		}
	}

	public override List<Recording.Timeline> GetTimelines()
	{
		List<Recording.Timeline> result = new List<Recording.Timeline>();
		Recording.InputTimeline timeline = new Recording.InputTimeline();
		timeline.ObjectName = this.name;

		foreach (Recording.RecordedEvent evt in events) {
			timeline.Events.Add(evt);
		}

		result.Add(timeline);
		return result;
	}

	public override void SetTimelines(List<Recording.Timeline> timelines)
	{
		Clear();

		foreach (Recording.Timeline t in timelines) {
			Recording.InputTimeline inputTimeline = t as Recording.InputTimeline;

			if (inputTimeline != null) {
				foreach (Recording.RecordedEvent evt in inputTimeline.Events) {
					this.events.Enqueue(evt);
				}
			}
		}
	}

	public override void Clear()
	{
		events.Clear();
		finishedEvents.Clear();
	}

	public override float Length {
		get {
			if (events.Count == 0) {
				return 0;
			}

			var array = this.events.ToArray();
			return array[array.Length - 1].Time;
		}
	}
}

