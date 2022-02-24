//interface for the pickup item manager

public interface IGameManager {
    ManagerStatus status { get; }

    void Startup();
}