using AdminExpedientePersonal.Services.Interfaces;

namespace AdminExpedientePersonal.Services;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Publish<T>(T @event)
    {
        if (_handlers.ContainsKey(typeof(T)))
        {
            foreach (var handler in _handlers[typeof(T)].Cast<Action<T>>())
            {
                handler(@event);
            }
        }
    }
    public void Subscribe<T>(Action<T> action)
    {
        if (!_handlers.ContainsKey(typeof(T)))
        {
            _handlers[typeof(T)] = new List<Delegate>();
        }
        _handlers[typeof(T)].Add(action);
    }
}