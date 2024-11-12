namespace WeatherWebHook.Model
{
    public class DialogflowRequest
    {
        public QueryResult QueryResult { get; set; }
    }
    public class QueryResult
    {
        public IDictionary<string, object> Parameters { get; set; }
    }
}
