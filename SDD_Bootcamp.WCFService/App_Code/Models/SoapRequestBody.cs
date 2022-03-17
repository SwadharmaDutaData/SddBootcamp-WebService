using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for SoapRequestBody
/// </summary>
public class SoapRequestBody<T> : SoapHeader
{
    public string RequestOperation { get; set; }
    public T RequstBody { get; set; }
}