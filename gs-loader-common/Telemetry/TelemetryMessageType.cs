namespace gs_loader_common.Telemetry
{
    /// <summary>
    /// Tipo de mensagem de telemetria
    /// </summary>
    public enum TelemetryMessageType
    {
        None = 0,
        InstallSuccess = 1,
        InstallError = 2,
        UpdateSuccess = 3,
        UpdateError = 4,
        RunningExceptionLog = 5,
        RunningStatistics = 6
    }
}