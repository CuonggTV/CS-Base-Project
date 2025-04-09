using System.Text.Json.Serialization;

namespace CS_Base_Project.DAL.Data.Metadatas;

public class PaginationMeta
{
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_items")]
    public long TotalItems { get; set; }

    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }
}