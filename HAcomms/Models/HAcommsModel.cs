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
    private HaEvent _keyboardComboEvent = new(["keyboard_combo_pressed"]);
    

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

        device.HasEvent(config => config.OnModel(this)
            .WithEvent(nameof(_keyboardComboEvent))
            .WithFriendlyName("Keyboard Combo Event")
            .WithNodeId("keyboard_combo_event"));

        var mqttOptions = HassMqttClientFactory.CreateQuickStartOptions(Properties.Resources.MqttClientId, appConfig);
        return new BridgeConfiguration()
            .WithMqttOptions(mqttOptions)
            .HasDevice(device)
            .Build();
    }

    public void FireKeyboardComboEvent(IDictionary<string, string> args) {
        _keyboardComboEvent.Fire(args);
    }
    
    public void FireKeyboardComboEvent(params (string key, string value)[] args) {
        _keyboardComboEvent.Fire(args);
    }
}