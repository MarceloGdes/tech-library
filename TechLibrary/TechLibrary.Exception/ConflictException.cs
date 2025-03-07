﻿using System.Net;

namespace TechLibrary.Exception;

public class ConflictException(string message) : TechLibraryException(message)
{
    public override List<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}
