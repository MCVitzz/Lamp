package engine.events;

public interface EventTrigger<T> {
    public void addListener(Listener listener);
    public void notifyListeners();
    public void eventOccurred(T event);
}
