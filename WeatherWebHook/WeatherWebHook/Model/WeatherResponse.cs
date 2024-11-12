namespace WeatherWebHook.Model
{
    public class WeatherResponse
    {
        public string FulfillmentText { get; set; }
        // If you are using fulfillmentMessages, it could be like this:
        public List<FulfillmentMessage> FulfillmentMessages { get; set; }
    }
    public class FulfillmentMessage
    {
        public Text Text { get; set; }
    }

    public class Text
    {
        public List<string> TextList { get; set; }
    }
}
