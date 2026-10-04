using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Platform;

namespace map_app;

public class NodeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "road";

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    [JsonPropertyName("accessible")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Accessible { get; set; }

    [JsonPropertyName("building")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Building { get; set; }
}

public class EdgeDto
{
    [JsonPropertyName("source")]
    public int Source { get; set; }

    [JsonPropertyName("target")]
    public int Target { get; set; }

    /// <summary>Время прохождения связи в секундах.</summary>
    [JsonPropertyName("time")]
    public double Time { get; set; }
}

public class GraphDataDto
{
    [JsonPropertyName("nodes")]
    public List<NodeDto> Nodes { get; set; } = new();

    [JsonPropertyName("edges")]
    public List<EdgeDto> Edges { get; set; } = new();
}

public static class GraphLoader
{
    public static GraphDataDto? LoadFromResource(string resourceUri)
    {
        try
        {
            using var stream = AssetLoader.Open(new Uri(resourceUri));
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<GraphDataDto>(stream, options);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[GraphLoader Error]: {ex.Message}");
            return null;
        }
    }
}