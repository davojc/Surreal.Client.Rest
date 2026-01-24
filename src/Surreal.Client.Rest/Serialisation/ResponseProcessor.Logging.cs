using Microsoft.Extensions.Logging;

namespace Surreal.Client.Rest.Serialisation;

internal partial class ResponseProcessor
{
    [LoggerMessage(
    EventId = 100,
    Level = LogLevel.Error,
    Message = "Error processing response from SurrealDb")]
    partial void LogProcessingError(Exception exception);


    [LoggerMessage(
        EventId = 100,
        Level = LogLevel.Error,
        Message = "Raw response: Content: {Content}")]
    partial void LogProcessingDebug(string content);
}
