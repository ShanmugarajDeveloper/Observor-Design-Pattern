using System;
using System.Collections.Generic;

// Subject interface
public interface ISubject
{
    void Notify();
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
}

// Observer interface
public interface IObserver
{
    void Update(string weather);
}

// RealSubject (Concrete Subject)
public class WeatherStation : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _weather;

    public void SetWeather(string weather)
    {
        _weather = weather;
        Notify();  // Violated: Directly calls Notify in setter, without handling it properly.
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_weather);  // Violated: Not passing any data to the observers.
        }
    }

    public void Subscribe(IObserver observer)
    {
        bool isSubscribed=_observers.Contains(observer);
        if(!isSubscribed)
            _observers.Add(observer); 
    }

    public void Unsubscribe(IObserver observer)
    {
        bool subscribed = _observers.Contains(observer);
        if(subscribed)
            _observers.Remove(observer);
    }
}

// Concrete Observer
public class Display : IObserver
{
    public void Update(string weather)
    {
        Console.WriteLine(weather+" updated!");
        // No data from the Subject (WeatherStation), violates the pattern as Observer isn't notified with any information.
    }
}

public class Client
{
    public static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();

        Display display1 = new Display();
        Display display2 = new Display();
        weatherStation.Subscribe(display1);
        weatherStation.Subscribe(display2);

        weatherStation.SetWeather("Sunny");
        weatherStation.SetWeather("Rainy");

        weatherStation.Unsubscribe(display1);  // Correct way to unsubscribe
        weatherStation.SetWeather("Cloudy");
    }
}
