using System.Net.Http;

abstract class Service
{
    protected HttpClient _client;
    protected string BaseApiUrl = "https://localhost:44373/api/Produto";

    public Service()
    {
        _client = new HttpClient();
    }
}