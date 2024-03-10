namespace MyEntegrasyon.Services
{
    public class MatchService : IMathService
    {
        private readonly ILogger<MatchService> _logger;
        public MatchService(ILogger<MatchService> logger)
        {
                _logger = logger;
        }
        public decimal Divide(decimal parameterOne, decimal parameterTwo)
        {
            _logger.LogInformation(" parameter 1: " + parameterOne);
            _logger.LogInformation(" parameter 2: " + parameterTwo);

            decimal result = decimal.Zero;
            try
            {
                result = parameterOne / parameterTwo;
            }
            catch (DivideByZeroException e)
            {
                _logger.LogInformation(e + "You cannot divide by zero.");
                //throw e;
            }

            return result;
        }
    }
}
