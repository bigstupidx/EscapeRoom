using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputRecorder : Recorder, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	private struct RecordedEvent
	{
		// The time the event occured, relative to the start of the recording in seconds
		public float Time { get; set; }

		// The type of event that occured
		public EventType Type { get; set; }

		// The data from the event
		public PointerEventData Data { get; set; }
	}

	private enum EventType
	{
		OnPointerEnter,
		OnPointerExit,
		OnPointerDown,
		OnPointerUp,
		OnPointerClick
	};

	Queue<RecordedEvent> events = new Queue<RecordedEvent>();

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
				RecordedEvent evt = events.Peek();

				if (evt.Time <= playbackTime) {
					// Dequeue and dispatch the event since its time has come
					DispatchEvent(events.Dequeue());
				} else {
					// Stop looping after we hit the first one that is in the future, since they are in time order
					break;
				}
			}
		}
	}

	private void DispatchEvent(RecordedEvent evt)
	{
		EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;

		switch (evt.Type) {
		case EventType.OnPointerEnter:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerEnterHandler);
			break;
		case EventType.OnPointerExit:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerExitHandler);
			break;
		case EventType.OnPointerDown:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerDownHandler);
			break;
		case EventType.OnPointerUp:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerUpHandler);
			break;
		case EventType.OnPointerClick:
			ExecuteEvents.Execute(this.gameObject, new PointerEventData(eventSystem), ExecuteEvents.pointerClickHandler);
			break;
		}
	}

	public override void StartRecording()
	{
		// Clear any previously recorded events
		events.Clear();

		base.StartRecording();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new RecordedEvent() {
				Time = GetRelativeTime(),
				Type = EventType.OnPointerEnter,
				Data = eventData
			});
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new RecordedEvent() {
				Time = GetRelativeTime(),
				Type = EventType.OnPointerExit,
				Data = eventData
			});
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new RecordedEvent() {
				Time = GetRelativeTime(),
				Type = EventType.OnPointerDown,
				Data = eventData
			});
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new RecordedEvent() {
				Time = GetRelativeTime(),
				Type = EventType.OnPointerUp,
				Data = eventData
			});
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (State == RecordingState.Recording) {
			events.Enqueue(new RecordedEvent() {
				Time = GetRelativeTime(),
				Type = EventType.OnPointerClick,
				Data = eventData
			});
		}
	}
}

