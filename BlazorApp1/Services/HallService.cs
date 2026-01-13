using BlazorApp1.DTO;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
namespace BlazorApp1.Services
{
    public class HallsService
    {
        private readonly IHttpClientFactory _factory;

        public HallsService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<HallDto>> GetHallsAsync()
        {
            var client = _factory.CreateClient("Backend");
            return await client.GetFromJsonAsync<List<HallDto>>("api/Halls")
                   ?? new List<HallDto>();
        }
        public async Task<(bool ok, string message)> UploadXmlAsync(IBrowserFile file, CancellationToken ct = default)
        {
            // You can raise this if your XML is bigger
            const long maxFileSize = 10 * 1024 * 1024; // 10 MB

            if (file is null)
                return (false, "No file selected.");

            if (file.Size > maxFileSize)
                return (false, $"File is too large. Max {maxFileSize / (1024 * 1024)} MB.");

            using var content = new MultipartFormDataContent();

            // Read the file stream
            await using var stream = file.OpenReadStream(maxAllowedSize: maxFileSize, cancellationToken: ct);

            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType ?? "application/xml");

            // IMPORTANT: "file" must match your controller parameter name: ImportXml(IFormFile file)
            content.Add(fileContent, "file", file.Name);

            var client = _factory.CreateClient("Backend");

            // If your endpoint returns string (Ok("...")), ReadAsStringAsync is fine
            var response = await client.PostAsync("api/import/xml", content, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            return response.IsSuccessStatusCode
                ? (true, string.IsNullOrWhiteSpace(body) ? "XML imported successfully." : body)
                : (false, string.IsNullOrWhiteSpace(body) ? $"Upload failed: {response.StatusCode}" : body);
        }
    }
}

