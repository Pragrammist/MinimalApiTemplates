public class AppConfigurationException : Exception{
    public AppConfigurationException(string message) : base($"Check configuration: {message}")
    {
        
    }


}