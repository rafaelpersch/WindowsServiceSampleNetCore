using Microsoft.Extensions.Configuration;

namespace WorkerServiceExample
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        //private readonly ILogger<Worker> _logger;
        private Thread _workerThread;
        private bool _running;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            //_logger = logger;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("=== OnStart called at: {time}", DateTimeOffset.Now);
            Log($"OnStart...");

            _running = true;
            _workerThread = new Thread(DoWork);
            _workerThread.Start();

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            while (_running)
            {
                //_logger.LogInformation("Working... {time}", DateTimeOffset.Now);
                Log($"Working... " + _configuration["KeyTest"]);
                Thread.Sleep(5000); // Sleep for 5 seconds
            }

            //_logger.LogInformation("Worker thread exiting.");
            Log($"Worker thread exiting.");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("=== OnStop called at: {time}", DateTimeOffset.Now);
            Log($"OnStop...");
            _running = false;

            if (_workerThread != null && _workerThread.IsAlive)
            {
                _workerThread.Join(); // Wait for the thread to finish
            }

            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This method won't be used since we're handling everything in Start/Stop
            return Task.CompletedTask;
        }

        private void Log(string message)
        {
            try
            {
                string logPath = Path.Combine(AppContext.BaseDirectory, "logs", $"log_{DateTime.Now.ToString("yyyy_MM_dd")}.log");

                string directory = Path.GetDirectoryName(logPath);
                
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";

                File.AppendAllText(logPath, logLine);
            }
            catch
            {
                // Evita travar o serviço por falha de log
            }
        }

    }
}