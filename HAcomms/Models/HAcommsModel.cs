using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using NoeticTools.Net2HassMqtt.Configuration;
using NoeticTools.Net2HassMqtt.Configuration.Building;
using CommunityToolkit.Mvvm.ComponentModel;
using NoeticTools.Net2HassMqtt.Entities.Framework;

namespace HAcomms.Models;

public partial class HAcommsModel : ObservableObject {
    [ObservableProperty] private bool _watchedEntriesPresent;
    [ObservableProperty] private bool _webcamInUse;
    [ObservableProperty] private bool _microphoneInUse;
    public HaEvent KeyboardEvent = new(["keyboard_combo_pressed"]);

    public INet2HassMqttBridge BuildBridge(IConfigurationRoot appConfig) {
        var device = new DeviceBuilder().WithId("hacomms")
            .WithFriendlyName("HAcomms")
            .WithManufacturer("AlmostInteractive")
            .WithModel("HA Desktop Comms");

        device.HasBinarySensor(config => config.OnModel(this)
            .WithStatusProperty(nameof(WatchedEntriesPresent))
            .WithFriendlyName("Watched Entries Present")
            .WithNodeId("watched_entries_present"));

        device.HasBinarySensor(config => config.OnModel(this)
            .WithStatusProperty(nameof(WebcamInUse))
            .WithFriendlyName("Webcam In Use")
            .WithNodeId("webcam_in_use"));
        
        device.HasBinarySensor(config => config.OnModel(this)
            .WithStatusProperty(nameof(MicrophoneInUse))
            .WithFriendlyName("Microphone In Use")
            .WithNodeId("microphone_in_use"));

        device.HasButton(config => config.OnModel(this)
            .WithCommandMethod("CommandMethod")
            .WithFriendlyName("Test Button")
            .WithNodeId("test_button"));

        device.HasEvent(config => config.OnModel(this)
            .WithEvent(nameof(KeyboardEvent))
            .WithFriendlyName("Test Event")
            .WithNodeId("test_event"));

        var mqttOptions = HassMqttClientFactory.CreateQuickStartOptions(Properties.Resources.MqttClientId, appConfig);
        return new BridgeConfiguration()
            .WithMqttOptions(mqttOptions)
            .HasDevice(device)
            .Build();
    }

    public void CommandMethod(string json) { Console.WriteLine("CommandMethod: {0}", json); }
}