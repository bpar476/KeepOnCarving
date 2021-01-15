using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{

    private IDictionary<Type, IDictionary<Guid, IEventResponder>> channels;

    public EventBus()
    {
        channels = new Dictionary<Type, IDictionary<Guid, IEventResponder>>();
    }

    public Guid ListenTo<TEvent>(Action<TEvent> response) where TEvent : KeepOnCarvingEvent
    {
        var type = typeof(TEvent);
        if (!channels.ContainsKey(type))
        {
            channels.Add(type, new Dictionary<Guid, IEventResponder>());
        }
        var guid = Guid.NewGuid();
        channels[type].Add(guid, new EventResponder<TEvent>(response));
        return guid;
    }

    public void Raise<TEvent>(TEvent publishedEvent) where TEvent : KeepOnCarvingEvent
    {
        var type = typeof(TEvent);
        Debug.LogFormat("Raised {0} event", type.Name);
        if (!channels.ContainsKey(type))
        {
            channels.Add(type, new Dictionary<Guid, IEventResponder>());
        }
        foreach (var entry in channels[type])
        {
            entry.Value.Respond(publishedEvent);
        }
    }

    public void UnsubscribeFrom<TEvent>(Guid identifier)
    {
        var type = typeof(TEvent);
        if (channels.ContainsKey(type))
        {
            channels[type].Remove(identifier);
        }
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