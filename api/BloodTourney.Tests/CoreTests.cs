using Xunit;
using BloodTourney;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BloodTourney.Tests;
public class CoreTests
{
    private readonly ITestOutputHelper _out;
    public CoreTests(ITestOutputHelper testOutputHelper)
    {
        _out = testOutputHelper;
    }
    
    [Theory]
    [InlineData(Core.RulesetPresets.SardineBowl2025)]
    // [InlineData(Core.RulesetPresets.EuroBowl2025)]
    public void GetBaseRuleset_ShouldReturnValidRuleset(Core.RulesetPresets ruleset)
    {
        // Act
        var result = new Core().GetPresetRuleset(ruleset);

        var jsonSerializer = new Newtonsoft.Json.JsonSerializer
        {
            Converters = { new StringEnumConverter(), new JavaScriptDateTimeConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
        
        using (var stringWriter = new StringWriter()) 
        using (var jsonWriter = new JsonTextWriter(stringWriter))
        {
            jsonSerializer.Serialize(jsonWriter, result);
            _out.WriteLine(stringWriter.ToString());
        }


        // Assert
        
        // Ensure Victory Points have correct defaults
        Assert.Equal(3u, result.VictoryPoints.Win);
        Assert.Equal(1u, result.VictoryPoints.Draw);
        Assert.Equal(0u, result.VictoryPoints.Loss);

        // Validate Tiers exist
        Assert.NotNull(result.Tiers);
        Assert.NotEmpty(result.Tiers);

        // Validate TieBreakers
        Assert.NotNull(result.TieBreakers);
        Assert.NotEmpty(result.TieBreakers);

        // Validate Inducements
        Assert.NotNull(result.Inducements);
        Assert.NotEmpty(result.Inducements);
        
        // Specific case validation for SardineBowl2025
        if (ruleset == Core.RulesetPresets.SardineBowl2025)
        {
            Assert.Contains("Net TD's + Net CAS", result.TieBreakers);
            Assert.Contains("Random", result.TieBreakers);
            Assert.Equal(135u, result.Timekeeping.MatchTimelimitInMinutes);
            Assert.True(result.Timekeeping.ChessClockAllowed);
        }
    }
}
