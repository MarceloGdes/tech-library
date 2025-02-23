﻿using System.Net;

namespace TechLibrary.Exception;

public class ErrorOnValidatioException(List<string> errorMessages) : TechLibraryException (string.Empty)
{
    private readonly List<string> _errors = errorMessages;

    public override List<string> GetErrorMessages() => _errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
