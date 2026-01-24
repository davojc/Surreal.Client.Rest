using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Surreal.Client.Rest;

public partial class SurrealRestClient
{

	[LoggerMessage(
		EventId = 100,
		Level = LogLevel.Error,
		Message = "Error trying to call SurrealDb endpoint: {Url}")]
	partial void LogCallError(Exception exception, string url);
}

