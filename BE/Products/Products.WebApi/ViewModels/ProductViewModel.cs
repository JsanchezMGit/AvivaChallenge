namespace Products.WebApi.ViewModels;

public class ProductViewModel
{
    private string _status;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Descrition { get; set; }
    public string Status => _status;
    public ProductViewModel(int id, string name, string description, int stock)
    {
        Id = id;
        Name = name;
        Descrition = description;
        _status = stock > 0 ? "Available" : "Not available";
    }
}
