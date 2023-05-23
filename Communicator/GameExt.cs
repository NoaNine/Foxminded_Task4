using Microsoft.Extensions.Options;

namespace Communicator
{
    public class GameExt
    {
        private readonly INumberGenerator _generator;
        private readonly GameSettingsExt _settings;

        public GameExt(INumberGenerator generator, IOptionsMonitor<GameSettingsExt> settings) 
        { 
            _generator = generator ?? throw new ArgumentNullException(nameof(generator)); 
            _settings = settings?.CurrentValue ?? throw new ArgumentNullException(nameof(settings)); 
        }

        public void Debug() 
        { 
            Console.WriteLine($"Hide number: {_generator.GenerateNumber(_settings.MinValueOfHiddenNumber, _settings.MaxValueOfHiddenNumber)}"); 
            Console.WriteLine($"Settings: {_settings.MinValueOfHiddenNumber} / {_settings.MaxValueOfHiddenNumber}"); 
        }
    }
}
