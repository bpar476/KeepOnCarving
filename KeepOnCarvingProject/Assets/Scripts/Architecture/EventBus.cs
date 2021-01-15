using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    private IDictionary<Type, List<IEventResponder>> channels;

    public EventBus()
    {
        channels = new Dictionary<Type, List<IEventResponder>>();
    }

    public void ListenTo<TEvent>(Action<TEvent> response) where TEvent : KeepOnCarvingEvent
    {
        var type = typeof(TEvent);
        if (!channels.ContainsKey(type))
        {
            channels.Add(type, new List<IEventResponder>());
        }
        channels[type].Add(new EventResponder<TEvent>(response));
    }

    public void Raise<TEvent>(TEvent publishedEvent) where TEvent : KeepOnCarvingEvent
    {
        var type = typeof(TEvent);
        Debug.LogFormat("Raised {0} event", type.Name);
        if (!channels.ContainsKey(type))
        {
            channels.Add(type, new List<IEventResponder>());
        }
        channels[type].ForEach(action =>
        {
            action.Respond(publishedEvent);
        });
    }
}

public class EventResponder<TEvent> : IEventResponder where TEvent : KeepOnCarvingEvent
{
    private readonly Action<TEvent> response;

    public EventResponder(Action<TEvent> response)
    {
        this.response = response;
    }

    public void Respond(KeepOnCarvingEvent raisedEvent)
    {
        response.Invoke(raisedEvent as TEvent);
    }
}

public interface IEventResponder
{
    void Respond(KeepOnCarvingEvent raisedEvent);
}