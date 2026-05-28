public class ProductUpdateDTO
{
    public string ProductName { get; set; }

    public string ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }

    public int CatId { get; set; }

    public bool IsAvailable { get; set; }
}