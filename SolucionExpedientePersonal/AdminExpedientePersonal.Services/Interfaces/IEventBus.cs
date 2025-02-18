namespace AdminExpedientePersonal.Services.Interfaces;

public interface IEventBus
{
    void Publish<T>(T @event);
    void Subscribe<T>(Action<T> action);
}