using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SoapResponseBody
/// </summary>
public class SoapResponseBody<T>
{
    public string ResponCode { get; set; }
    public string ResponMessage { get; set; }
    public T ResponBody { get; set; }
}