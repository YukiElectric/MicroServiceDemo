namespace StudentAPI.Common
{
    public class CalllAPI
    {
        public static async Task<String> getAuth(String auth, String token) 
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5074/api/Account/"+auth);
                request.Headers.Add("Authorization", token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode) return string.Empty;
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e) {
                return new
                {
                    status = false,
                    message = e.Message
                }.ToString();
            }
        }
    }
}
