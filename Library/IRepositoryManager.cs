namespace Library;

public interface IRepositoryManager
{
    void Initialize();
    Task Register(string itemName, string itemContent, int itemType);
    Task<string> Retrieve(string itemName);
    Task<int> GetType(string itemName);
    Task Deregister(string itemName);
}