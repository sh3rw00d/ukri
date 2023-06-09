﻿using System.Net.Http.Headers;
using Newtonsoft.Json;
using Serilog;
using Ukri.Cli.Api.Model;

namespace Ukri.Cli.Api;

public class ApiClient : IDisposable
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://gtr.ukri.org"),
        };

        _httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/vnd.rcuk.gtr.json-v7"));

    }
    
    public async Task<Fund> GetFundAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/gtr/api/funds/{id}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Fund>(json);
    }

    public async Task<Project[]> SearchProjectsAsync(string query)
    {
        const int size = 100;
        
        var page = 0;
        var projects = new List<Project>();
        var searchProjectsResponse = new SearchProjectsResponse();
        do
        {
            page += 1;
            
            var response = await _httpClient.GetAsync($"/gtr/api/projects?q={query}&s={size}&p={page}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            searchProjectsResponse = JsonConvert.DeserializeObject<SearchProjectsResponse>(json);
            projects.AddRange(searchProjectsResponse.Projects);
        } while (page != searchProjectsResponse.TotalPages);

        return projects.ToArray();
    }

    public void Dispose() => _httpClient?.Dispose();
}