using HAcomms.BrowserTools;
using HAcomms.Tools;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using NoeticTools.Net2HassMqtt.Configuration;
using NoeticTools.Net2HassMqtt.Configuration.Building;

namespace HAcomms.Models;

using CommunityToolkit.Mvvm.ComponentModel;

// A Better way of doing the same thing as above ...
public partial class HAcommsModel : ObservableObject {
    [ObservableProperty] private bool _watchedEntriesPresent;
    
    private INet2HassMqttBridge? _bridge; 

    public INet2HassMqttBridge BuildBridge(IConfigurationRoot appConfig) {
        var device = new DeviceBuilder().WithFriendlyName("HAcomms")
            .WithId("hacomms")
            .WithFriendlyName("HAcomms");

        device.HasBinarySensor(config => config.OnModel(this)
            .WithStatusProperty(nameof(HAcommsModel.WatchedEntriesPresent))
            .WithFriendlyName("Watched Entries Present")
            .WithNodeId("watched_entries_present"));

        var mqttOptions = HassMqttClientFactory.CreateQuickStartOptions(Properties.Resources.MqttClientId, appConfig);
        return _bridge = new BridgeConfiguration()
            .WithMqttOptions(mqttOptions)
            .HasDevice(device)
            .Build();
    }
}

