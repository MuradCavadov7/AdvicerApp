﻿namespace AdvicerApp.BL.Helper;

public class SmtpOptions
{
    public const string Name = "SmtpSettings";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
    public string Sender { get; set; }
}
